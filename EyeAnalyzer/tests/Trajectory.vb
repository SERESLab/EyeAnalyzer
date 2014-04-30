Imports System.Drawing
Imports System.Xml
Imports System.Data

Public Class Trajectory
    Dim gaze As New GazePoints
    Dim tc As New TrajectoryClass
    Private _sortedList As New List(Of GazePoints)

    Public Sub New(ByVal ls As List(Of GazePoints))
        _sortedList = ls
    End Sub


    Public Structure TrajStruct
        Dim startPoint As Point
        Dim endPoint As Point
        Dim startAOI As String
        Dim endAOI As String
    End Structure


    Public Function GetTrajectory() As List(Of TrajStruct)

        Dim Slist As New List(Of GazePoints)
        Slist = _sortedList
        Dim trajList As New List(Of TrajStruct)
        Dim listCount As Integer = _sortedList.Count
        Dim i As Integer = 0
        Dim j As Integer = 0

        While listCount > 0

            Dim t As New TrajStruct

            If listCount = 1 Then
                t.startPoint = Slist(1).gpoint
                t.endPoint = Slist(1).gpoint
                t.startAOI = Slist(1).aoi
                t.endAOI = Slist(1).aoi

            ElseIf listCount Then
                j = i + 1
                t.startPoint = Slist(i).gpoint
                t.endPoint = Slist(j).gpoint
                t.startAOI = Slist(i).aoi
                t.endAOI = Slist(j).aoi
            End If
            trajList.Add(t)
            listCount -= 1
            i += 1
        End While

        Return trajList
    End Function

    Dim lst() As Point

    Public Function DrawTrajectory() As Bitmap

        'FURTHER PROCESSING SHOULD BE DONE HERE ABOUT AOI LOCATION
        ' Create a new custom pen. 
        Dim listTraj As New List(Of TrajStruct)
        listTraj = GetTrajectory()
        'Dim picturebox2 As New PictureBox
        Dim redPen As New Pen(Brushes.Red, 1.0F)
        Dim tbmp As New Bitmap(588, 410)

        Dim gfx As Drawing.Graphics = Drawing.Graphics.FromImage(tbmp)
        ' Set the StartCap property.
        redPen.StartCap = Drawing2D.LineCap.Triangle
        ' Set the EndCap property.
        redPen.EndCap = Drawing2D.LineCap.ArrowAnchor
        Dim k As Integer = listTraj.Count
        Dim i As Integer = 0
        While k > 0
            'gfx.DrawEllipse(redPen, Lst())
            gfx.DrawLine(redPen, listTraj(i).startPoint, listTraj(i).endPoint)
            i += 1
            k -= 1
        End While

        Return tbmp

    End Function


End Class
