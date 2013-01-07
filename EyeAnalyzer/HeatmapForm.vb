''' <summary>
''' Form for constructing and exporting heatmaps from fixations.
''' </summary>
Public Class HeatmapForm

    Private _defaultRadius As Integer = 6
    Private _defaultOpacity As Integer = 255
    Private _defaultLowColor As Color = Color.Green
    Private _defaultMidColor As Color = Color.Yellow
    Private _defaultHighColor As Color = Color.Red

    Private _heatmapNameDictionary As New Dictionary(Of String, Heatmap)
    Private _lastDirImport As String = Nothing
    Private _lastDirStimulusImage As String = Nothing
    Private _clearStatusOnNextUpdate As Boolean

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub HeatmapForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        writeStatusMessage("No fixations loaded.")
        _clearStatusOnNextUpdate = True
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
            _lastDirImport = MainOpenFileDialog.FileName.Substring(0, MainOpenFileDialog.FileName.LastIndexOf("\") + 1)
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
            Dim stimulusCount As Integer = 0
            Dim fixations As New Stack(Of List(Of Heatmap.FixationPoint))
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
                        Dim point As New Heatmap.FixationPoint
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
                        fixations.Push(New List(Of Heatmap.FixationPoint))
                    End If
                End While
            End Using

            While fixations.Count > 0
                Dim fixationsList As List(Of Heatmap.FixationPoint) = fixations.Pop()
                Dim stimulusName As String = stimulusNames.Pop()
                Dim heatmap As Heatmap

                If _heatmapNameDictionary.ContainsKey(stimulusName) Then
                    heatmap = _heatmapNameDictionary.Item(stimulusName)
                Else
                    heatmap = New Heatmap(stimulusName, _defaultRadius, _defaultOpacity, _defaultLowColor, _
                                          _defaultMidColor, _defaultHighColor)
                    _heatmapNameDictionary.Add(stimulusName, heatmap)
                    HeatmapsListBox.Items.Add(heatmap)
                End If
                If fixationsList.Count > 0 Then
                    heatmap.addSubjectFixations(fixationsList)
                    stimulusCount += 1
                End If
            End While
            stimulusDurations.Clear()

            If HeatmapsListBox.SelectedItem IsNot Nothing Then
                Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
                NumberOfSubjectsTextBox.Text = heatmap.SubjectCount
                TotalFixationsTextBox.Text = heatmap.FixationCount
                StimulusImageTextBox.Text = heatmap.StimulusImageFilename
                HeatmapPictureBox.Image = Nothing
                HeatmapPictureBox.Image = heatmap.Image
            End If

            HeatmapsListBox.Refresh()
            writeStatusMessage("Imported " & fixationCount & " total fixations on " & stimulusCount & " stimuli from " & filename & ".")
            ClearFixationLocationsToolStripMenuItem.Enabled = (fixationCount > 0)
            ExportHeatmapsToolStripMenuItem.Enabled = (fixationCount > 0)
            SelectAllButton.Enabled = (fixationCount > 0)
            SelectNoneButton.Enabled = (fixationCount > 0)
        Next
    End Sub

    Private Sub writeStatusMessage(ByVal msg As String)
        If _clearStatusOnNextUpdate Then
            clearStatus()
            _clearStatusOnNextUpdate = False
        End If
        Dim b As System.Text.StringBuilder = New System.Text.StringBuilder(StatusTextBox.Text)
        b.AppendLine(msg)
        StatusTextBox.Text = b.ToString()
    End Sub

    Private Sub clearStatus()
        StatusTextBox.Text = Nothing
    End Sub

    Private Sub ClearFixationLocationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearFixationLocationsToolStripMenuItem.Click

        If MessageBox.Show("Are you sure you would like to clear all fixations?", "Clear fixations", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.No Then
            Return
        End If

        SelectAllButton.Enabled = False
        SelectNoneButton.Enabled = False
        HeatmapsListBox.ClearSelected()
        HeatmapsListBox.Items.Clear()
        HeatmapsListBox.Refresh()
        _heatmapNameDictionary.Clear()
        ClearFixationLocationsToolStripMenuItem.Enabled = False
        ExportHeatmapsToolStripMenuItem.Enabled = False
        clearStatus()
        writeStatusMessage("Cleared all fixations.")
        _clearStatusOnNextUpdate = True
    End Sub

    Private Sub HeatmapsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeatmapsListBox.SelectedIndexChanged
        If HeatmapsListBox.SelectedItem IsNot Nothing Then
            Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
            HeatmapGroupBox.Enabled = True
            NumberOfSubjectsTextBox.Text = heatmap.SubjectCount
            TotalFixationsTextBox.Text = heatmap.FixationCount
            StimulusImageTextBox.Text = heatmap.StimulusImageFilename
            HeatmapPictureBox.Image = heatmap.Image
            LowFixationsColorLabel.BackColor = heatmap.LowFixationsColor
            MidFixationsColorLabel.BackColor = heatmap.MidFixationsColor
            HighFixationsColorLabel.BackColor = heatmap.HighFixationsColor
            RadiusNumericUpDown.Value = heatmap.FixationRadius
            AlphaTrackBar.Value = heatmap.FixationAlpha
        Else
            LowFixationsColorLabel.BackColor = Color.Black
            MidFixationsColorLabel.BackColor = Color.Black
            HighFixationsColorLabel.BackColor = Color.Black
            RadiusNumericUpDown.Value = 2
            AlphaTrackBar.Value = 255
            NumberOfSubjectsTextBox.Text = ""
            TotalFixationsTextBox.Text = ""
            StimulusImageTextBox.Text = ""
            HeatmapPictureBox.Image = Nothing
            HeatmapGroupBox.Enabled = False
        End If
    End Sub

    Private Sub ClearStimulusImageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearStimulusImageButton.Click
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        heatmap.clearStimulusImage()
        StimulusImageTextBox.Text = heatmap.StimulusImageFilename
        HeatmapPictureBox.Image = Nothing
        HeatmapPictureBox.Image = heatmap.Image
        HeatmapsListBox.Refresh()
    End Sub

    Private Sub LoadStimulusImageButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadStimulusImageButton.Click
        If _lastDirStimulusImage IsNot Nothing Then
            MainOpenFileDialog.InitialDirectory = _lastDirStimulusImage
        End If
        MainOpenFileDialog.Multiselect = False
        MainOpenFileDialog.Title = "Load stimulus image"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG;*.TIFF"
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            _lastDirStimulusImage = MainOpenFileDialog.FileName.Substring(0, MainOpenFileDialog.FileName.LastIndexOf("\") + 1)
            Try
                Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
                heatmap.loadStimulusImage(MainOpenFileDialog.FileName)
                StimulusImageTextBox.Text = heatmap.StimulusImageFilename
                HeatmapPictureBox.Image = Nothing
                HeatmapPictureBox.Image = heatmap.Image
                HeatmapsListBox.Refresh()
            Catch ex As Exception
                MessageBox.Show("An error occurred while loading the stimulus image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ExportHeatmapsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportHeatmapsToolStripMenuItem.Click
        For Each heatmap As Heatmap In HeatmapsListBox.CheckedItems
            MainSaveFileDialog.Title = "Save " & heatmap.StimulusName & " heatmap"
            MainSaveFileDialog.FileName = heatmap.StimulusName & "-Heatmap"
            MainSaveFileDialog.Filter = "PNG (*.png)|*.png"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                heatmap.Image.Save(MainSaveFileDialog.FileName, Imaging.ImageFormat.Png)
                writeStatusMessage("Exported heatmap image for " & heatmap.StimulusName & " to " & MainSaveFileDialog.FileName & ".")
            End If
        Next
    End Sub

    Private Sub SelectAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllButton.Click
        For i As Integer = 0 To HeatmapsListBox.Items.Count - 1
            HeatmapsListBox.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub SelectNoneButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectNoneButton.Click
        For i As Integer = 0 To HeatmapsListBox.Items.Count - 1
            HeatmapsListBox.SetItemChecked(i, False)
        Next
    End Sub

    Private Sub AlphaTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlphaTrackBar.Scroll
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        If heatmap IsNot Nothing Then
            heatmap.FixationAlpha = AlphaTrackBar.Value
            HeatmapPictureBox.Image = Nothing
            HeatmapPictureBox.Image = heatmap.Image
        End If
    End Sub

    Private Sub RadiusNumericUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadiusNumericUpDown.ValueChanged
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        If heatmap IsNot Nothing Then
            heatmap.FixationRadius = RadiusNumericUpDown.Value
            HeatmapPictureBox.Image = Nothing
            HeatmapPictureBox.Image = heatmap.Image
        End If
    End Sub

    Private Sub LowFixationsColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowFixationsColorLabel.Click
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        If heatmap IsNot Nothing Then
            MainColorDialog.Color = LowFixationsColorLabel.BackColor
            If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                heatmap.LowFixationsColor = MainColorDialog.Color
                HeatmapPictureBox.Image = Nothing
                HeatmapPictureBox.Image = heatmap.Image
                LowFixationsColorLabel.BackColor = MainColorDialog.Color
            End If
        End If
    End Sub

    Private Sub MidFixationsColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MidFixationsColorLabel.Click
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        If heatmap IsNot Nothing Then
            MainColorDialog.Color = MidFixationsColorLabel.BackColor
            If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                heatmap.MidFixationsColor = MainColorDialog.Color
                HeatmapPictureBox.Image = Nothing
                HeatmapPictureBox.Image = heatmap.Image
                MidFixationsColorLabel.BackColor = MainColorDialog.Color
            End If
        End If
    End Sub

    Private Sub HighFixationsColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighFixationsColorLabel.Click
        Dim heatmap As Heatmap = HeatmapsListBox.SelectedItem
        If heatmap IsNot Nothing Then
            MainColorDialog.Color = HighFixationsColorLabel.BackColor
            If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                heatmap.HighFixationsColor = MainColorDialog.Color
                HeatmapPictureBox.Image = Nothing
                HeatmapPictureBox.Image = heatmap.Image
                HighFixationsColorLabel.BackColor = MainColorDialog.Color
            End If
        End If
    End Sub
End Class