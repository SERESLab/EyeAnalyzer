''' <summary>
''' Main form for measuring fixations. Allows the user to specify stimulus segments
''' and AOIs and execute processing.
''' </summary>
Public Class MainForm

    Private Const MIN_FIXATION_DURATION_MS As Integer = 40
    Private Const AOI_DRAW_WIDTH As Single = 1.0
    Private Const SELECTED_AOI_DRAW_WIDTH As Single = 3.0
    Private Const AOI_FILL_ALPHA As Byte = 170

    Private _defaultAoiColor As Color = Color.LightBlue
    Private _defaultNewAoiColor As Color = Color.Yellow
    Private _defaultSelectedAoiColor As Color = Color.Red
    Private _defaultNonExclusiveAoiColor As Color = Color.Green
    Private _newAoiPen As Pen
    Private _selectedAoiPen As Pen
    Private _aoiPen As Pen
    Private _nonExclusiveAoiPen As Pen
    Private _newAoiBrush As Brush
    Private _selectedAoiBrush As Brush
    Private _aoiBrush As Brush
    Private _nonExclusiveAoiBrush As Brush

    Private _stimulusSegments As List(Of StimulusSegment) = Nothing

    Private _isDraggingAoi As Boolean = False
    Private _newAoiRect? As Rectangle = Nothing
    Private _mouseDownLocation? As Point = Nothing

    Private _videoRecording As VideoRecording = Nothing
    Private _eyeTrackerData As EyeTrackerData = Nothing
    Private _isRedrawRequired As Boolean = False

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
        AoiColorLabel.BackColor = _defaultAoiColor
        NewAoiColorLabel.BackColor = _defaultNewAoiColor
        SelectedAoiColorLabel.BackColor = _defaultSelectedAoiColor
        NonExclusiveAoiColorLabel.BackColor = _defaultNonExclusiveAoiColor
        updateAoiDrawObjects()
        FixationDurationTextBox.Text = MIN_FIXATION_DURATION_MS
        resetForm()
    End Sub

    Private Sub updateAoiDrawObjects()
        Dim c As Color
        c = AoiColorLabel.BackColor
        _aoiPen = New Pen(c, AOI_DRAW_WIDTH)
        c = Color.FromArgb(AOI_FILL_ALPHA, c)
        _aoiBrush = New SolidBrush(c)
        c = NewAoiColorLabel.BackColor
        _newAoiPen = New Pen(c, SELECTED_AOI_DRAW_WIDTH)
        _newAoiPen.DashStyle = Drawing2D.DashStyle.Dash
        c = Color.FromArgb(AOI_FILL_ALPHA, c)
        _newAoiBrush = New SolidBrush(c)
        c = SelectedAoiColorLabel.BackColor
        _selectedAoiPen = New Pen(c, SELECTED_AOI_DRAW_WIDTH)
        c = Color.FromArgb(AOI_FILL_ALPHA, c)
        _selectedAoiBrush = New SolidBrush(c)
        c = NonExclusiveAoiColorLabel.BackColor
        _nonExclusiveAoiPen = New Pen(c, AOI_DRAW_WIDTH)
        c = Color.FromArgb(AOI_FILL_ALPHA, c)
        _nonExclusiveAoiBrush = New SolidBrush(c)
        _isRedrawRequired = True
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
        AddRenameAoiButton.Enabled = False
        DeleteAoiButton.Enabled = False
        ProcessFixationsButton.Enabled = False
        VideoPictureBox.Image = Nothing
        VideoPictureBox.Width = VideoPanel.Width
        VideoPictureBox.Height = VideoPanel.Height
        VideoActualSizeCheckBox.Checked = False
        _segmentStart = Nothing
        _segmentEnd = Nothing
        _stimulusSegments = New List(Of StimulusSegment)()
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
            AoiXUpDown.Maximum = _videoRecording.Width
            AoiYUpDown.Maximum = _videoRecording.Height
            AoiWidthUpDown.Maximum = _videoRecording.Width
            AoiHeightUpDown.Maximum = _videoRecording.Height
            setStatusMessage("Loaded study data.")
            VideoPositionUpDown.Maximum = _videoRecording.LengthMs
            VideoPositionUpDown.Increment = _videoRecording.TimeBetweenFramesMs
            _isRedrawRequired = True
            RedrawTimer.Start()
        Else
            setStatusMessage("Study data not loaded.")
        End If
    End Sub

    Private Sub VideoPositionUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionUpDown.ValueChanged
        _isRedrawRequired = True
        Dim percent As Single = CType(VideoPositionUpDown.Value, Single) / _videoRecording.LengthMs
        VideoPositionTrackBar.Value = percent * 100
        VideoPositionLabel.Text = makeTimeString(VideoPositionUpDown.Value)
    End Sub

    Private Sub VideoPositionTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionTrackBar.Scroll
        _isRedrawRequired = True
        Dim percent As Single = VideoPositionTrackBar.Value / 100.0
        Dim value As ULong = Math.Round(percent * _videoRecording.LengthMs)
        value -= value Mod _videoRecording.TimeBetweenFramesMs
        VideoPositionUpDown.Value = value
    End Sub

    Private Sub RedrawTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedrawTimer.Tick
        If _isRedrawRequired Then
            If Not VideoActualSizeCheckBox.Checked Then
                VideoPictureBox.Image = _videoRecording.drawFrameAtPosition(VideoPositionUpDown.Value, _
                                                 VideoPanel.Width, VideoPanel.Height)
            Else
                VideoPictureBox.Image = _videoRecording.drawFrameAtPosition(VideoPositionUpDown.Value, _
                                                 _videoRecording.Width, _videoRecording.Height)
            End If
            drawAois()
            _isRedrawRequired = False
        End If
        If SegmentNameTextBox.Text.Trim.Length > 0 And _segmentStart IsNot Nothing And _segmentEnd IsNot Nothing Then
            AddUpdateSegmentButton.Enabled = True
        Else
            AddUpdateSegmentButton.Enabled = False
        End If
        If _newAoiRect IsNot Nothing Or AoiListBox.SelectedItem IsNot Nothing Then
            If _isEditingSegment And AoiNameTextBox.Text.Trim.Length > 0 Then
                AddRenameAoiButton.Enabled = True
            Else
                AddRenameAoiButton.Enabled = False
            End If
            AoiXUpDown.Enabled = True
            AoiYUpDown.Enabled = True
            AoiWidthUpDown.Enabled = True
            AoiHeightUpDown.Enabled = True
        Else
            AddRenameAoiButton.Enabled = False
            AoiXUpDown.Enabled = False
            AoiYUpDown.Enabled = False
            AoiWidthUpDown.Enabled = False
            AoiHeightUpDown.Enabled = False
        End If
    End Sub

    Private Sub drawAois()
        Dim image As Image = VideoPictureBox.Image
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        If ss IsNot Nothing Then
            Using g = Graphics.FromImage(image)
                Dim scaledRect As Rectangle
                For Each aoi As AreaOfInterest In ss.AOIs
                    If aoi IsNot AoiListBox.SelectedItem Then
                        If VideoActualSizeCheckBox.Checked Then
                            scaledRect = aoi.Area
                        Else
                            scaledRect = scaleRectangle(aoi.Area, _videoRecording.Width, _videoRecording.Height, _
                                                        VideoPictureBox.Width, VideoPictureBox.Height)
                        End If
                        If aoi.NonExclusive Then
                            g.FillRectangle(_nonExclusiveAoiBrush, scaledRect)
                            g.DrawRectangle(_nonExclusiveAoiPen, scaledRect)
                        Else
                            g.FillRectangle(_aoiBrush, scaledRect)
                            g.DrawRectangle(_aoiPen, scaledRect)
                        End If
                    End If
                Next
                If AoiListBox.SelectedItem IsNot Nothing Then
                    Dim aoi As AreaOfInterest = AoiListBox.SelectedItem
                    If VideoActualSizeCheckBox.Checked Then
                        scaledRect = aoi.Area
                    Else
                        scaledRect = scaleRectangle(aoi.Area, _videoRecording.Width, _videoRecording.Height, _
                                                    VideoPictureBox.Width, VideoPictureBox.Height)
                    End If
                    g.FillRectangle(_selectedAoiBrush, scaledRect)
                    g.DrawRectangle(_selectedAoiPen, scaledRect)
                End If
                If _newAoiRect IsNot Nothing Then
                    If VideoActualSizeCheckBox.Checked Then
                        scaledRect = _newAoiRect
                    Else
                        scaledRect = scaleRectangle(_newAoiRect, _videoRecording.Width, _videoRecording.Height, _
                                                    VideoPictureBox.Width, VideoPictureBox.Height)
                    End If
                    g.FillRectangle(_newAoiBrush, scaledRect)
                    g.DrawRectangle(_newAoiPen, scaledRect)
                End If
            End Using
            VideoPictureBox.Image = Nothing
            VideoPictureBox.Image = image
        End If
    End Sub

    Private Function scaleRectangle(ByVal rect As Rectangle, ByVal currentWidth As Integer, ByVal currentHeight As Integer, _
                                    ByVal targetWidth As Integer, ByVal targetHeight As Integer) As Rectangle
        Dim xScale As Single = CType(targetWidth, Single) / CType(currentWidth, Single)
        Dim yScale As Single = CType(targetHeight, Single) / CType(currentHeight, Single)
        Return Rectangle.FromLTRB(rect.Left * xScale, rect.Top * yScale, rect.Right * xScale, rect.Bottom * yScale)
    End Function

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
            For Each segment As StimulusSegment In _stimulusSegments
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
            For Each segment As StimulusSegment In _stimulusSegments
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
        For Each segment As StimulusSegment In _stimulusSegments
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
        _stimulusSegments.Sort()
        SegmentsListBox.DataSource = Nothing
        SegmentsListBox.DataSource = _stimulusSegments
        SegmentsListBox.ClearSelected()
        setStatusMessage("Updated stimulus segment '" & ss.Name & "'.")
    End Sub

    Private Sub addNewSegment()
        For Each segment As StimulusSegment In _stimulusSegments
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
        _stimulusSegments.Add(ss)
        _stimulusSegments.Sort()
        SegmentsListBox.DataSource = Nothing
        SegmentsListBox.DataSource = _stimulusSegments
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
            AoiGroupBox.Enabled = True
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.ClearSelected()
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
            AoiListBox.DataSource = Nothing
            AoiGroupBox.Enabled = False
        End If
        _newAoiRect = Nothing
        _isRedrawRequired = True
    End Sub

    Private Sub UnselectSegmentButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnselectSegmentButton.Click
        SegmentsListBox.ClearSelected()
    End Sub

    Private Sub DeleteSegmentButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteSegmentButton.Click
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        If MessageBox.Show("Are you sure you would like to delete the segment '" & ss.Name & "'?", _
                                "Delete stimulus segment", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
            _stimulusSegments.Remove(ss)
            SegmentsListBox.DataSource = Nothing
            SegmentsListBox.DataSource = _stimulusSegments
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

    Private Sub VideoActualSizeCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoActualSizeCheckBox.CheckedChanged
        _isRedrawRequired = True
    End Sub

    Private Sub VideoPictureBox_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles VideoPictureBox.MouseDown
        If _isEditingSegment And _mouseDownLocation Is Nothing Then
            Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
            Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
            Dim scaledRect As Rectangle
            _isDraggingAoi = False

            ' check for click on selected aoi
            If selectedAoi IsNot Nothing Then
                If VideoActualSizeCheckBox.Checked Then
                    scaledRect = selectedAoi.Area
                Else
                    scaledRect = scaleRectangle(selectedAoi.Area, _videoRecording.Width, _videoRecording.Height, _
                                                VideoPictureBox.Width, VideoPictureBox.Height)
                End If
                If scaledRect.Contains(e.Location) Then
                    _newAoiRect = Nothing
                    _isDraggingAoi = True
                End If
            End If

            ' check for click on all other aois
            If Not _isDraggingAoi Then
                AoiListBox.ClearSelected()
                For Each aoi As AreaOfInterest In ss.AOIs
                    If VideoActualSizeCheckBox.Checked Then
                        scaledRect = aoi.Area
                    Else
                        scaledRect = scaleRectangle(aoi.Area, _videoRecording.Width, _videoRecording.Height, _
                                                    VideoPictureBox.Width, VideoPictureBox.Height)
                    End If
                    If scaledRect.Contains(e.Location) Then
                        _newAoiRect = Nothing
                        AoiListBox.SelectedItem = aoi
                        _isDraggingAoi = True
                        Exit For
                    End If
                Next
            End If

            ' check for click on drawn region
            If Not _isDraggingAoi And _newAoiRect IsNot Nothing Then
                If VideoActualSizeCheckBox.Checked Then
                    scaledRect = _newAoiRect
                Else
                    scaledRect = scaleRectangle(_newAoiRect, _videoRecording.Width, _videoRecording.Height, _
                                                VideoPictureBox.Width, VideoPictureBox.Height)
                End If
                If scaledRect.Contains(e.Location) Then
                    _isDraggingAoi = True
                End If
            End If
            _mouseDownLocation = e.Location
        End If
    End Sub

    Private Sub VideoPictureBox_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles VideoPictureBox.MouseLeave
        _mouseDownLocation = Nothing
    End Sub

    Private Sub VideoPictureBox_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles VideoPictureBox.MouseMove
        If _mouseDownLocation IsNot Nothing Then
            Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
            If _isDraggingAoi Then
                Dim offsetAmount As Point
                If VideoActualSizeCheckBox.Checked Then
                    offsetAmount = e.Location - _mouseDownLocation.Value
                Else
                    Dim oldLocation As Point = scaleRectangle(New Rectangle(_mouseDownLocation, New Size), _
                                                   VideoPictureBox.Width, VideoPictureBox.Height, _
                                                   _videoRecording.Width, _videoRecording.Height).Location
                    Dim newLocation As Point = scaleRectangle(New Rectangle(e.Location, New Size), _
                                                   VideoPictureBox.Width, VideoPictureBox.Height, _
                                                   _videoRecording.Width, _videoRecording.Height).Location
                    offsetAmount = newLocation - oldLocation
                End If

                ' drag selected rectangle
                Dim newRect As Rectangle
                If selectedAoi IsNot Nothing Then
                    newRect = New Rectangle(selectedAoi.Area.Location + offsetAmount, selectedAoi.Area.Size)
                ElseIf _newAoiRect IsNot Nothing Then
                    newRect = New Rectangle(_newAoiRect.Value.Location + offsetAmount, _newAoiRect.Value.Size)
                End If
                If Not isRectColliding(newRect) Then
                    If selectedAoi IsNot Nothing Then
                        selectedAoi.Area = newRect
                    ElseIf _newAoiRect IsNot Nothing Then
                        _newAoiRect = newRect
                    End If
                End If
                _mouseDownLocation = e.Location
            Else
                ' draw rectangle region
                Dim left As Integer = _mouseDownLocation.Value.X
                Dim top As Integer = _mouseDownLocation.Value.Y
                Dim right As Integer = e.Location.X
                Dim bottom As Integer = e.Location.Y
                If left > right Then
                    Dim tmp As Integer = right
                    right = left
                    left = tmp
                End If
                If top > bottom Then
                    Dim tmp As Integer = bottom
                    bottom = top
                    top = tmp
                End If
                Dim rect As Rectangle = Rectangle.FromLTRB(left, top, right, bottom)

                If Not VideoActualSizeCheckBox.Checked Then
                    rect = scaleRectangle(rect, VideoPictureBox.Width, VideoPictureBox.Height, _
                                                 _videoRecording.Width, _videoRecording.Height)
                End If
                If rect.Width < 1 Or rect.Height < 1 Then
                    _newAoiRect = Nothing
                    AoiXUpDown.Value = 0
                    AoiYUpDown.Value = 0
                    AoiWidthUpDown.Value = 1
                    AoiHeightUpDown.Value = 1
                Else
                    If Not isRectColliding(rect) Then
                        _newAoiRect = rect
                    End If
                End If
            End If
            If selectedAoi IsNot Nothing Then
                AoiXUpDown.Value = selectedAoi.Area.X
                AoiYUpDown.Value = selectedAoi.Area.Y
                AoiWidthUpDown.Value = selectedAoi.Area.Width
                AoiHeightUpDown.Value = selectedAoi.Area.Height
            ElseIf _newAoiRect IsNot Nothing Then
                AoiXUpDown.Value = _newAoiRect.Value.X
                AoiYUpDown.Value = _newAoiRect.Value.Y
                AoiWidthUpDown.Value = _newAoiRect.Value.Width
                AoiHeightUpDown.Value = _newAoiRect.Value.Height
            End If
            _isRedrawRequired = True
        End If
    End Sub

    Private Function isRectColliding(ByVal rect As Rectangle) As Boolean
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        Dim bounds As New Rectangle(0, 0, _videoRecording.Width, _videoRecording.Height)
        Dim isColliding As Boolean = Not bounds.Contains(rect)
        If Not AoiNonexclusiveCheckBox.Checked Then
            For Each testAoi As AreaOfInterest In AoiListBox.DataSource
                If testAoi IsNot selectedAoi And Not testAoi.NonExclusive Then
                    If testAoi.Area.IntersectsWith(rect) Then
                        isColliding = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return isColliding
    End Function

    Private Sub VideoPictureBox_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles VideoPictureBox.MouseUp
        If _mouseDownLocation IsNot Nothing Then
            If _mouseDownLocation.Value = e.Location Then
                If Not _isDraggingAoi Then
                    AoiListBox.ClearSelected()
                    _newAoiRect = Nothing
                    AoiXUpDown.Value = 0
                    AoiYUpDown.Value = 0
                    AoiWidthUpDown.Value = 1
                    AoiHeightUpDown.Value = 1
                End If
                _isRedrawRequired = True
            End If
        End If
        _isDraggingAoi = False
        _mouseDownLocation = Nothing
    End Sub

    Private Sub AoiColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiColorLabel.Click
        MainColorDialog.Color = AoiColorLabel.BackColor
        If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            AoiColorLabel.BackColor = MainColorDialog.Color
            updateAoiDrawObjects()
        End If
    End Sub

    Private Sub NewAoiColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewAoiColorLabel.Click
        MainColorDialog.Color = NewAoiColorLabel.BackColor
        If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            NewAoiColorLabel.BackColor = MainColorDialog.Color
            updateAoiDrawObjects()
        End If
    End Sub

    Private Sub SelectedAoiColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectedAoiColorLabel.Click
        MainColorDialog.Color = SelectedAoiColorLabel.BackColor
        If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            SelectedAoiColorLabel.BackColor = MainColorDialog.Color
            updateAoiDrawObjects()
        End If
    End Sub

    Private Sub NonExclusiveAoiColorLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonExclusiveAoiColorLabel.Click
        MainColorDialog.Color = NonExclusiveAoiColorLabel.BackColor
        If MainColorDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            NonExclusiveAoiColorLabel.BackColor = MainColorDialog.Color
            updateAoiDrawObjects()
        End If
    End Sub

    Private Sub AddRenameAoiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRenameAoiButton.Click
        If AoiListBox.SelectedItem Is Nothing Then
            addNewAoi()
        Else
            updateSelectedAoi()
        End If
    End Sub

    Private Sub addNewAoi()
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        Dim name As String = AoiNameTextBox.Text.Trim
        Dim doesNameExist As Boolean = False

        For Each aoi As AreaOfInterest In ss.AOIs
            If aoi.Name = name Then
                doesNameExist = True
                Exit For
            End If
        Next

        If doesNameExist Then
            MessageBox.Show("Specified AOI name is already in use!", _
                                "Invalid AOI name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim newAoi As New AreaOfInterest(name, _newAoiRect)
            newAoi.NonExclusive = AoiNonexclusiveCheckBox.Checked
            _newAoiRect = Nothing
            ss.AOIs.Add(newAoi)
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.SelectedItem = newAoi
        End If
    End Sub

    Private Sub updateSelectedAoi()
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        Dim name As String = AoiNameTextBox.Text.Trim
        Dim doesNameExist As Boolean = False

        For Each aoi As AreaOfInterest In ss.AOIs
            If aoi IsNot selectedAoi And aoi.Name = name Then
                doesNameExist = True
                Exit For
            End If
        Next

        If doesNameExist Then
            MessageBox.Show("Specified AOI name is already in use!", _
                                "Invalid AOI name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            selectedAoi.Name = name
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.SelectedItem = selectedAoi
        End If
    End Sub

    Private Sub AoiListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiListBox.SelectedIndexChanged
        _isDraggingAoi = False
        _newAoiRect = Nothing
        If AoiListBox.SelectedItem IsNot Nothing Then
            Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
            AoiNameTextBox.Text = selectedAoi.Name
            AoiXUpDown.Value = selectedAoi.Area.X
            AoiYUpDown.Value = selectedAoi.Area.Y
            AoiWidthUpDown.Value = selectedAoi.Area.Width
            AoiHeightUpDown.Value = selectedAoi.Area.Height
            AoiNonexclusiveCheckBox.Checked = selectedAoi.NonExclusive
            AddRenameAoiButton.Text = "Rename"
            DeleteAoiButton.Enabled = True
        Else
            AoiNameTextBox.Text = ""
            AoiXUpDown.Value = 0
            AoiYUpDown.Value = 0
            AoiWidthUpDown.Value = 1
            AoiHeightUpDown.Value = 1
            AoiNonexclusiveCheckBox.Checked = False
            AddRenameAoiButton.Text = "Add New"
            DeleteAoiButton.Enabled = False
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub AoiNonexclusiveCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiNonexclusiveCheckBox.CheckedChanged
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        If selectedAoi IsNot Nothing Then
            If Not AoiNonexclusiveCheckBox.Checked And isRectColliding(selectedAoi.Area) Then
                AoiNonexclusiveCheckBox.Checked = True
                MessageBox.Show("Cannot set AOI as non-exclusive because it overlaps an exclusive AOI.", "Non-exclusive AOI", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                selectedAoi.NonExclusive = AoiNonexclusiveCheckBox.Checked
            End If
        ElseIf _newAoiRect IsNot Nothing Then
            If isRectColliding(_newAoiRect.Value) Then
                AoiNonexclusiveCheckBox.Checked = True
                MessageBox.Show("Cannot set selection as non-exclusive because it overlaps an exclusive AOI.", "Non-exclusive AOI", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub AoiXUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiXUpDown.ValueChanged
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        If selectedAoi IsNot Nothing Then
            Dim rect As Rectangle = selectedAoi.Area
            rect.X = AoiXUpDown.Value
            If Not isRectColliding(rect) Then
                selectedAoi.Area = rect
            Else
                AoiXUpDown.Value = selectedAoi.Area.X
            End If
        ElseIf _newAoiRect IsNot Nothing Then
            Dim rect As Rectangle = _newAoiRect
            rect.X = AoiXUpDown.Value
            If Not isRectColliding(rect) Then
                _newAoiRect = rect
            Else
                AoiXUpDown.Value = _newAoiRect.Value.X
            End If
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub AoiYUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiYUpDown.ValueChanged
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        If selectedAoi IsNot Nothing Then
            Dim rect As Rectangle = selectedAoi.Area
            rect.Y = AoiYUpDown.Value
            If Not isRectColliding(rect) Then
                selectedAoi.Area = rect
            Else
                AoiYUpDown.Value = selectedAoi.Area.Y
            End If
        ElseIf _newAoiRect IsNot Nothing Then
            Dim rect As Rectangle = _newAoiRect
            rect.Y = AoiYUpDown.Value
            If Not isRectColliding(rect) Then
                _newAoiRect = rect
            Else
                AoiYUpDown.Value = _newAoiRect.Value.Y
            End If
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub AoiWidthUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiWidthUpDown.ValueChanged
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        If selectedAoi IsNot Nothing Then
            Dim rect As Rectangle = selectedAoi.Area
            rect.Width = AoiWidthUpDown.Value
            If Not isRectColliding(rect) Then
                selectedAoi.Area = rect
            Else
                AoiWidthUpDown.Value = selectedAoi.Area.Width
            End If
        ElseIf _newAoiRect IsNot Nothing Then
            Dim rect As Rectangle = _newAoiRect
            rect.Width = AoiWidthUpDown.Value
            If Not isRectColliding(rect) Then
                _newAoiRect = rect
            Else
                AoiWidthUpDown.Value = _newAoiRect.Value.Width
            End If
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub AoiHeightUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiHeightUpDown.ValueChanged
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        If selectedAoi IsNot Nothing Then
            Dim rect As Rectangle = selectedAoi.Area
            rect.Height = AoiHeightUpDown.Value
            If Not isRectColliding(rect) Then
                selectedAoi.Area = rect
            Else
                AoiHeightUpDown.Value = selectedAoi.Area.Height
            End If
        ElseIf _newAoiRect IsNot Nothing Then
            Dim rect As Rectangle = _newAoiRect
            rect.Height = AoiHeightUpDown.Value
            If Not isRectColliding(rect) Then
                _newAoiRect = rect
            Else
                AoiHeightUpDown.Value = _newAoiRect.Value.Height
            End If
        End If
        _isRedrawRequired = True
    End Sub

    Private Sub DeleteAoiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteAoiButton.Click
        If MessageBox.Show("Are you sure you would like to delete the specified AOI?", _
                                "Delete AOI", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Yes Then
            Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
            ss.AOIs.Remove(AoiListBox.SelectedItem)
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.ClearSelected()
        End If
    End Sub
End Class
