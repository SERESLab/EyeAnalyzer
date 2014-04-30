
Imports System.Drawing
Public Class TrajClass
    Private _startPoint As Point
    Private _endPoint As Point
    Private _name As String

    Public Sub New(ByVal startp As Point, ByVal endp As Point, name As String)
        _startPoint = startp
        _endPoint = endp
        _name = name
    End Sub
End Class
