''' <summary>
''' Main form for measuring fixations. Allows the user to specify stimulus segments
''' and AOIs and execute processing.
''' </summary>
Public Class MainForm

    Private Const MIN_FIXATION_DURATION_MS As Integer = 40

    Private _segments As List(Of StimulusSegment) = Nothing

    Private _videoRecording As VideoRecording = Nothing
    Private _eyeTrackerData As EyeTrackerData = Nothing
    Private _didFrameChange As Boolean = False

    Private _isEditingSegment As Boolean = False
    Private _segmentStart? As ULong = Nothing
    Private _segmentEnd? As ULong = Nothing

    ''' <summary>
    ''' Formats the specified time in milliseconds to a string of the
    ''' format HH:MM:SS
    ''' </summary>
    Private Function makeTimeString(ByVal timeMs As ULong) As String
        Dim hours As Integer = timeMs \ 3600000L
        timeMs -= hours * 3600000L
        Dim minutes As Integer = timeMs \ 60000L
        timeMs -= minutes * 60000L
        Dim seconds As Integer = timeMs \ 1000L
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
        SegmentStartLinkLabel.Text = "-:-:-"
        SegmentEndLinkLabel.Text = "-:-:-"
        VideoPositionLabel.Text = makeTimeString(0)
        UnselectSegmentButton.Enabled = False
        VideoGroupBox.Enabled = False
        SegmentsGroupBox.Enabled = False
        AddUpdateSegmentButton.Enabled = False
        DeleteSegmentButton.Enabled = False
        AoiGroupBox.Enabled = False
        AddAoiButton.Enabled = False
        CopyAoiButton.Enabled = False
        DeleteAoiButton.Enabled = False
        ProcessFixationsButton.Enabled = False
        VideoPictureBox.Image = Nothing
        _segmentStart = Nothing
        _segmentEnd = Nothing
        _segments = New List(Of StimulusSegment)()
        _isEditingSegment = False
        SegmentsListBox.DataSource = Nothing
        setStatusMessage("Ready for study data...")
    End Sub

    Private Sub setStatusMessage(ByVal msg As String)
        MainStatusLabel.Text = msg
    End Sub

    Private Sub OpenStudyDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenStudyDataToolStripMenuItem.Click
        If _videoRecording IsNot Nothing Or _eyeTrackerData IsNot Nothing Then
            If MessageBox.Show("Loading new data will clear all segments and AOIs. Would you like to continue?", _
                               "Clear all data?", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.OK Then
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
        Dim value As ULong = Math.Round(percent * _videoRecording.LengthMs)
        value -= value Mod _videoRecording.TimeBetweenFramesMs
        VideoPositionUpDown.Value = value
    End Sub

    Private Sub RedrawTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedrawTimer.Tick
        If _didFrameChange Then
            VideoPictureBox.Image = _videoRecording.drawFrameAtPosition(VideoPositionUpDown.Value, _
                                             VideoPictureBox.Width, VideoPictureBox.Height)
            _didFrameChange = False
        End If

        If SegmentNameTextBox.Text.Length > 0 _
            And _segmentStart IsNot Nothing _
            And _segmentEnd IsNot Nothing Then
            AddUpdateSegmentButton.Enabled = True
        Else
            AddUpdateSegmentButton.Enabled = False
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
        If _segmentEnd Is Nothing Or _segmentEnd > VideoPositionUpDown.Value Then
            Dim value As ULong = VideoPositionUpDown.Value
            Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
            For Each segment As StimulusSegment In _segments
                If (value >= segment.StartMs And value <= segment.EndMs) _
                    And (Not _isEditingSegment Or segment IsNot ss) Then
                    MessageBox.Show("Specified segment start time falls within segment '" & segment.Name & "'!", _
                                    "Invalid start time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            Next
            _segmentStart = value
            SegmentStartLinkLabel.Text = makeTimeString(_segmentStart)
        Else
            MessageBox.Show("Cannot choose start position at or after the chosen end position.", _
                                    "Invalid start time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub SegmentEndButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SegmentEndButton.Click
        If _segmentStart Is Nothing Or _segmentStart < VideoPositionUpDown.Value Then
            Dim value As ULong = VideoPositionUpDown.Value
            Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
            For Each segment As StimulusSegment In _segments
                If (value >= segment.StartMs And value <= segment.EndMs) _
                    And (Not _isEditingSegment Or segment IsNot ss) Then
                    MessageBox.Show("Specified segment end time falls within segment '" & segment.Name & "'!", _
                                    "Invalid end time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
            Next
            _segmentEnd = value
            SegmentEndLinkLabel.Text = makeTimeString(_segmentEnd)
        Else
            MessageBox.Show("Cannot choose end position at or before the chosen start position.", _
                                    "Invalid end time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub SegmentNameTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SegmentNameTextBox.KeyDown
        If e.KeyCode = Keys.Enter And AddUpdateSegmentButton.Enabled = True Then
            addNewSegment()
        End If
    End Sub

    Private Sub AddUpdateSegmentButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddUpdateSegmentButton.Click
        If _isEditingSegment Then
            updateSegment()
        Else
            addNewSegment()
        End If
    End Sub

    Private Sub updateSegment()
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        For Each segment As StimulusSegment In _segments
            If segment IsNot ss And segment.Name = SegmentNameTextBox.Text Then
                MessageBox.Show("Specified segment name is already in use!", _
                                "Invalid segment name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            If segment IsNot ss And segment.StartMs >= _segmentStart And segment.EndMs <= _segmentEnd Then
                MessageBox.Show("Segment '" & segment.Name & "' falls between the specified start and end times.", _
                                "Invalid segment boundaries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
        Next
        ss.Name = SegmentNameTextBox.Text
        ss.StartMs = _segmentStart
        ss.EndMs = _segmentEnd
        _segments.Sort()
        SegmentsListBox.DataSource = Nothing
        SegmentsListBox.DataSource = _segments
        SegmentsListBox.ClearSelected()
        setStatusMessage("Updated stimulus segment '" & ss.Name & "'.")
    End Sub

    Private Sub addNewSegment()
        For Each segment As StimulusSegment In _segments
            If segment.Name = SegmentNameTextBox.Text Then
                MessageBox.Show("Specified segment name is already in use!", _
                                "Invalid segment name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
            If segment.StartMs >= _segmentStart And segment.EndMs <= _segmentEnd Then
                MessageBox.Show("Segment '" & segment.Name & "' falls between the specified start and end times.", _
                                "Invalid segment boundaries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
        Next
        Dim ss As New StimulusSegment()
        ss.Name = SegmentNameTextBox.Text
        ss.StartMs = _segmentStart
        ss.EndMs = _segmentEnd

        _segmentStart = Nothing
        _segmentEnd = Nothing
        SegmentNameTextBox.Text = ""
        SegmentStartLinkLabel.Text = "-:-:-"
        SegmentEndLinkLabel.Text = "-:-:-"
        _segments.Add(ss)
        _segments.Sort()
        SegmentsListBox.DataSource = Nothing
        SegmentsListBox.DataSource = _segments
        SegmentsListBox.ClearSelected()
        setStatusMessage("New stimulus segment '" & ss.Name & "' added.")
    End Sub

    Private Sub SegmentsListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SegmentsListBox.SelectedIndexChanged
        If SegmentsListBox.SelectedItem IsNot Nothing Then
            Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
            _isEditingSegment = True
            AddUpdateSegmentButton.Text = "Update"
            SegmentNameTextBox.Text = ss.Name
            SegmentStartLinkLabel.Text = makeTimeString(ss.StartMs)
            SegmentEndLinkLabel.Text = makeTimeString(ss.EndMs)
            _segmentStart = ss.StartMs
            _segmentEnd = ss.EndMs
            UnselectSegmentButton.Enabled = True
            DeleteSegmentButton.Enabled = True
        Else
            _isEditingSegment = False
            AddUpdateSegmentButton.Text = "Add New"
            _segmentStart = Nothing
            _segmentEnd = Nothing
            SegmentNameTextBox.Text = ""
            SegmentStartLinkLabel.Text = "-:-:-"
            SegmentEndLinkLabel.Text = "-:-:-"
            UnselectSegmentButton.Enabled = False
            DeleteSegmentButton.Enabled = False
        End If
    End Sub

    Private Sub UnselectSegmentButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnselectSegmentButton.Click
        SegmentsListBox.ClearSelected()
    End Sub

    Private Sub DeleteSegmentButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSegmentButton.Click
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        If MessageBox.Show("Are you sure you would like to delete the segment '" & ss.Name & "'?", _
                                "Delete stimulus segment", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
            _segments.Remove(ss)
            SegmentsListBox.DataSource = Nothing
            SegmentsListBox.DataSource = _segments
            SegmentsListBox.ClearSelected()
            setStatusMessage("Removed stimulus segment '" & ss.Name & "'.")
        End If
    End Sub

    Private Sub SegmentStartLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles SegmentStartLinkLabel.LinkClicked
        If _segmentStart IsNot Nothing Then
            VideoPositionUpDown.Value = _segmentStart
        End If
    End Sub

    Private Sub SegmentEndLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles SegmentEndLinkLabel.LinkClicked
        If _segmentEnd IsNot Nothing Then
            VideoPositionUpDown.Value = _segmentEnd
        End If
    End Sub
End Class
