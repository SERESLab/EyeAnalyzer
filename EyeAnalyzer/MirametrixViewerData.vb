''' <summary>
''' Represents a collection of data measured by a
''' Mirametrix Viewer eye tracker.
''' </summary>
Public Class MirametrixViewerData

    ''' <summary>
    ''' Models a gaze point on the screen.
    ''' </summary>
    Private Structure PointOfGaze
        Public start As ULong
        Public duration As ULong
        Public isValid As Boolean
        Public x As Integer
        Public y As Integer
    End Structure

    Private _gazes As New Dictionary(Of String, PointOfGaze)
    Private _screenWidth As Integer = 0
    Private _screenHeight As Integer = 0
    Private _averageCalibrationError As Single = 0.0
    Private _validCalibrationPoints As Integer = 0

    ''' <summary>
    ''' Gets the number of gaze points.
    ''' </summary>
    Public ReadOnly Property GazeCount As Integer
        Get
            Return _gazes.Count
        End Get
    End Property

    ''' <summary>
    ''' Gets the width of the screen area in pixels.
    ''' </summary>
    Public ReadOnly Property ScreenWidth As Integer
        Get
            Return _screenWidth
        End Get
    End Property

    ''' <summary>
    ''' Gets the height of the screen area in pixels.
    ''' </summary>
    Public ReadOnly Property ScreenHeight As Integer
        Get
            Return _screenHeight
        End Get
    End Property

    ''' <summary>
    ''' Gets the average error (in pixels) over all calibration points.
    ''' </summary>
    Public ReadOnly Property AverageCalibrationError As Single
        Get
            Return _averageCalibrationError
        End Get
    End Property

    ''' <summary>
    ''' Gets the number of calibration points with valid data.
    ''' </summary>
    Public ReadOnly Property ValidCalibrationPoints As Integer
        Get
            Return _validCalibrationPoints
        End Get
    End Property

    ''' <summary>
    ''' Creates a new viewer data object from the specified file.
    ''' </summary>
    Public Shared Function FromFile(ByVal filename As String) As MirametrixViewerData
        Return New MirametrixViewerData(filename)
    End Function

    ''' <summary>
    ''' Private constructor.
    ''' </summary>
    Private Sub New(ByVal filename As String)

        Dim firstGazeMs As ULong = 0

        Using reader As New System.Xml.XmlTextReader(filename)
            While reader.Read()
                If reader.IsStartElement("REC") Then

                    Dim id As String = reader.GetAttribute("FPOGID")
                    Dim startStr As String = reader.GetAttribute("FPOGS")
                    Dim durationStr As String = reader.GetAttribute("FPOGD")
                    Dim xPercentStr As String = reader.GetAttribute("FPOGX")
                    Dim yPercentStr As String = reader.GetAttribute("FPOGY")
                    Dim isValidStr As String = reader.GetAttribute("FPOGV")

                    If id Is Nothing Or startStr Is Nothing Or durationStr Is Nothing Or _
                        xPercentStr Is Nothing Or yPercentStr Is Nothing Or isValidStr Is Nothing Then
                        Throw New Exception("Bad format.")
                    End If

                    Dim startMs As ULong = Double.Parse(startStr) * 1000
                    If firstGazeMs = 0 Then
                        firstGazeMs = startMs
                    End If

                    If isValidStr = "1" Then
                        Dim gaze As New PointOfGaze
                        gaze.start = startMs - firstGazeMs
                        gaze.duration = Double.Parse(durationStr) * 1000
                        If _screenWidth > 0 And _screenHeight > 0 Then
                            Dim xPercent As Single = Single.Parse(xPercentStr)
                            Dim yPercent As Single = Single.Parse(yPercentStr)
                            gaze.x = xPercent * _screenWidth
                            gaze.y = yPercent * _screenHeight
                        Else
                            Throw New Exception("Bad format.")
                        End If

                        If Not _gazes.ContainsKey(id) Then
                            _gazes.Add(id, gaze)
                        Else
                            Dim avg As PointOfGaze = _gazes.Item(id)
                            avg.duration = Math.Max(avg.duration, gaze.duration)
                            avg.x = (avg.x + gaze.x) / 2
                            avg.y = (avg.y + gaze.y) / 2
                            _gazes.Item(id) = avg
                        End If

                    End If
                ElseIf reader.IsStartElement("SCREEN_SIZE") Then
                    Dim width As String = reader.GetAttribute("WIDTH")
                    Dim height As String = reader.GetAttribute("HEIGHT")
                    If width IsNot Nothing And height IsNot Nothing Then
                        _screenWidth = Integer.Parse(width)
                        _screenHeight = Integer.Parse(height)
                    End If
                ElseIf reader.IsStartElement("CALIB_ERROR") Then
                    Dim avgError As String = reader.GetAttribute("AVE_ERROR")
                    Dim validPoints As String = reader.GetAttribute("VALID_POINTS")
                    If avgError IsNot Nothing Then
                        _averageCalibrationError = Single.Parse(avgError.Trim)
                    End If
                    If validPoints IsNot Nothing Then
                        _validCalibrationPoints = Integer.Parse(validPoints.Trim)
                    End If
                End If
            End While
        End Using

        If _screenWidth = 0 Or _screenHeight = 0 Or _gazes.Count = 0 Then
            Throw New Exception("Invalid data.")
        End If
    End Sub

    ''' <summary>
    ''' Processes the eye data and returns an object storing the results.
    ''' </summary>
    Public Function process(ByVal minFixationDuration As Integer, _
                            ByVal stimulusSegments As List(Of StimulusSegment)) As ProcessingResults

        ' initialize results
        Dim results As New ProcessingResults(_averageCalibrationError, _validCalibrationPoints)
        For Each s As StimulusSegment In stimulusSegments
            results.createSegment(s.Name, s.DurationMs)
            For Each aoi As AreaOfInterest In s.AOIs
                results.createAOI(s.Name, aoi.Name)
            Next
        Next

        ' process fixations
        For Each pog As PointOfGaze In _gazes.Values

            ' locate segment in which the current gaze begins
            Dim segment As StimulusSegment = Nothing
            For Each s As StimulusSegment In stimulusSegments
                If pog.start >= s.StartMs And pog.start <= s.EndMs Then
                    segment = s
                    Exit For
                End If
            Next
            If segment Is Nothing Then
                Continue For
            End If

            Dim fixation As New Fixation(New Point(pog.x, pog.y))
            fixation.StartMs = pog.start
            fixation.EndMs = pog.start + pog.duration

            ' clip fixation to segment boundary
            If fixation.EndMs > segment.EndMs Then
                fixation.EndMs = segment.EndMs
            End If

            ' skip fixations less than the minimum duration
            If fixation.Duration < minFixationDuration Then
                Continue For
            End If

            ' find AOIs in which the fixation falls
            Dim excluded As Boolean = False
            For Each aoi In segment.AOIs
                If aoi.Area.Contains(fixation.Point) And _
                    (aoi.NonExclusive Or Not excluded) Then
                    results.addFixation(segment.Name, aoi.Name, fixation)
                    If Not aoi.NonExclusive Then
                        excluded = True
                    End If
                End If
            Next
        Next

        Return results
    End Function
End Class
