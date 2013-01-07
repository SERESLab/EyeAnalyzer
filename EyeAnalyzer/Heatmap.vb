Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

''' <summary>
''' Models a heatmap showing fixations over a stimulus image.
''' </summary>
Public Class Heatmap

    ''' <summary>
    ''' Represents a single fixation point on a heatmap.
    ''' </summary>
    Public Structure FixationPoint
        Public x As Double
        Public y As Double
        Public strength As Single
    End Structure

    Private _pointRadius As Integer
    Private _lowColor As Color
    Private _midColor As Color
    Private _highColor As Color
    Private _heatmapAlpha As Byte
    Private _remapPalette(255) As ColorMap

    Private _subjectCount As Integer = 0
    Private _fixations As New List(Of FixationPoint)
    Private _stimulusImageFilename As String = "no image"
    Private _stimulusImage As Image = Nothing
    Private _heatmapImage As Image = Nothing
    Private _stimulusName As String
    Private _requiresRender As Boolean = True
    Private _maxFixationX As Integer = 1
    Private _maxFixationY As Integer = 1

    ''' <summary>
    ''' Gets the number of subjects whose fixations have been added to the heatmap.
    ''' </summary>
    Public ReadOnly Property SubjectCount As Integer
        Get
            Return _subjectCount
        End Get
    End Property

    ''' <summary>
    ''' Gets the total number of fixations that have been added to the heatmap.
    ''' </summary>
    Public ReadOnly Property FixationCount As Integer
        Get
            Return _fixations.Count
        End Get
    End Property

    ''' <summary>
    ''' Gets the name of the stimulus over which the heatmap shows fixations.
    ''' </summary>
    Public ReadOnly Property StimulusName As String
        Get
            Return _stimulusName
        End Get
    End Property

    ''' <summary>
    ''' Gets the filename of the stimulus image if it has been loaded.
    ''' </summary>
    Public ReadOnly Property StimulusImageFilename As String
        Get
            Return _stimulusImageFilename
        End Get
    End Property

    ''' <summary>
    ''' Gets the rendered heatmap image.
    ''' </summary>
    Public ReadOnly Property Image As Image
        Get
            If _requiresRender Then
                render()
                _requiresRender = False
            End If
            Return _heatmapImage
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the color for rendering areas of low fixations.
    ''' </summary>
    Public Property LowFixationsColor As Color
        Get
            Return _lowColor
        End Get
        Set(ByVal value As Color)
            _lowColor = value
            generateRemapPalette()
            _requiresRender = True
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the color for rendering areas of medium fixations.
    ''' </summary>
    Public Property MidFixationsColor As Color
        Get
            Return _midColor
        End Get
        Set(ByVal value As Color)
            _midColor = value
            generateRemapPalette()
            _requiresRender = True
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the color for rendering areas of high fixations.
    ''' </summary>
    Public Property HighFixationsColor As Color
        Get
            Return _highColor
        End Get
        Set(ByVal value As Color)
            _highColor = value
            generateRemapPalette()
            _requiresRender = True
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the alpha value for rendering fixations.
    ''' </summary>
    Public Property FixationAlpha As Integer
        Get
            Return _heatmapAlpha
        End Get
        Set(ByVal value As Integer)
            _heatmapAlpha = value
            generateRemapPalette()
            _requiresRender = True
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the radius for rendering fixations.
    ''' </summary>
    Public Property FixationRadius As Integer
        Get
            Return _pointRadius
        End Get
        Set(ByVal value As Integer)
            _pointRadius = value
            _requiresRender = True
        End Set
    End Property


    Public Overrides Function ToString() As String
        Return _stimulusName & " [" & _stimulusImageFilename & "]"
    End Function

    ''' <summary>
    ''' Constructs a new heatmap with the specified properties.
    ''' </summary>
    Public Sub New(ByVal stimulusName As String, ByVal pointRadius As Integer, ByVal alpha As Integer, _
                   ByVal lowColor As Color, ByVal midColor As Color, ByVal highColor As Color)
        _stimulusName = stimulusName
        _pointRadius = pointRadius
        _heatmapAlpha = alpha
        _lowColor = lowColor
        _midColor = midColor
        _highColor = highColor
        generateRemapPalette()
    End Sub

    ''' <summary>
    ''' Destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()
        If _heatmapImage IsNot Nothing Then
            _heatmapImage.Dispose()
        End If
        If _stimulusImage IsNot Nothing Then
            _stimulusImage.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Adds the specified list of fixation points corresponding to a single subject.
    ''' </summary>
    Public Sub addSubjectFixations(ByVal fixationPoints As List(Of FixationPoint))
        If fixationPoints.Count > 0 Then
            For Each fp As FixationPoint In fixationPoints
                If fp.x > _maxFixationX Then
                    _maxFixationX = fp.x
                End If
                If fp.y > _maxFixationY Then
                    _maxFixationY = fp.y
                End If
                _fixations.Add(fp)
            Next
            _subjectCount += 1
            _requiresRender = True
        End If
    End Sub

    ''' <summary>
    ''' Loads the specified image to use as the background image on which
    ''' fixations are rendered.
    ''' </summary>
    Public Sub loadStimulusImage(ByVal filename As String)
        clearStimulusImage()
        _stimulusImage = Image.FromFile(filename)
        _stimulusImageFilename = filename.Substring(filename.LastIndexOf("\") + 1)
        _requiresRender = True
    End Sub

    ''' <summary>
    ''' Clears the background image.
    ''' </summary>
    Public Sub clearStimulusImage()
        If _stimulusImage IsNot Nothing Then
            _stimulusImage.Dispose()
        End If
        _stimulusImage = Nothing
        _stimulusImageFilename = "no image"
        _requiresRender = True
    End Sub

    ''' <summary>
    ''' Renders the heatmap image by rendering colored fixations over the background image.
    ''' </summary>
    Private Sub render()
        Dim intensityMap As Bitmap = renderFixationIntensityMap()
        If _stimulusImage IsNot Nothing Then
            _heatmapImage = New Bitmap(_stimulusImage.Width, _stimulusImage.Height, Imaging.PixelFormat.Format32bppPArgb)
        Else
            _heatmapImage = New Bitmap(intensityMap.Width, intensityMap.Height, Imaging.PixelFormat.Format32bppPArgb)
        End If

        Using g = Graphics.FromImage(_heatmapImage)
            g.Clear(Color.Transparent)
            If _stimulusImage IsNot Nothing Then
                g.DrawImageUnscaledAndClipped(_stimulusImage, New Rectangle(0, 0, _stimulusImage.Width, _stimulusImage.Height))
            End If

            Dim attributes As New ImageAttributes()
            attributes.SetRemapTable(_remapPalette)
            g.DrawImage(intensityMap, New Rectangle(0, 0, intensityMap.Width, intensityMap.Height), _
                        0, 0, intensityMap.Width, intensityMap.Height, GraphicsUnit.Pixel, attributes)
        End Using

    End Sub

    ''' <summary>
    ''' Renders the fixation points as an intensity map.
    ''' </summary>
    Private Function renderFixationIntensityMap() As Bitmap
        Dim b As Bitmap = New Bitmap(_maxFixationX + _pointRadius + 1, _maxFixationY + _pointRadius + 1)

        Using g = Graphics.FromImage(b)
            g.Clear(Color.White)

            For Each fp As FixationPoint In _fixations

                ' construct polygon to represent the fixation point
                Dim gradientPoints As New List(Of Point)
                For theta As Double = 0.0 To 2 * Math.PI Step Math.PI / 10
                    Dim x As Integer = CType(fp.x + _pointRadius * Math.Cos(theta), Integer)
                    Dim y As Integer = CType(fp.y + _pointRadius * Math.Sin(theta), Integer)
                    gradientPoints.Add(New Point(x, y))
                Next

                ' create the gradient brush based on the point's strength value
                Dim alpha As Integer = fp.strength * 128 + 127
                Dim pointsArray() As Point = gradientPoints.ToArray()
                Dim pathGradient As New PathGradientBrush(pointsArray)
                Dim colorBlend As New ColorBlend(3)
                colorBlend.Positions = New Single() {0.0, 0.8, 1.0}
                colorBlend.Colors = New Color() {Color.FromArgb(0, Color.White), _
                                                 Color.FromArgb(alpha / 2, Color.Black), _
                                                 Color.FromArgb(alpha, Color.Black)}
                pathGradient.InterpolationColors = colorBlend

                ' draw the point
                g.FillPolygon(pathGradient, pointsArray)
            Next

        End Using
        Return b
    End Function

    ''' <summary>
    ''' Generates the palette for remapping intensity values to colors.
    ''' </summary>
    Private Sub generateRemapPalette()
        For i As Integer = 0 To 255
            Dim map As New ColorMap()
            Dim newColor As Color
            Dim position As Single = (CType(i, Single) / 255)
            Dim alpha As Integer = _heatmapAlpha - _heatmapAlpha * position * position

            If i < 128 Then
                newColor = interpolateColors(_highColor, _midColor, position)
            Else
                newColor = interpolateColors(_midColor, _lowColor, position)
            End If

            map.OldColor = Color.FromArgb(i, i, i)
            map.NewColor = Color.FromArgb(alpha, newColor)
            _remapPalette(i) = map
        Next
    End Sub

    ''' <summary>
    ''' Returns a color between c1 and c2 at the specified position between 0 and 1.
    ''' </summary>
    Private Function interpolateColors(ByVal c1 As Color, ByVal c2 As Color, ByVal position As Single) As Color
        Dim r1 As Integer = c1.R
        Dim r2 As Integer = c2.R
        Dim g1 As Integer = c1.G
        Dim g2 As Integer = c2.G
        Dim b1 As Integer = c1.B
        Dim b2 As Integer = c2.B
        Dim r As Integer = position * (r2 - r1) + r1
        Dim g As Integer = position * (g2 - g1) + g1
        Dim b As Integer = position * (b2 - b1) + b1
        Return Color.FromArgb(r, g, b)
    End Function
End Class
