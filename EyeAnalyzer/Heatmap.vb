''' <summary>
''' Models a heatmap showing fixations over a stimulus image.
''' </summary>
Public Class Heatmap

    ''' <summary>
    ''' Represents a single fixation point on a heatmap.
    ''' </summary>
    Public Structure FixationPoint
        Public x As Integer
        Public y As Integer
        Public strength As Single
    End Structure

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

    Public Overrides Function ToString() As String
        Return _stimulusName & " [" & _stimulusImageFilename & "]"
    End Function

    ''' <summary>
    ''' Constructs a new heatmap with the specified stimulus name.
    ''' </summary>
    Public Sub New(ByVal stimulusName As String)
        _stimulusName = stimulusName
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
    ''' Renders the heatmap image by rendering fixations over the background image.
    ''' </summary>
    Private Sub render()
        Dim fixationsImage As Bitmap = renderFixations()
        If _stimulusImage IsNot Nothing Then
            _heatmapImage = New Bitmap(_stimulusImage.Width, _stimulusImage.Height, Imaging.PixelFormat.Format32bppPArgb)
            Using g = Graphics.FromImage(_heatmapImage)
                g.Clear(Color.Black)
                g.DrawImageUnscaledAndClipped(_stimulusImage, New Rectangle(0, 0, _stimulusImage.Width, _stimulusImage.Height))
                g.DrawImageUnscaledAndClipped(fixationsImage, New Rectangle(0, 0, fixationsImage.Width, fixationsImage.Height))
            End Using
        Else
            _heatmapImage = fixationsImage
        End If
    End Sub

    ''' <summary>
    ''' Renders the fixation points over a transparent background.
    ''' </summary>
    Private Function renderFixations() As Bitmap
        Dim b As Bitmap = New Bitmap(_maxFixationX + 1, _maxFixationY + 1, Imaging.PixelFormat.Format32bppPArgb)
        Using g = Graphics.FromImage(b)
            g.Clear(Color.Transparent)

            For Each fp As FixationPoint In _fixations
                Dim x As Integer = fp.x - 2
                Dim y As Integer = fp.y - 2
                g.FillEllipse(Brushes.Red, x, y, 4, 4)
            Next

        End Using
        Return b
    End Function
End Class
