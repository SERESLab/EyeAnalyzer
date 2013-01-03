''' <summary>
''' Form for constructing and exporting heatmaps from fixations.
''' </summary>
Public Class HeatmapForm

    Private Structure HeatmapFixationPoint
        Public x As Integer
        Public y As Integer
        Public strength As Single
    End Structure

    Private _heatmaps As New List(Of Heatmap)
    Private _heatmapNameDictionary As New Dictionary(Of String, Heatmap)
    Private _lastDirImport As String = Nothing

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub HeatmapForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        writeStatusMessage("No fixations loaded.")
    End Sub

    Private Sub ImportFixationLocationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportFixationLocationsToolStripMenuItem.Click
        If _lastDirImport IsNot Nothing Then
            MainOpenFileDialog.InitialDirectory = _lastDirImport
        End If
        MainOpenFileDialog.Multiselect = True
        MainOpenFileDialog.Title = "Import fixation locations"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "XML (*.xml)|*.xml"
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            _lastDirImport = MainOpenFileDialog.FileName.Substring(0, MainSaveFileDialog.FileName.LastIndexOf("\") + 1)
            Try
                importFixationLocations(MainOpenFileDialog.FileNames)
            Catch ex As Exception
                writeStatusMessage("***Error importing fixation locations: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub importFixationLocations(ByVal filenames As String())
        For Each filename As String In filenames
            Dim fixationCount As Integer = 0
            Dim fixations As New Stack(Of List(Of HeatmapFixationPoint))
            Dim stimulusNames As New Stack(Of String)
            Dim stimulusDurations As New Stack(Of ULong)

            Using reader = New System.Xml.XmlTextReader(filename)
                While reader.Read()
                    If reader.IsStartElement("FixationPoint") Then
                        Dim xStr As String = reader.GetAttribute("x")
                        Dim yStr As String = reader.GetAttribute("y")
                        Dim durationStr As String = reader.GetAttribute("duration")
                        If xStr Is Nothing Or yStr Is Nothing Or durationStr Is Nothing Then
                            Throw New Exception("Bad format.")
                        End If
                        Dim point As New HeatmapFixationPoint
                        point.x = Integer.Parse(xStr)
                        point.y = Integer.Parse(yStr)
                        point.strength = Single.Parse(durationStr) / stimulusDurations.Peek
                        fixations.Peek().Add(point)
                        fixationCount += 1

                    ElseIf reader.IsStartElement("StimulusSegment") Then
                        Dim name As String = reader.GetAttribute("name")
                        Dim durationStr As String = reader.GetAttribute("duration")
                        If name Is Nothing Or durationStr Is Nothing Then
                            Throw New Exception("Bad format.")
                        End If
                        stimulusDurations.Push(ULong.Parse(durationStr))
                        stimulusNames.Push(name)
                        fixations.Push(New List(Of HeatmapFixationPoint))
                    End If
                End While
            End Using

            writeStatusMessage("Imported " & fixationCount & " total fixations on " & stimulusNames.Count & " stimuli from " & filename & ".")
        Next
    End Sub

    Private Sub writeStatusMessage(ByVal msg As String)
        Dim b As System.Text.StringBuilder = New System.Text.StringBuilder(StatusTextBox.Text)
        b.AppendLine(msg)
        StatusTextBox.Text = b.ToString()
    End Sub

    Private Sub clearStatus()
        StatusTextBox.Text = Nothing
    End Sub
End Class