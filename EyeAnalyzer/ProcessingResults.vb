''' <summary>
''' Represents the results of processing eye tracking data.
''' </summary>
Public Class ProcessingResults

    ''' <summary>
    ''' Writes fixation locations in XML format to the specified filename.
    ''' </summary>
    Public Sub saveFixationLocations(ByVal filename As String)

    End Sub

    ''' <summary>
    ''' Writes the specified fixation measurements (counts, durations) in CSV
    ''' format to the specified filename.
    ''' </summary>
    Public Sub saveFixationMeasurements(ByVal saveCounts As Boolean, ByVal saveDurations As Boolean, _
                                        ByVal filename As String)

    End Sub

    ''' <summary>
    ''' Writes segment durations in CSV format to the specified filename.
    ''' </summary>
    Public Sub saveSegmentDurations(ByVal filename As String)

    End Sub

    ''' <summary>
    ''' Writes calibration error data in CSV format to the specified filename.
    ''' </summary>
    Public Sub saveCalibrationError(ByVal filename As String)

    End Sub
End Class
