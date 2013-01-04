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

    ''' <summary>
    ''' Adds the specified list of fixation points corresponding to a single subject.
    ''' </summary>
    Public Sub addSubjectFixations(ByVal fixationPoints As List(Of FixationPoint))

    End Sub
End Class
