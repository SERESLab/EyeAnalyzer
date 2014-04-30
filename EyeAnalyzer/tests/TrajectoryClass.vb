
Imports System.Drawing.Drawing2D
Imports System.Drawing.Graphics
Imports System.Xml
Imports System.Text

Public Class TrajectoryClass
    Private _filename As String
    Private _segment As String
    Private _listOfFixation As List(Of Fixation)
  

    Public Property GetFileName() As String
        Get
            Return _filename
        End Get
        Set(value As String)
            _filename = value
        End Set
    End Property

  
    Public Structure Fixation
        Dim x As Integer
        Dim y As Integer
        Dim aoi As String
        Dim st_time As Integer
    End Structure

    Dim swidth As Integer = 588
    Dim sheight As Integer = 410

    Dim _WIDTH As Integer = 1920
    Dim _HEIGHT As Integer = 1080

    Public Function GetFixations(ByVal filename As String) As List(Of GazePoints)
        Dim list As New List(Of GazePoints)
        Dim reader As XmlTextReader = New XmlTextReader(filename)

        While reader.Read()
            Dim gz As New GazePoints
            If reader.IsStartElement("StimulusSegment") Then
               
            ElseIf reader.IsStartElement("FixationPoint") Then
                Dim f As Fixation

                f.x = Integer.Parse(reader.GetAttribute("x")) * swidth / _WIDTH
                f.y = Integer.Parse(reader.GetAttribute("y")) * sheight / _HEIGHT
                f.st_time = Integer.Parse(reader.GetAttribute("start"))
                f.aoi = reader.GetAttribute("aoi").ToString
                gz.gpoint = New Point(f.x, f.y)
                gz.aoi = f.aoi
                gz.stime = f.st_time
                list.Add(gz)
            End If

        End While


        Dim sortedFixations = (From q In list Order By q.stime Select q).ToList
        Return sortedFixations
    End Function

End Class
