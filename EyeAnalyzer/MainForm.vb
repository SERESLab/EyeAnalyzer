''' <summary>
''' Main form for measuring fixations. Allows the user to specify stimulus segments
''' and AOIs and execute processing.
''' </summary>
Public Class MainForm

    Private Const MIN_FIXATION_DURATION_MS As Integer = 40

    Private _newSegment As StimulusSegment = Nothing
    Private _selectedSegment As StimulusSegment = Nothing

    Private _videoRecording As VideoRecording = Nothing
    Private _eyeTrackerData As EyeTrackerData = Nothing
    Private _didFrameChange As Boolean = False


    ''' <summary>
    ''' Formats the specified time in milliseconds to a string of the
    ''' format HH:MM:SS
    ''' </summary>
    Private Function makeTimeString(ByVal timeMs As ULong) As String
        Dim hours As Integer = timeMs / 3600000L
        timeMs = timeMs Mod 3600000L
        Dim minutes As Integer = timeMs / 60000L
        timeMs = timeMs Mod 60000L
        Dim seconds As Integer = timeMs / 1000L
        Return hours.ToString("D2") & ":" & minutes.ToString("D2") & ":" & _
            seconds.ToString("D2")
    End Function

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
        SegmentStartTextBox.Text = ""
        SegmentEndTextBox.Text = ""
        VideoPositionLabel.Text = makeTimeString(0)
        VideoGroupBox.Enabled = False
        SegmentsGroupBox.Enabled = False
        AddSegmentButton.Enabled = False
        DeleteSegmentButton.Enabled = False
        AoiGroupBox.Enabled = False
        AddAoiButton.Enabled = False
        CopyAoiButton.Enabled = False
        DeleteAoiButton.Enabled = False
        ProcessFixationsButton.Enabled = False
        VideoPictureBox.Image = Nothing
        _newSegment = New StimulusSegment()
        setStatusMessage("Ready for study data...")
    End Sub

    Private Sub setStatusMessage(ByVal msg As String)
        MainStatusLabel.Text = msg
    End Sub

    Private Sub OpenStudyDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenStudyDataToolStripMenuItem.Click
        If _videoRecording IsNot Nothing Or _eyeTrackerData IsNot Nothing Then
            If MessageBox.Show("Loading new data will clear all segments and AOIs. Would you like to continue?", _
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
            SegmentsGroupBox.Enabled = True
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
        VideoPositionLabel.Text = makeTimeString(VideoPositionUpDown.Value)
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

    Private Sub SegmentStartButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SegmentStartButton.Click
        If _newSegment.EndMs Is Nothing Or _newSegment.EndMs > VideoPositionUpDown.Value Then
            _newSegment.StartMs = VideoPositionUpDown.Value
            SegmentStartTextBox.Text = makeTimeString(_newSegment.StartMs)
        Else
            MessageBox.Show("Cannot choose start position at or after the chosen end position.")
        End If
    End Sub

    Private Sub SegmentEndButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SegmentEndButton.Click
        If _newSegment.StartMs Is Nothing Or _newSegment.StartMs < VideoPositionUpDown.Value Then
            _newSegment.EndMs = VideoPositionUpDown.Value
            SegmentEndTextBox.Text = makeTimeString(_newSegment.EndMs)
        Else
            MessageBox.Show("Cannot choose end position at or before the chosen start position.")
        End If
    End Sub
End Class
