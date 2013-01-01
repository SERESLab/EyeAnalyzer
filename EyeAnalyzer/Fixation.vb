''' <summary>
''' Represents an eye fixation on some stimulus.
''' </summary>
Public Class Fixation

    Private _startMs As ULong = 0
    Private _endMs As ULong = 0
    Private _point As Point

    ''' <summary>
    ''' Gets or sets the start of the fixation in milliseconds.
    ''' </summary>
    Public Property StartMs As ULong
        Get
            Return _startMs
        End Get
        Set(ByVal value As ULong)
            _startMs = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the end of the fixation in milliseconds.
    ''' </summary>
    Public Property EndMs As ULong
        Get
            Return _endMs
        End Get
        Set(ByVal value As ULong)
            _endMs = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the point of fixation.
    ''' </summary>
    Public Property Point As Point
        Get
            Return _point
        End Get
        Set(ByVal value As Point)
            _point = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the duration of the fixation in milliseconds.
    ''' </summary>
    Public ReadOnly Property Duration As ULong
        Get
            If _endMs > _startMs Then
                Return _endMs - _startMs
            End If
            Return 0
        End Get
    End Property

    ''' <summary>
    ''' Constructs a new fixation point at the specified location.
    ''' </summary>
    Public Sub New(ByVal location As Point)
        _point = location
    End Sub
End Class
