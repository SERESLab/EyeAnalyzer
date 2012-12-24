''' <summary>
''' Main form for measuring fixations. Allows the user to specify stimulus intervals
''' and AOIs and execute processing.
''' </summary>
Public Class MainForm

    Private Const MIN_FIXATION_DURATION_MS As Integer = 40

    Private _videoRecording As VideoRecording = Nothing
    Private _eyeTrackerData As EyeTrackerData = Nothing
    Private _didFrameChange As Boolean = False

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub GenerateNewHeatmapToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateNewHeatmapToolStripMenuItem.Click
        HeatmapForm.Show()
    End Sub

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FixationDurationTextBox.Text = MIN_FIXATION_DURATION_MS
        resetForm()
    End Sub

    Private Sub resetForm()
        RedrawTimer.Stop()
        VideoPositionUpDown.Value = 0
        VideoPositionTrackBar.Value = 0
        _videoRecording = Nothing
        _eyeTrackerData = Nothing
        VideoFileStatusLabel.Text = "-"
        XmlFileStatusLabel.Text = "-"
        IntervalStartTextBox.Text = ""
        IntervalEndTextBox.Text = ""
        VideoGroupBox.Enabled = False
        IntervalsGroupBox.Enabled = False
        AddIntervalButton.Enabled = False
        DeleteIntervalButton.Enabled = False
        AoiGroupBox.Enabled = False
        AddAoiButton.Enabled = False
        CopyAoiButton.Enabled = False
        DeleteAoiButton.Enabled = False
        ProcessFixationsButton.Enabled = False
        VideoPictureBox.Image = Nothing
        setStatusMessage("Ready for study data...")
    End Sub

    Private Sub setStatusMessage(ByVal msg As String)
        MainStatusLabel.Text = msg
    End Sub

    Private Sub OpenStudyDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenStudyDataToolStripMenuItem.Click
        If _videoRecording IsNot Nothing Or _eyeTrackerData IsNot Nothing Then
            If MessageBox.Show("Loading new data will clear all intervals and AOIs. Would you like to continue?", _
                               "Clear all data?", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
                resetForm()
            Else
                Return
            End If
        End If
        loadStudyData()
    End Sub

    Private Sub loadStudyData()
        Dim videoFileFullName As String = Nothing
        Dim xmlFileFullName As String = Nothing
        Dim videoFileName As String = Nothing
        Dim xmlFileName As String = Nothing

        MainOpenFileDialog.Multiselect = False
        MainOpenFileDialog.Title = "Select a video recording"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Video recording (*.avi)|*.avi"
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            videoFileFullName = MainOpenFileDialog.FileName
            videoFileName = MainOpenFileDialog.SafeFileName
        Else
            setStatusMessage("Study data not loaded.")
            Return
        End If

        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Title = "Select eye-tracker data"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Eye-tracker data (*.xml)|*.xml"
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            xmlFileFullName = MainOpenFileDialog.FileName
            xmlFileName = MainOpenFileDialog.SafeFileName
        End If

        If videoFileFullName IsNot Nothing And xmlFileFullName IsNot Nothing Then
            _videoRecording = VideoRecording.FromFile(videoFileFullName)
            VideoFileStatusLabel.Text = videoFileName
            _eyeTrackerData = EyeTrackerData.FromFile(xmlFileFullName)
            XmlFileStatusLabel.Text = xmlFileName
            VideoGroupBox.Enabled = True
            IntervalsGroupBox.Enabled = True
            setStatusMessage("Loaded study data.")
            VideoPositionUpDown.Maximum = _videoRecording.LengthMs
            VideoPositionUpDown.Increment = _videoRecording.TimeBetweenFramesMs
            _didFrameChange = True
            RedrawTimer.Start()
        Else
            setStatusMessage("Study data not loaded.")
        End If
    End Sub

    Private Sub VideoPositionUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionUpDown.ValueChanged
        _didFrameChange = True
        Dim percent As Single = CType(VideoPositionUpDown.Value, Single) / _videoRecording.LengthMs
        VideoPositionTrackBar.Value = percent * 100
    End Sub

    Private Sub VideoPositionTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionTrackBar.Scroll
        _didFrameChange = True
        Dim percent As Single = VideoPositionTrackBar.Value / 100.0
        VideoPositionUpDown.Value = Math.Round(percent * _videoRecording.LengthMs)
    End Sub

    Private Sub RedrawTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedrawTimer.Tick
        If _didFrameChange Then
            VideoPictureBox.Image = _videoRecording.drawFrameAtPosition(VideoPositionUpDown.Value, _
                                             VideoPictureBox.Width, VideoPictureBox.Height)
            _didFrameChange = False
        End If
    End Sub

    Private Sub SaveScreenshotButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveScreenshotButton.Click
        Using b = _videoRecording.drawFrameAtPosition(VideoPositionUpDown.Value, _
                                             _videoRecording.Width, _videoRecording.Height)
            MainSaveFileDialog.Title = "Save screenshot"
            MainSaveFileDialog.FileName = "Screenshot"
            MainSaveFileDialog.Filter = "PNG (*.png)|*.png"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                b.Save(MainSaveFileDialog.FileName, Imaging.ImageFormat.Png)
                setStatusMessage("Screenshot saved to " & MainSaveFileDialog.FileName & ".")
            End If
        End Using
    End Sub

    Private Sub IntervalStartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntervalStartButton.Click
        IntervalStartTextBox.Text = VideoPositionUpDown.Value
    End Sub

    Private Sub IntervalEndButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IntervalEndButton.Click
        IntervalEndTextBox.Text = VideoPositionUpDown.Value
    End Sub
End Class
