''' <summary>
''' Represents a named region of the screen that is of
''' particular interest.
''' </summary>
Public Class AreaOfInterest

    Private _rect As Rectangle
    Private _name As String
    Private _isNonExclusive As Boolean = False

    ''' <summary>
    ''' Gets or sets the name of the AOI.
    ''' </summary>
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                Throw New Exception("Name must have a value.")
            End If
            If value.Trim.Length = 0 Then
                Throw New Exception("Name must have a value.")
            End If
            _name = value.Trim
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the screen area rectangle.
    ''' </summary>
    Public Property Area As Rectangle
        Get
            Return _rect
        End Get
        Set(ByVal value As Rectangle)
            _rect = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the AOI is non-exclusive. If true, fixation points will
    ''' be measured even if they fall within another AOI.
    ''' </summary>
    Public Property NonExclusive As Boolean
        Get
            Return _isNonExclusive
        End Get
        Set(ByVal value As Boolean)
            _isNonExclusive = value
        End Set
    End Property

    ''' <summary>
    ''' Constructs a new area of interest with the given name and
    ''' rectangle region.
    ''' </summary>
    Public Sub New(ByVal name As String, ByVal area As Rectangle)
        Me.Name = name
        Me.Area = area
    End Sub

    Public Overrides Function ToString() As String
        Return _name
    End Function
End Class
