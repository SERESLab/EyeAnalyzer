''' <summary>
''' Represents a collection of data measured by an eye tracker.
''' </summary>
Public Class EyeTrackerData

    ''' <summary>
    ''' Creates a new eye tracker data object from the specified file.
    ''' </summary>
    Public Shared Function FromFile(ByVal filename As String) As EyeTrackerData
        Return New EyeTrackerData(filename)
    End Function

    ''' <summary>
    ''' Private constructor.
    ''' </summary>
    Private Sub New(ByVal filename As String)

    End Sub

    ''' <summary>
    ''' Destructor.
    ''' </summary>
    Protected Overrides Sub Finalize()

    End Sub

End Class
