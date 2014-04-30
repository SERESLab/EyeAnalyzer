
Imports System.Xml
Imports System.Text
Imports System.IO
Imports System.Drawing

Public Class VisualizationClass

    Dim _WIDTH As Integer = 1920
    Dim _HEIGHT As Integer = 1080
    Dim swidth As Integer = 567
    Dim sheight As Integer = 425

    Private _filename As String ' xml full filename

    Property Gefilename() As String
        Get
            Return _filename
        End Get
        Set(value As String)
            _filename = value
        End Set
    End Property

    Dim _segment As New List(Of String)
    Dim GPlist As New List(Of GazePoints)

    Public Property SegmentList() As List(Of String)
        Get
            Return _segment
        End Get
        Set(value As List(Of String))
            _segment = value
        End Set
    End Property


    Public Structure Trajectory
        Dim startPoint As Point
        Dim endPoint As Point
        Dim startname As String
        Dim endname As String
    End Structure


    Public Function GetGPFromFile() As List(Of GazePoints)

        ' Dim ss As New StimulusSegment
        Dim list As New List(Of GazePoints)

        
        _segment.Clear()

        Try
            If _filename = Nothing Then
                list = Nothing
                Return list
                Exit Function
            End If

            Dim rd As XmlTextReader = New XmlTextReader(_filename)
            If rd.IsStartElement("FixationPoint") Then
                If rd.GetAttribute("x") = "" Or rd.GetAttribute("y") = "" Or rd.GetAttribute("start") = "" Or rd.GetAttribute("duration") = "" Then
                    MessageBox.Show("Wrong data, check and try again")
                    _segment = Nothing
                    Return Nothing
                    Exit Function
                End If
            End If
            Dim s As String = ""
            While rd.Read()
                Dim gp As New GazePoints
                Dim x As Integer
                Dim y As Integer

                If rd.IsStartElement("StimulusSegment") Then
                    s = rd.GetAttribute("name")
                    _segment.Add(s)
                End If
                If rd.IsStartElement("FixationPoint") Then

                    x = Integer.Parse(rd.GetAttribute("x")) * swidth / _WIDTH
                    y = Integer.Parse(rd.GetAttribute("y")) * sheight / _HEIGHT

                    gp.aoi = rd.GetAttribute("aoi")
                    gp.gpoint = New Point(x, y)
                    gp.segment = s
                    gp.stime = Integer.Parse(rd.GetAttribute("start"))
                    gp.duration = Integer.Parse(rd.GetAttribute("duration"))
                    list.Add(gp)
                End If

            End While
        Catch ex As Exception
            Dim r As DialogResult = MessageBox.Show(ex.Message, "Incorrect File", MessageBoxButtons.OK, MessageBoxIcon.Error)

            If r = DialogResult.OK Then
                MessageBox.Show("Please check your input data and make sure it contains: x, y, start and duration." _
                                 & vbCrLf & "Those parameters are used to track gaze sequences.", "Wrong Data", MessageBoxButtons.OK)

                list = Nothing
                _segment.Clear()
            End If

        End Try

        Return list  'Returns list of points with with their stimulus segment
    End Function

    'get lis by segment

    Public Function GetStimulusSegment(ByVal segment As String, GPL As List(Of GazePoints)) As List(Of GazePoints)

        Dim l As New List(Of GazePoints)
        Dim query = From glist As GazePoints In GPL
                   Where glist.segment = segment
                   Select glist

        For Each gp As GazePoints In query
            Dim p As New GazePoints
            l.Add(gp)
        Next

        Dim sortedFixations = (From q In l Order By q.stime Select q).ToList

        Return sortedFixations
    End Function




End Class
