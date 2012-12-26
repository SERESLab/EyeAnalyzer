''' <summary>
''' Models a segment of a recording session with a collection of Area of
''' Interest regions.
''' </summary>
Public Class StimulusSegment
    Implements IComparable(Of StimulusSegment)

    Private _name As String = ""
    Private _startMs? As ULong = Nothing
    Private _endMs? As ULong = Nothing

    ''' <summary>
    ''' Gets or sets the name of the segment.
    ''' </summary>
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the start time of the segment, in milliseconds.
    ''' Mutable.
    ''' </summary>
    Public Property StartMs As ULong?
        Get
            Return _startMs
        End Get
        Set(ByVal value? As ULong)
            _startMs = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the end time of the segment, in milliseconds.
    ''' Mutable.
    ''' </summary>
    Public Property EndMs As ULong?
        Get
            Return _endMs
        End Get
        Set(ByVal value? As ULong)
            _endMs = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the duration of the segment, in milliseconds.
    ''' </summary>
    Public ReadOnly Property DurationMs As ULong
        Get
            If _startMs Is Nothing Or _endMs Is Nothing Then
                Return 0
            End If
            Return _endMs - _startMs
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim startStr As String = ""
        Dim endStr As String = ""
        If _startMs IsNot Nothing Then
            startStr = makeTimeString(_startMs)
        End If
        If _endMs IsNot Nothing Then
            endStr = makeTimeString(_endMs)
        End If
        Return _name & " (" & startStr & " - " & endStr & ")"
    End Function

    ''' <summary>
    ''' Formats the specified time in milliseconds to a string of the
    ''' format HH:MM:SS
    ''' </summary>
    Private Function makeTimeString(ByVal timeMs As ULong) As String
        Dim hours As Integer = timeMs \ 3600000L
        timeMs -= hours * 3600000L
        Dim minutes As Integer = timeMs \ 60000L
        timeMs -= minutes * 60000L
        Dim seconds As Integer = timeMs \ 1000L
        Return hours.ToString("D2") & ":" & minutes.ToString("D2") & ":" & _
            seconds.ToString("D2")
    End Function

    ''' <summary>
    ''' Compares the start time of this segment to that of another segment.
    ''' </summary>
    Public Function CompareTo(ByVal other As StimulusSegment) As Integer _
        Implements IComparable(Of StimulusSegment).CompareTo
        Dim thisStart As Integer = _startMs
        Dim otherStart As Integer = other.StartMs
        Return thisStart - otherStart
    End Function
End Class
