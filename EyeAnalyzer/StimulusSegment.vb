''' <summary>
''' Models a segment of a recording session with a collection of Area of
''' Interest regions.
''' </summary>
Public Class StimulusSegment

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
End Class
