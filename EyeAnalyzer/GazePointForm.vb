Imports System.Xml

Public Class GazePointForm
    Dim g1 As Drawing.Graphics
    Dim g2 As Drawing.Graphics

    Dim swidth As Integer = 567
    Dim sheight As Integer = 398

    Dim _W As Integer = 1920
    Dim _H As Integer = 1080

    Dim xmlFileFullName As String = Nothing
    Dim xmlfileName As String = Nothing
    Dim _lastDirData As String = Nothing

    Private Sub GazePointForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
       
    End Sub

    Public Function LoadData() As List(Of Point)

        Dim plist As New List(Of Point)
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Title = "Select eye-tracker data"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Processed data (*.xml)|*.xml"


        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            xmlFileFullName = MainOpenFileDialog.FileName
            xmlfileName = MainOpenFileDialog.SafeFileName
            _lastDirData = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
        End If

        If xmlFileFullName IsNot Nothing Then

            Dim reader As XmlTextReader = New XmlTextReader(xmlFileFullName)
            Dim p As Point
            While reader.Read()

                If reader.IsStartElement("FixationPoint") Then
                    Dim x As Integer = Integer.Parse(reader.GetAttribute("x")) * swidth / _W
                    Dim y As Integer = Integer.Parse(reader.GetAttribute("y")) * sheight / _H
                    p = New Point(x, y)
                End If

                plist.Add(p)
            End While

        Else

            plist = Nothing
            MessageBox.Show("No data loaded")
        End If
        Return plist
    End Function

    Public Function drawPoint(x As Integer, y As Integer) As Rectangle
        Return New Rectangle(x, y, 10, 10)
    End Function

    Public Sub DrawLine(list As List(Of Point))
        g1 = Panel1.CreateGraphics()
        g2 = Panel1.CreateGraphics()
        Dim redPen As New Pen(Color.Red)

        ' Set the StartCap property.
        redPen.StartCap = Drawing2D.LineCap.RoundAnchor

        ' Set the EndCap property.
        redPen.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim j As Integer = 0
        Dim lp(list.Count) As Point
        For Each p As Point In list
            'g1.FillEllipse(Brushes.Blue, drawPoint(p.X, p.Y))
            g1.DrawEllipse(Pens.Red, drawPoint(p.X - 12, p.Y - 2))
            lp(j) = New Point(p.X - 8, p.Y + 3)

            j += 1
        Next
        g2.DrawLines(redPen, lp)

    End Sub

    Dim tc As New TrajectoryClass
    Dim list As New List(Of GazePoints)


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim plist As New List(Of Point)
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Title = "Select eye-tracker data"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Processed data (*.xml)|*.xml"


        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            xmlFileFullName = MainOpenFileDialog.FileName
            xmlfileName = MainOpenFileDialog.SafeFileName
            _lastDirData = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
        End If
        list = tc.GetFixations(xmlFileFullName)

        Dim tj As New Trajectory(list)


        PictureBox1.Image = tj.DrawTrajectory()
        'Me.DrawLine(LoadData())

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Me.Panel1.BackgroundImage = Image.FromFile("C:\Users\abk\Documents\Visual Studio 2012\Projects\graphics\screenshot.png")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        
    End Sub
End Class