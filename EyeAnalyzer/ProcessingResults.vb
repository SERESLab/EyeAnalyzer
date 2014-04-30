''' <summary>
''' Holds and saves the results of processing eye tracking data.
''' </summary>
Public Class ProcessingResults

    'Declaration of private viriables

    Private _averageCalibrationError As Single = 0.0
    Private _validCalibrationPoints As Integer = 0
    Private _fixations As New Dictionary(Of String, Dictionary(Of String, List(Of Fixation))) '_fixations holds collection of fixation points
    Private _segmentDurations As New Dictionary(Of String, ULong) 'holds collection of _segmentDurations: key-value pair

    ''' <summary>
    ''' Constructs an empty results object with the specified calibration error
    ''' information.
    ''' </summary>
    Public Sub New(ByVal avgCalibrationError As Single, ByVal validCalibrationPoints As Integer)
        _averageCalibrationError = avgCalibrationError
        _validCalibrationPoints = validCalibrationPoints
    End Sub

    ''' <summary>
    ''' Creates a new segment for AOIs to be added.
    ''' </summary>
    Public Sub createSegment(ByVal name As String, ByVal durationMs As ULong)
        _segmentDurations.Add(name, durationMs)
        _fixations.Add(name, New Dictionary(Of String, List(Of Fixation)))
    End Sub

    ''' <summary>
    ''' Creates a new AOI in the specified segment for fixations to be added.
    ''' </summary>
    Public Sub createAOI(ByVal inSegment As String, ByVal name As String)
        _fixations.Item(inSegment).Add(name, New List(Of Fixation))
    End Sub

    ''' <summary>
    ''' Adds the specified fixation object to the specified segment and AOI.
    ''' </summary>
    Public Sub addFixation(ByVal inSegment As String, ByVal inAoi As String, ByVal fixation As Fixation)
        _fixations.Item(inSegment).Item(inAoi).Add(fixation)
    End Sub

    ''' <summary>
    ''' Writes fixation locations in XML format to the specified filename.
    ''' </summary>
    Public Sub saveFixationLocations(ByVal filename As String)
        Using writer = New System.Xml.XmlTextWriter(filename, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = System.Xml.Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("StimulusSegments")
            For Each kvpDurations As KeyValuePair(Of String, ULong) In _segmentDurations
                writer.WriteStartElement("StimulusSegment")
                writer.WriteAttributeString("name", kvpDurations.Key)
                writer.WriteAttributeString("duration", kvpDurations.Value.ToString())
                For Each kvpFixations As KeyValuePair(Of String, List(Of Fixation)) In _fixations.Item(kvpDurations.Key)
                    Dim aoiName As String = kvpFixations.Key
                    For Each f As Fixation In kvpFixations.Value
                        writer.WriteStartElement("FixationPoint")
                        writer.WriteAttributeString("Count", f.Count.ToString())
                        writer.WriteAttributeString("x", f.Point.X.ToString())
                        writer.WriteAttributeString("y", f.Point.Y.ToString())
                        writer.WriteAttributeString("start", f.StartMs.ToString())
                        writer.WriteAttributeString("duration", f.Duration.ToString())
                        writer.WriteAttributeString("aoi", aoiName)
                        writer.WriteEndElement()
                    Next
                Next
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
        End Using
    End Sub

    ''' <summary>
    ''' Writes the specified fixation measurements (counts, durations) in CSV
    ''' format to the specified filename.
    ''' </summary>
    Public Sub saveFixationMeasurements(ByVal saveCounts As Boolean, ByVal saveDurations As Boolean, _
                                        ByVal filename As String)
        Dim counts As New Queue(Of Integer)
        Dim durations As New Queue(Of ULong)

        Using writer As New System.IO.StreamWriter(filename)

            ' write column headers and measure counts/durations
            writer.Write("StimulusSegment-AOI")
            For Each kvpSegments As KeyValuePair(Of String, Dictionary(Of String, List(Of Fixation))) In _fixations
                Dim segmentName As String = kvpSegments.Key
                For Each kvpAois As KeyValuePair(Of String, List(Of Fixation)) In kvpSegments.Value
                    Dim aoiName As String = kvpAois.Key
                    writer.Write("," & segmentName & "-" & aoiName)
                    counts.Enqueue(kvpAois.Value.Count)
                    Dim duration As ULong = 0
                    For Each f As Fixation In kvpAois.Value
                        duration += f.Duration
                    Next
                    durations.Enqueue(duration)
                Next
            Next
            writer.WriteLine()

            If saveCounts Then
                writer.Write("Count")
                For Each c As Integer In counts
                    writer.Write("," & c.ToString())
                Next
                writer.WriteLine()
            End If

            If saveDurations Then
                writer.Write("DurationMS")
                For Each d As ULong In durations
                    writer.Write("," & d.ToString())
                Next
                writer.WriteLine()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Writes segment durations in CSV format to the specified filename.
    ''' </summary>
    Public Sub saveSegmentDurations(ByVal filename As String)
        Using writer As New System.IO.StreamWriter(filename)
            writer.Write("StimulusSegment")
            For Each kvp As KeyValuePair(Of String, ULong) In _segmentDurations
                writer.Write("," & kvp.Key)
            Next
            writer.WriteLine()
            writer.Write("DurationMS")
            For Each kvp As KeyValuePair(Of String, ULong) In _segmentDurations
                writer.Write("," & kvp.Value.ToString())
            Next
            writer.WriteLine()
        End Using
    End Sub

    ''' <summary>
    ''' Writes calibration error data in CSV format to the specified filename.
    ''' </summary>
    Public Sub saveCalibrationError(ByVal filename As String)
        Using writer As New System.IO.StreamWriter(filename)
            writer.WriteLine("AverageCalibrationError," & _averageCalibrationError.ToString())
            writer.WriteLine("ValidCalibrationPoints," & _validCalibrationPoints.ToString())
        End Using
    End Sub
End Class
