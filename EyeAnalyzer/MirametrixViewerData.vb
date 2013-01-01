''' <summary>
''' Represents a collection of data measured by a
''' Mirametrix Viewer eye tracker.
''' </summary>
Public Class MirametrixViewerData

    ''' <summary>
    ''' Models a gaze point on the screen.
    ''' </summary>
    Private Structure PointOfGaze
        Implements IComparable(Of PointOfGaze)
        Public time As ULong
        Public isValid As Boolean
        Public x As Integer
        Public y As Integer
        Public Function compareTo(ByVal other As PointOfGaze) As Integer _
            Implements IComparable(Of EyeAnalyzer.MirametrixViewerData.PointOfGaze).CompareTo
            If time < other.time Then
                Return -1
            ElseIf time > other.time Then
                Return 1
            Else
                Return 0
            End If
        End Function
    End Structure

    Private _gazes As New List(Of PointOfGaze)
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
        Dim firstTimeMs As ULong = 0
        Using reader As New System.Xml.XmlTextReader(filename)
            While reader.Read()
                If reader.IsStartElement("REC") Then
                    Dim timeStr As String = reader.GetAttribute("TIME")
                    Dim xPercentStr As String = reader.GetAttribute("FPOGX")
                    Dim yPercentStr As String = reader.GetAttribute("FPOGY")
                    Dim isValidStr As String = reader.GetAttribute("FPOGV")
                    If timeStr IsNot Nothing And xPercentStr IsNot Nothing And _
                        yPercentStr IsNot Nothing And isValidStr IsNot Nothing Then
                        Dim gaze As New PointOfGaze
                        gaze.isValid = (isValidStr = "1")

                        Dim timeMs As ULong = Double.Parse(timeStr) * 1000
                        If firstTimeMs = 0 Then
                            firstTimeMs = timeMs
                        End If
                        gaze.time = timeMs - firstTimeMs

                        If _screenWidth > 0 And _screenHeight > 0 Then
                            Dim xPercent As Single = Single.Parse(xPercentStr)
                            Dim yPercent As Single = Single.Parse(yPercentStr)
                            gaze.x = xPercent * _screenWidth
                            gaze.y = yPercent * _screenHeight
                        Else
                            Throw New Exception("Bad format.")
                        End If

                        _gazes.Add(gaze)
                    Else
                        Throw New Exception("Bad format.")
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
        _gazes.Sort()
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
        Dim lastSegment As StimulusSegment = Nothing
        Dim currentFixation As Fixation = Nothing
        For Each pog As PointOfGaze In _gazes

            ' locate segment in which the current gaze falls
            Dim currentSegment As StimulusSegment = Nothing
            For Each s As StimulusSegment In stimulusSegments
                If pog.time >= s.StartMs And pog.time <= s.EndMs Then
                    currentSegment = s
                    Exit For
                End If
            Next
            If currentSegment Is Nothing Then
                Continue For
            End If

            ' check whether to start a new fixation object or continue the last one
            If currentSegment IsNot lastSegment And lastSegment IsNot Nothing Then
                If currentFixation.Duration >= minFixationDuration Then
                    addFixationToSegment(currentFixation, lastSegment, results)
                End If
                currentFixation = New Fixation(New Point(pog.x, pog.y))
                currentFixation.StartMs = pog.time
            ElseIf currentFixation Is Nothing Then
                currentFixation = New Fixation(New Point(pog.x, pog.y))
                currentFixation.StartMs = pog.time
            ElseIf Not (currentFixation.Point.X = pog.x And currentFixation.Point.Y = pog.y) Then
                If currentFixation.Duration >= minFixationDuration Then
                    addFixationToSegment(currentFixation, currentSegment, results)
                End If
                currentFixation = New Fixation(New Point(pog.x, pog.y))
                currentFixation.StartMs = pog.time
            Else
                currentFixation.EndMs = pog.time
            End If

            lastSegment = currentSegment
        Next
        If currentFixation.Duration >= minFixationDuration Then
            addFixationToSegment(currentFixation, lastSegment, results)
        End If

        Return results
    End Function

    ''' <summary>
    ''' Finds the AOI in which the fixation falls and adds it to the results.
    ''' </summary>
    Private Sub addFixationToSegment(ByVal fixation As Fixation, _
                                     ByVal segment As StimulusSegment, _
                                     ByVal results As ProcessingResults)
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
    End Sub
End Class
