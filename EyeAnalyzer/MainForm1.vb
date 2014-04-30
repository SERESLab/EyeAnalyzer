Imports System.Xml
Imports System.Timers
Imports System.IO

''' <summary>
''' Main form for measuring fixations. Allows the user to specify stimulus segments
''' and AOIs and execute processing.
''' </summary>
Public Class MainForm1
    ''' <summary>
    ''' Creating of tabs for Settings and Visualization as well as for heatmap. 
    ''' This was copied from the original Michael Falcone code.
    ''' by Abdul-Basit
    ''' </summary>
    ''' <remarks></remarks>
    ''' ---------------------------------------------------
    ''' 
#Region "Heatmap Tab"


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
        lblAOI.Text = ""
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
    '-------------------------------------------------------------------
#End Region

#Region "Experiments and Settings tab"

    'Declare private members/fields
    ''' <summary>
    ''' Setting-up and processing experiment data: setting initial parameters and thresholds 
    ''' and also extracting the required data
    ''' </summary>
    ''' <remarks>Abdul-basit</remarks>

    Private Const AOI_DRAW_WIDTH As Single = 1.0
    Private Const SELECTED_AOI_DRAW_WIDTH As Single = 2.0

    Private _lastDirVideoData As String = Nothing
    Private _lastDirEyeData As String = Nothing
    Private _lastDirStimulusSegments As String = Nothing
    Private _lastDirProcessingOutput As String = Nothing
    Private _lastDirScreenshots As String = Nothing

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

    Private WithEvents _videoRecording As VideoRecording = Nothing
    Private _eyeTrackerData As MirametrixViewerData = Nothing
    Private _isRedrawRequired As Boolean = False

    Private _isEditingSegment As Boolean = False
    Private _segmentStart? As ULong = Nothing
    Private _segmentEnd? As ULong = Nothing

    ''' <summary>
    ''' Formats the specified time in milliseconds to a string of the
    ''' format HH:MM:SS
    ''' </summary>
    ''' 
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



    Private Sub MainForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        updateAoiDrawObjects()
        resetForm()
    End Sub

    Private Sub updateAoiDrawObjects()
        Dim c As Color
        c = AoiColorLabel.BackColor
        _aoiPen = New Pen(c, AOI_DRAW_WIDTH)
        c = Color.FromArgb(AoiOpacityTrackBar.Value, c)
        _aoiBrush = New SolidBrush(c)
        c = NewAoiColorLabel.BackColor
        _newAoiPen = New Pen(c, SELECTED_AOI_DRAW_WIDTH)
        _newAoiPen.DashStyle = Drawing2D.DashStyle.Dash
        c = Color.FromArgb(AoiOpacityTrackBar.Value, c)
        _newAoiBrush = New SolidBrush(c)
        c = SelectedAoiColorLabel.BackColor
        _selectedAoiPen = New Pen(c, SELECTED_AOI_DRAW_WIDTH)
        c = Color.FromArgb(AoiOpacityTrackBar.Value, c)
        _selectedAoiBrush = New SolidBrush(c)
        c = NonExclusiveAoiColorLabel.BackColor
        _nonExclusiveAoiPen = New Pen(c, AOI_DRAW_WIDTH)
        c = Color.FromArgb(AoiOpacityTrackBar.Value, c)
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
        DisplaySettingsGroupBox.Enabled = False
        UnselectSegmentButton.Enabled = False
        SegmentStartLinkLabel.Enabled = False
        SegmentEndLinkLabel.Enabled = False
        VideoGroupBox.Enabled = False
        SegmentsGroupBox.Enabled = False
        AddUpdateSegmentButton.Enabled = False
        DeleteSegmentButton.Enabled = False
        AoiGroupBox.Enabled = False
        AddRenameAoiButton.Enabled = False
        DeleteAoiButton.Enabled = False
        ProcessButton.Enabled = False
        ImportStimulusSegmentsToolStripMenuItem.Enabled = False
        ExportStimulusSegmentsToolStripMenuItem.Enabled = False
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
                               "Clear all data?", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Cancel Then
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
        If _lastDirVideoData IsNot Nothing Then
            MainOpenFileDialog.InitialDirectory = _lastDirVideoData
        End If
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            videoFileFullName = MainOpenFileDialog.FileName
            videoFileName = MainOpenFileDialog.SafeFileName
            _lastDirVideoData = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
        Else
            setStatusMessage("Study data not loaded.")
            Return
        End If

        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Title = "Select eye-tracker data"
        MainOpenFileDialog.FileName = ""  '---duplication. Already initialized---.
        MainOpenFileDialog.Filter = "Eye-tracker data (*.xml)|*.xml"
        If _lastDirEyeData IsNot Nothing Then
            MainOpenFileDialog.InitialDirectory = _lastDirEyeData
        End If
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            xmlFileFullName = MainOpenFileDialog.FileName
            xmlFileName = MainOpenFileDialog.SafeFileName
            _lastDirEyeData = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
        End If

        If videoFileFullName IsNot Nothing And xmlFileFullName IsNot Nothing Then
            setStatusMessage("Loading...")
            Dim recording As VideoRecording = Nothing
            Dim eyeData As MirametrixViewerData = Nothing
            Try
                recording = VideoRecording.FromFile(videoFileFullName)
            Catch ex As Exception
                setStatusMessage("Study data not loaded.")
                MessageBox.Show("Could not load screen-capture video.", "Error loading study data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
            Try
                eyeData = MirametrixViewerData.FromFile(xmlFileFullName)
            Catch ex As Exception
                setStatusMessage("Study data not loaded.")
                MessageBox.Show("Could not load eye tracker data.", "Error loading study data", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

            ' reset and prepare form if everything loaded properly
            resetForm()
            _eyeTrackerData = eyeData
            _videoRecording = recording
            VideoFileStatusLabel.Text = videoFileName
            _eyeTrackerData = MirametrixViewerData.FromFile(xmlFileFullName)
            XmlFileStatusLabel.Text = xmlFileName
            VideoGroupBox.Enabled = True
            SegmentsGroupBox.Enabled = True
            AoiXUpDown.Maximum = _videoRecording.Width
            AoiYUpDown.Maximum = _videoRecording.Height
            AoiWidthUpDown.Maximum = _videoRecording.Width
            AoiHeightUpDown.Maximum = _videoRecording.Height
            setStatusMessage("Loaded study data (" & _eyeTrackerData.GazeCount & " gaze points).")
            VideoPositionUpDown.Maximum = _videoRecording.LengthMs
            VideoPositionUpDown.Increment = _videoRecording.TimeBetweenFramesMs
            DisplaySettingsGroupBox.Enabled = True
            ImportStimulusSegmentsToolStripMenuItem.Enabled = True
            _isRedrawRequired = True
            RedrawTimer.Start()
        Else
            setStatusMessage("Study data not loaded.")
        End If
    End Sub

    Private Sub VideoPositionUpDown_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionUpDown.ValueChanged
        Dim percent As Single = CType(VideoPositionUpDown.Value, Single) / _videoRecording.LengthMs
        VideoPositionTrackBar.Value = percent * 100
        VideoPositionLabel.Text = makeTimeString(VideoPositionUpDown.Value)
        _videoRecording.Position = VideoPositionUpDown.Value
    End Sub

    Private Sub VideoPositionTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoPositionTrackBar.Scroll
        Dim percent As Single = VideoPositionTrackBar.Value / 100.0
        Dim value As ULong = Math.Round(percent * _videoRecording.LengthMs)
        value -= value Mod _videoRecording.TimeBetweenFramesMs
        VideoPositionUpDown.Value = value
        _videoRecording.Position = VideoPositionUpDown.Value
    End Sub

    Private Sub RedrawTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedrawTimer.Tick
        If _isRedrawRequired Then
            If Not VideoActualSizeCheckBox.Checked Then
                VideoPictureBox.Image = _videoRecording.getScaledFrame(VideoPanel.Width, VideoPanel.Height)
            Else
                VideoPictureBox.Image = _videoRecording.getScaledFrame(_videoRecording.Width, _videoRecording.Height)
            End If
            drawAois()
            _isRedrawRequired = False
        End If

        ' enable exporting
        ExportStimulusSegmentsToolStripMenuItem.Enabled = _stimulusSegments.Count > 0

        ' enable modifying stimulus segments if prereqs are met
        AddUpdateSegmentButton.Enabled = SegmentNameTextBox.Text.Trim.Length > 0 And _segmentStart IsNot Nothing And _segmentEnd IsNot Nothing

        ' enable modifying AOIs if prereqs are met
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

        ' enable processing if prereqs are met
        Dim enableProcessing As Boolean = False
        For Each segment As StimulusSegment In _stimulusSegments
            If segment.AOIs.Count > 0 Then
                enableProcessing = True
                Exit For
            End If
        Next
        ProcessButton.Enabled = enableProcessing
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
        Using b = _videoRecording.getScaledFrame(_videoRecording.Width, _videoRecording.Height)
            MainSaveFileDialog.Title = "Save screenshot"
            MainSaveFileDialog.FileName = "screenshot"
            MainSaveFileDialog.Filter = "PNG (*.png)|*.png"
            If _lastDirScreenshots IsNot Nothing Then
                MainSaveFileDialog.InitialDirectory = _lastDirScreenshots
            End If
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                _lastDirScreenshots = MainSaveFileDialog.FileName
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
            SegmentStartLinkLabel.Enabled = True
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
            SegmentEndLinkLabel.Enabled = True
        Else
            MessageBox.Show("Cannot choose end position at or before the chosen start position.", _
                                    "Invalid end time", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub SegmentNameTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SegmentNameTextBox.KeyDown
        If e.KeyCode = Keys.Enter And AddUpdateSegmentButton.Enabled = True Then
            If _isEditingSegment Then
                updateSegment()
            Else
                addNewSegment()
            End If
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
            SegmentStartLinkLabel.Enabled = (ss.StartMs <= _videoRecording.LengthMs)
            SegmentEndLinkLabel.Enabled = (ss.EndMs <= _videoRecording.LengthMs)
            _segmentStart = ss.StartMs
            _segmentEnd = ss.EndMs
            UnselectSegmentButton.Enabled = True
            DeleteSegmentButton.Enabled = True
            AoiGroupBox.Enabled = True
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.ClearSelected()
            setStatusMessage("Click and drag over video preview to draw a new area of interest.")
        Else
            _isEditingSegment = False
            AddUpdateSegmentButton.Text = "Add New"
            _segmentStart = Nothing
            _segmentEnd = Nothing
            SegmentNameTextBox.Text = ""
            SegmentStartLinkLabel.Text = "-:-:-"
            SegmentEndLinkLabel.Text = "-:-:-"
            SegmentStartLinkLabel.Enabled = False
            SegmentEndLinkLabel.Enabled = False
            UnselectSegmentButton.Enabled = False
            DeleteSegmentButton.Enabled = False
            AoiListBox.DataSource = Nothing
            AoiGroupBox.Enabled = False
            setStatusMessage("Ready.")
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
            setStatusMessage("Added new AOI '" & name & "' to stimulus segment '" & ss.Name & "'.")
        End If
    End Sub

    Private Sub updateSelectedAoi()
        Dim ss As StimulusSegment = SegmentsListBox.SelectedItem
        Dim selectedAoi As AreaOfInterest = AoiListBox.SelectedItem
        Dim name As String = AoiNameTextBox.Text.Trim
        Dim oldName As String = selectedAoi.Name
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
            setStatusMessage("Renamed AOI '" & oldName & "' to '" & name & "' in stimulus segment '" & ss.Name & "'.")
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
            Dim aoi As AreaOfInterest = AoiListBox.SelectedItem
            ss.AOIs.Remove(aoi)
            AoiListBox.DataSource = Nothing
            AoiListBox.DataSource = ss.AOIs
            AoiListBox.ClearSelected()
            setStatusMessage("Removed AOI '" & aoi.Name & "' from stimulus segment '" & ss.Name & "'.")
        End If
    End Sub

    Private Sub AoiNameTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AoiNameTextBox.KeyDown
        If e.KeyCode = Keys.Enter And AddRenameAoiButton.Enabled Then
            If AoiListBox.SelectedItem Is Nothing Then
                addNewAoi()
            Else
                updateSelectedAoi()
            End If
        End If
    End Sub

    Private Sub AoiOpacityTrackBar_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AoiOpacityTrackBar.Scroll
        updateAoiDrawObjects()
    End Sub

    Private Sub ExportStimulusSegmentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportStimulusSegmentsToolStripMenuItem.Click
        MainSaveFileDialog.Title = "Export stimulus segments"
        MainSaveFileDialog.FileName = "segments"
        MainSaveFileDialog.Filter = "Stimulus Segment XML (*.xml)|*.xml"
        If _lastDirStimulusSegments IsNot Nothing Then
            MainSaveFileDialog.InitialDirectory = _lastDirStimulusSegments
        End If
        If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            _lastDirStimulusSegments = MainSaveFileDialog.FileName
            Try
                exportStimulusSegments(MainSaveFileDialog.FileName)
                setStatusMessage("Exported stimulus segments to " & MainSaveFileDialog.FileName & ".")
            Catch ex As Exception
                MessageBox.Show("An error occurred while exporting stimulus segments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ImportStimulusSegmentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportStimulusSegmentsToolStripMenuItem.Click
        If _stimulusSegments.Count > 0 Then
            If MessageBox.Show("Importing stimulus segments will clear existing segments. Would you like to continue?", _
                               "Clear stimulus segments?", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) = Windows.Forms.DialogResult.Cancel Then
                Return
            End If
        End If
        MainOpenFileDialog.Multiselect = False
        MainOpenFileDialog.Title = "Import stimulus segments"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Stimulus Segment XML (*.xml)|*.xml"
        If _lastDirStimulusSegments IsNot Nothing Then
            MainOpenFileDialog.InitialDirectory = _lastDirStimulusSegments
        End If
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            _lastDirStimulusSegments = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
            Try
                importStimulusSegments(MainOpenFileDialog.FileName)
                setStatusMessage("Imported stimulus segments from " & MainOpenFileDialog.FileName & ".")
            Catch ex As Exception
                MessageBox.Show("An error occurred while importing stimulus segments.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub importStimulusSegments(ByVal filename As String)
        Dim segments As New List(Of StimulusSegment)
        Dim currentSegment As StimulusSegment = Nothing
        Using reader As New System.Xml.XmlTextReader(filename)
            While reader.Read()
                If reader.IsStartElement("AOI") Then
                    Dim name As String = reader.GetAttribute("name")
                    Dim nonExclusive As String = reader.GetAttribute("nonExclusive")
                    Dim x As String = reader.GetAttribute("x")
                    Dim y As String = reader.GetAttribute("y")
                    Dim width As String = reader.GetAttribute("width")
                    Dim height As String = reader.GetAttribute("height")

                    If currentSegment Is Nothing Or name Is Nothing Or nonExclusive Is Nothing _
                        Or x Is Nothing Or y Is Nothing Or width Is Nothing Or height Is Nothing Then
                        Throw New Exception("Bad format.")
                    End If
                    Dim rect As New Rectangle(Integer.Parse(x), Integer.Parse(y), Integer.Parse(width), Integer.Parse(height))
                    Dim aoi As New AreaOfInterest(name, rect)
                    aoi.NonExclusive = (nonExclusive = "1")
                    currentSegment.AOIs.Add(aoi)
                ElseIf reader.IsStartElement("StimulusSegment") Then
                    Dim name As String = reader.GetAttribute("name")
                    Dim startTime As String = reader.GetAttribute("startTime")
                    Dim endTime As String = reader.GetAttribute("endTime")

                    If name Is Nothing Or startTime Is Nothing Or endTime Is Nothing Then
                        Throw New Exception("Bad format.")
                    End If
                    currentSegment = New StimulusSegment()
                    currentSegment.Name = name
                    currentSegment.StartMs = ULong.Parse(startTime)
                    currentSegment.EndMs = ULong.Parse(endTime)
                    segments.Add(currentSegment)
                End If
            End While
        End Using
        segments.Sort()
        _stimulusSegments = segments
        SegmentsListBox.DataSource = Nothing
        SegmentsListBox.DataSource = _stimulusSegments
        SegmentsListBox.ClearSelected()
    End Sub

    Private Sub exportStimulusSegments(ByVal filename As String)
        Using writer = New System.Xml.XmlTextWriter(filename, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = System.Xml.Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("StimulusSegments")
            For Each segment As StimulusSegment In _stimulusSegments
                writer.WriteStartElement("StimulusSegment")
                writer.WriteAttributeString("name", segment.Name)
                writer.WriteAttributeString("startTime", segment.StartMs.ToString())
                writer.WriteAttributeString("endTime", segment.EndMs.ToString())
                '  writer.WriteAttributeString("AOIHeight", segment.AOIheight.ToString)
                writer.WriteStartElement("AreasOfInterest")

                For Each aoi As AreaOfInterest In segment.AOIs
                    writer.WriteStartElement("AOI")
                    writer.WriteAttributeString("name", aoi.Name)
                    writer.WriteAttributeString("nonExclusive", If(aoi.NonExclusive, "1", "0"))
                    writer.WriteAttributeString("x", aoi.Area.X.ToString())
                    writer.WriteAttributeString("y", aoi.Area.Y.ToString())
                    writer.WriteAttributeString("width", aoi.Area.Width.ToString())
                    writer.WriteAttributeString("height", aoi.Area.Height.ToString())
                    writer.WriteEndElement()
                Next
                writer.WriteEndElement()
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        End Using
    End Sub

    Private Sub MeasureFixationsCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MeasureFixationsCheckBox.CheckedChanged
        MeasureFixationsGroupBox.Enabled = MeasureFixationsCheckBox.Checked
        SaveFixationLocationsCheckBox.Enabled = MeasureFixationsCheckBox.Checked
    End Sub

    Private Sub ProcessButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessButton.Click
        Dim minFixationLength As Integer = Integer.Parse(FixationDurationTextBox.Text)
        Dim results As ProcessingResults = _eyeTrackerData.process(minFixationLength, _stimulusSegments)
        Dim filenameBase As String = XmlFileStatusLabel.Text.Substring(0, XmlFileStatusLabel.Text.Length - 4)

        If _lastDirProcessingOutput IsNot Nothing Then
            MainSaveFileDialog.InitialDirectory = _lastDirProcessingOutput
        End If

        If SaveFixationLocationsCheckBox.Checked And MeasureFixationsCheckBox.Checked Then
            MainSaveFileDialog.Title = "Save fixation locations"
            MainSaveFileDialog.FileName = filenameBase & "-FixationLocations"
            MainSaveFileDialog.Filter = "XML (*.xml)|*.xml"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                _lastDirProcessingOutput = MainSaveFileDialog.FileName.Substring(0, MainSaveFileDialog.FileName.LastIndexOf("\") + 1)
                results.saveFixationLocations(MainSaveFileDialog.FileName)
                setStatusMessage("Fixation locations saved to " & MainSaveFileDialog.FileName & ".")
            End If
        End If

        If MeasureFixationsCheckBox.Checked Then
            MainSaveFileDialog.Title = "Save fixation measurements"
            MainSaveFileDialog.FileName = filenameBase & "-FixationMeasurements"
            MainSaveFileDialog.Filter = "Comma-separated values (*.csv)|*.csv"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                _lastDirProcessingOutput = MainSaveFileDialog.FileName.Substring(0, MainSaveFileDialog.FileName.LastIndexOf("\") + 1)
                results.saveFixationMeasurements(SaveFixationCountsCheckBox.Checked, _
                                                 SaveFixationDurationsCheckBox.Checked, _
                                                 MainSaveFileDialog.FileName)
                setStatusMessage("Fixation measurements saved to " & MainSaveFileDialog.FileName & ".")
            End If
        End If

        If SaveSegmentDurationsCheckBox.Checked Then
            MainSaveFileDialog.Title = "Save segment durations"
            MainSaveFileDialog.FileName = filenameBase & "-SegmentDurations"
            MainSaveFileDialog.Filter = "Comma-separated values (*.csv)|*.csv"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                _lastDirProcessingOutput = MainSaveFileDialog.FileName.Substring(0, MainSaveFileDialog.FileName.LastIndexOf("\") + 1)
                results.saveSegmentDurations(MainSaveFileDialog.FileName)
                setStatusMessage("Segment durations saved to " & MainSaveFileDialog.FileName & ".")
            End If
        End If

        If SaveCalibrationErrorCheckBox.Checked Then
            MainSaveFileDialog.Title = "Save calibration error"
            MainSaveFileDialog.FileName = filenameBase & "-CalibrationError"
            MainSaveFileDialog.Filter = "Comma-separated values (*.csv)|*.csv"
            If MainSaveFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                _lastDirProcessingOutput = MainSaveFileDialog.FileName.Substring(0, MainSaveFileDialog.FileName.LastIndexOf("\") + 1)
                results.saveCalibrationError(MainSaveFileDialog.FileName)
                setStatusMessage("Calibration error data saved to " & MainSaveFileDialog.FileName & ".")
            End If
        End If
    End Sub

    Private Sub _videoRecording_FrameImageChanged() Handles _videoRecording.FrameImageChanged

        _isRedrawRequired = True
    End Sub
#End Region

#Region "Static Visualization Tab"

    Dim inputpath As String
    Dim gPoint As List(Of GazePoints)
    Dim _imagePath As String
    Dim vs As New VisualizationClass

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click

        gPoint = New List(Of GazePoints)
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

        _imagePath = MainOpenFileDialog.FileName _
                    .Substring(0, MainOpenFileDialog.FileName.Length _
                               - MainOpenFileDialog.SafeFileName.Length - MainOpenFileDialog.SafeFileName.Length + ("-FixationLocations").Length + 2)
        vs.Gefilename = xmlFileFullName


        gPoint = vs.GetGPFromFile()
        ComboBox1.DataSource = vs.SegmentList
        ComboBox1.DisplayMember = vs.SegmentList(0)

    End Sub

    ''' <summary>
    ''' this used to retrieve stimulus segment snapshot
    ''' </summary>
    ''' <remarks>Abdul-Basit Kasim</remarks>
    ''' 
    Function GetStimulusSegmentPath(ByVal imageStr As String) As String
        Return My.Computer.FileSystem.CombinePath(_imagePath, imageStr & ".png")
    End Function

    Public Sub GetSnapShot()

        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Title = "Select Stimulus segment snapshot"
        MainOpenFileDialog.FileName = ""
        MainOpenFileDialog.Filter = "Processed data (*.png)|*.png"
        Dim SegmentSnapShot As String = ""
        Dim picfilename As String
        If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            SegmentSnapShot = MainOpenFileDialog.FileName
            picfilename = MainOpenFileDialog.SafeFileName
            _lastDirData = MainOpenFileDialog.FileName _
                .Substring(0, MainOpenFileDialog.FileName.Length _
                           - MainOpenFileDialog.SafeFileName.Length)
        End If
        If SegmentSnapShot = "" Then
            SegmentSnapShot = Nothing
            MessageBox.Show("No Stimulus Segment Selected")
            Exit Sub
        End If
        Me.Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Me.Panel1.BackgroundImage = Image.FromFile(SegmentSnapShot)

    End Sub

    Dim swidth As Integer = 567
    Dim sheight As Integer = 425

    Dim _W As Integer = 1920
    Dim _H As Integer = 1080

    Dim xmlFileFullName As String
    Dim xmlfileName As String
    Dim _lastDirData As String


    Dim g1 As Drawing.Graphics
    Dim g2 As Drawing.Graphics

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
        Return plist
    End Function

    Public Function drawPoint(p As Point) As Rectangle
        Return New Rectangle(p.X - 5, p.Y - 5, 10, 10)
    End Function

    Public Sub DrawTrajectory(list As List(Of GazePoints))
        Panel1.Refresh()


        g1 = Panel1.CreateGraphics()
        g2 = Panel1.CreateGraphics()
        Dim redPen As New Pen(Color.Red)

        ' Set the StartCap property.
        redPen.StartCap = Drawing2D.LineCap.RoundAnchor

        ' Set the EndCap property.
        redPen.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim j As Integer = 0
        Dim lp(list.Count) As Point
        For Each p As GazePoints In list

            g1.DrawEllipse(Pens.Red, drawPoint(p.gpoint))
            g1.FillEllipse(Brushes.Aqua, drawPoint(p.gpoint))
            lp(j) = New Point(p.gpoint.X, p.gpoint.Y)

            j += 1

        Next
        g1.DrawLines(redPen, lp)

    End Sub
#End Region

    Private Sub btnLoadSnapShot_Click(sender As Object, e As EventArgs) Handles btnLoadSnapShot.Click
        GetSnapShot()
        ' If ComboBox1.Items.Count > 0 Then
        btnVisualize.Enabled = True
        ' End If
    End Sub
    Dim g As Graphics


    Private Sub btnVisualize_Click(sender As Object, e As EventArgs) Handles btnVisualize.Click

        Dim ifname As String = ComboBox1.Text.Trim
        Dim imgPath As String = GetStimulusSegmentPath(ifname)

        If My.Computer.FileSystem.FileExists(imgPath) Then
            Panel1.BackgroundImageLayout = ImageLayout.Stretch
            Panel1.BackgroundImage = Image.FromFile(imgPath)
        Else
            MessageBox.Show("The image file does not exist", "No stimulus segment snapshot")

        End If

        Dim x As Integer = 240 * swidth / _W
        Dim y As Integer = 0
        Dim aoiwidth As Integer = 1440 * swidth / _W
        Dim aoiHeight As Integer = 90 * sheight / _H
        Dim sortedList As New List(Of GazePoints)
        Dim sgt As String = ComboBox1.Text.Trim()
        sortedList = vs.GetStimulusSegment(sgt, gPoint)

        ListView1.Clear()
        GetPercentages(sortedList)
        Dim cnt As Integer = Get_AOI_LIST(sortedList)
        DrawTrajectory(sortedList)
        g = Panel1.CreateGraphics

        For i As Integer = 0 To cnt - 1
            '  g.FillRectangle(Brushes.Azure, x, y, aoiwidth, aoiHeight)
            g.DrawRectangle(Pens.Blue, x - 2, y, aoiwidth, aoiHeight)
            aoiNamelist.Sort()
            ' Name_AOIs(aoiNamelist(i).ToString, x + aoiwidth, y + 5)
            y = y + aoiHeight

        Next
    End Sub
    Dim aoiNamelist As List(Of String)
    ''' <summary>
    ''' this is use to retrieve The number of AOIs and AOI names
    ''' </summary>
    ''' <param name="segment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Get_AOI_LIST(ByVal segment As List(Of GazePoints)) As Integer
        Dim _count As Integer = 0

        aoiNamelist = New List(Of String)
        aoiNamelist.Clear()
        Dim aoiList = From item As GazePoints In segment
             Select item.aoi
             Distinct



        For Each lst In aoiList
            Dim aname As String = lst
            aoiNamelist.Add(lst)
        Next

        _count = aoiList.Count
        Return _count
    End Function


    Public Sub GetPercentages(ByVal ls As List(Of GazePoints))

        ' Return customers that are grouped based on country. 
        Dim aoiPct = From aoip In ls
                        Order By aoip.aoi
                        Group By aoiname = aoip.aoi
                        Into aoiGrouping = Group, Count(), Average(aoip.duration)
                        Order By aoiname


        ' Output the results. 
        ListView1.Clear()
        ListView1.Columns.Add("AOI's")
        ListView1.Columns.Add("Fixation count", 100)
        ListView1.Columns.Add("Average Duration(ms)", 150)
        ListView1.Columns.Add("Percent")

        For Each a In aoiPct
            ' aoiDictionary.Add(a.aoiname, a.Count)
            ListView1.Items.Add(New ListViewItem(New String() {a.aoiname, a.Count.ToString, a.Average.ToString("N2"), (a.Count * 100 / ls.Count).ToString("N2") & " %"}))
        Next

    End Sub


    Dim g4 As Graphics
    Sub Name_AOIs(ByVal aname As String, ByVal x As Integer, ByVal y As Integer)
        g4 = Panel1.CreateGraphics
        Dim drawString As [String] = aname
        Dim strwidth As Single = 40.0F
        Dim strheight As Single = 90.0F
        ' Create font and brush. 
        Dim drawFont As New Font("Arial", 8)
        Dim drawBrush As New SolidBrush(Color.White)

        ' Create rectangle for drawing. 

        Dim drawRect As New RectangleF(x, y, strwidth, strheight)

        ' Draw rectangle to screen. 
        Dim blackPen As New Pen(Color.White)
        '  g4.DrawRectangle(blackPen, x, y, strwidth, strheight)

        ' Set format of string. 
        Dim drawFormat As New StringFormat
        drawFormat.Alignment = StringAlignment.Center

        ' Draw string to screen.
        g4.DrawString(drawString, drawFont, drawBrush, _
        drawRect, drawFormat)
    End Sub

    '==================Visualization with time (Tab 4)============================== 

    Dim sortedList As List(Of GazePoints)

    ''' <summary>
    ''' Retrieve processed data
    ''' </summary>
    ''' <remarks>done By Abdul-Basit</remarks>
    Dim gpoint1 As List(Of GazePoints)
    Private Sub btnLoadPData_Click(sender As Object, e As EventArgs) Handles btnLoadPData.Click


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

        gpoint1 = New List(Of GazePoints)
        vs = New VisualizationClass

        vs.Gefilename = xmlFileFullName

        gpoint1 = vs.GetGPFromFile()

        Try
            cbxSSegment.Items.Clear()
            cbxSSegment.DataSource = vs.SegmentList
            cbxSSegment.DisplayMember = vs.SegmentList(0)

        Catch ex As Exception

        End Try

    End Sub
    Dim SegmentSnapShot As String = ""
    Dim picfilename As String
   

    Dim clearanceInterval As Integer

    Private Sub btnVisualizeSS_Click(sender As Object, e As EventArgs) Handles btnVisualizeSS.Click

        btnVisualizeSS.Enabled = False
        If Me.Panel2.BackgroundImage Is Nothing Then
            Panel2.BackgroundImageLayout = ImageLayout.Stretch
            Panel2.BackgroundImage = Image.FromFile(GetImage(cbxSSegment.Text.Trim()))
        Else
            Me.Panel2.BackgroundImage = Nothing
            Panel2.BackgroundImageLayout = ImageLayout.Stretch
            Panel2.BackgroundImage = Image.FromFile(GetImage(cbxSSegment.Text.Trim))
        End If

        Dim x As Integer = 240 * swidth / _W
        Dim y As Integer = 0
        Dim aoiwidth As Integer = 1440 * swidth / _W
        Dim aoiHeight As Integer = 90 * sheight / _H
        clearanceInterval = Integer.Parse(TrackBar1.Value)
        Dim sgt As String = cbxSSegment.Text.Trim()
        sortedList = New List(Of GazePoints)

        sortedList = vs.GetStimulusSegment(sgt, gpoint1)
        Dim cnt As Integer = Get_AOI_LIST(sortedList)

        i = 0
        j = 0


        ''aTimer.AutoReset = True
        '' Hook up the Elapsed event for the timer. 
        'AddHandler aTimer.Elapsed, AddressOf OnTimedEvent

        '' Set the Interval to 4 seconds (400 milliseconds).
        'aTimer.Interval = 400
        'aTimer.Enabled = True
        Timer1.Interval = 500
        Timer1.Enabled = True
        Timer1.Start()

    End Sub

    Function GetImage(ByVal imageName As String) As String


        Dim imagageFullPath As String = My.Computer.FileSystem.CombinePath(lastDirectory, imageName & ".png")
        If My.Computer.FileSystem.FileExists(imagageFullPath) Then
            Return imagageFullPath
        Else
            Return Nothing
            MessageBox.Show("The image file does not exist", "No Stimulus Image found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

    End Function



    Private aTimer As System.Timers.Timer
    Dim g12 As Drawing.Graphics


    ' Specify what you want to happen when the Elapsed event is  
    ' raised. 
    'Private Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)

    '    g12 = Panel2.CreateGraphics
    '    Try
    '        If i >= sortedList.Count - 1 Or j > sortedList.Count - 1 Then
    '            aTimer.Stop()
    '            Exit Sub
    '        Else
    '            Me.drawCircles(sortedList(i))
    '            j = i + 1
    '            Me.drawCircles(sortedList(j))
    '            g12.DrawLine(Pens.Blue, sortedList(i).gpoint.X, sortedList(i).gpoint.Y, sortedList(j).gpoint.X, sortedList(j).gpoint.Y)
    '            i += 1

    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try


    'End Sub


    Dim j As Integer
    Dim i As Integer
    Sub plotDynamicGazepoint()

        g12 = Panel2.CreateGraphics
        Try
            If i >= sortedList.Count - 1 Or j > sortedList.Count - 1 Then
                Timer1.Stop()
                Timer1.Enabled = False
                Exit Sub
            Else
                Me.drawCircles(sortedList(i))
                j = i + 1
                Me.drawCircles(sortedList(j))
                g12.DrawLine(Pens.Blue, sortedList(i).gpoint.X, sortedList(i).gpoint.Y, sortedList(j).gpoint.X, sortedList(j).gpoint.Y)
                i += 1

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Public Sub drawCircles(s As GazePoints)

        g = Panel2.CreateGraphics
        g.DrawEllipse(Pens.Red, s.gpoint.X - 5, s.gpoint.Y - 5, 10, 10)
        g.FillEllipse(Brushes.Aqua, s.gpoint.X - 5, s.gpoint.Y - 5, 10, 10)
    End Sub

    Private Sub cbxSSegment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSSegment.SelectedIndexChanged
        Timer1.Stop()
        Timer1.Enabled = False
        btnVisualizeSS.Enabled = True


        'MessageBox.Show("load segment image")
        'btnLoadSnapShot_Click(sender, e)
        'SegmentSnapShot = MainOpenFileDialog.FileName
        'Panel2.BackgroundImage = Image.FromFile(SegmentSnapShot)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnClear.Click
        Panel2.Refresh()
    End Sub

    ''' <summary>
    ''' timer for dynamic display of gaze points
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        plotDynamicGazepoint()

        If (Now.Second Mod clearanceInterval) = 0 Then
            Panel2.Refresh()
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Panel1.BackgroundImage = Nothing
        ListView1.Clear()
        btnVisualize.Enabled = True
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        btnVisualizeSS.Enabled = True
        Timer1.Stop()
        Timer1.Enabled = False
    End Sub

    Dim lastDirectory As String
    Private Sub btnAddRecord_Click(sender As Object, e As EventArgs) Handles btnAddRecord.Click
        lvDirectory.Clear()
        lastDirectory = ChooseFolder()
        ' Make a reference to a directory. 
        Try
            Dim di As New DirectoryInfo(lastDirectory)
            ' Get a reference to each directory in that directory. 
            Dim diArr As DirectoryInfo() = di.GetDirectories()
            ' Display the names of the directories. 
            Dim dri As DirectoryInfo
            For Each dri In diArr
                Dim listviewitemX As New ListViewItem
                listviewitemX.Text = dri.Name
                listviewitemX.SubItems.Add(dri.FullName)
                lvDirectory.Items.Add(listviewitemX)
            Next dri
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try


    End Sub
    Dim _filePath As String

    Public Function buildFilePath(ByVal _fname As String) As String
        Dim _filePath As String = _fname + "-FixationLocations"
        Dim fileFullPath As String = My.Computer.FileSystem.CombinePath(lastDirectory, _filePath)

        Return fileFullPath
        
    End Function

    Public Function ChooseFolder() As String
        FolderBrowserDialog1.ShowNewFolderButton = False
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then

            Dim root As Environment.SpecialFolder = FolderBrowserDialog1.RootFolder

            Return FolderBrowserDialog1.SelectedPath
        End If
    End Function


    Private Sub lvDirectory_DoubleClick(sender As Object, e As EventArgs) Handles lvDirectory.DoubleClick


        Dim result As DialogResult = MessageBox.Show("Loading new data will clear the old data entry, do you still want to continue ?", _
                                                   "Load data", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            cbxSSegment.DataSource = Nothing
            Dim txtValue As String
            txtValue = lvDirectory.FocusedItem.Text.Trim() & "\" & lvDirectory.FocusedItem.Text.Trim() & "-FixationLocations.xml"

            
            Dim fileFullPath As String = My.Computer.FileSystem.CombinePath(lastDirectory, txtValue)

            'Dim f As String = buildFilePath(txtValue)



            gpoint1 = New List(Of GazePoints)

            vs = New VisualizationClass
            vs.Gefilename = fileFullPath
            gpoint1 = vs.GetGPFromFile()

            Try
                cbxSSegment.Items.Clear()
                cbxSSegment.DataSource = vs.SegmentList
                cbxSSegment.DisplayMember = vs.SegmentList(0)

            Catch ex As Exception

            End Try
        Else
            Exit Sub
        End If



    End Sub



    '====================================TAB 5 (AOI VISUALIZATION)================================================


#Region "AOI Visualiztion (Tab 5 )"


    Public Structure aoiStructure
        Dim postion As Point
        Dim name As String
    End Structure

    Dim aw As Single
    Dim ah As Single

    ''' <summary>
    ''' this function is to draw the AOI rectangles
    ''' </summary>
    ''' <param name="sx"></param>
    ''' <param name="sy"></param>
    ''' <param name="w"></param>
    ''' <param name="h"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function drRec(ByVal sx As Integer, ByVal sy As Integer, w As Integer, h As Integer) As Rectangle
        Return New Rectangle(sx, sy, w, h)
    End Function

    Dim gpointList As List(Of GazePoints)
    Private Sub btnLoadData_aoi_Click(sender As Object, e As EventArgs) Handles btnLoadData_aoi.Click

        Dim result As DialogResult = MessageBox.Show("Loading new data will clear the old data entry, do you still want to continue ?", _
                                                     "Load data", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = Windows.Forms.DialogResult.Yes Then
            picSegment.Image = Nothing
            cboSS_aoi.DataSource = Nothing

            cboSS_aoi.Items.Clear()
            aoiPanel.Refresh()
            gpointList = New List(Of GazePoints)

            MainOpenFileDialog.FileName = ""
            MainOpenFileDialog.Title = "Select eye-tracker data"
            MainOpenFileDialog.FileName = ""
            MainOpenFileDialog.Filter = "Processed data (*.xml)|*.xml"


            If MainOpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                xmlFileFullName = MainOpenFileDialog.FileName
                xmlfileName = MainOpenFileDialog.SafeFileName
                _lastDirData = MainOpenFileDialog.FileName _
                    .Substring(0, MainOpenFileDialog.FileName.Length _
                               - MainOpenFileDialog.SafeFileName.Length - MainOpenFileDialog.SafeFileName.Length + ("-FixationLocations.xml").Length - 2)
            End If

            gpointList = New List(Of GazePoints)

            vs.Gefilename = xmlFileFullName
            gpointList = vs.GetGPFromFile()

            Try
                cboSS_aoi.Items.Clear()
                cboSS_aoi.DataSource = vs.SegmentList
                cboSS_aoi.DisplayMember = vs.SegmentList(0)

            Catch ex As Exception

            End Try
        Else
            Exit Sub
        End If


    End Sub

    Dim AOISortedList As List(Of GazePoints)
    Dim cnt As Integer 'number of gazepoints per segment. By Abdul-Basit kasim
    Dim _cnt As Integer 'number of AOIs per segment.By Abdul-Basit kasim
    Dim aoiGfx As Graphics

    Dim ListOfAoi As List(Of aoiStructure)


    ''' <summary>
    ''' the function below returns the path to the stimulus segment image in the same folder where the data was stored.
    ''' </summary>
    ''' <param name="imageStr"></param>
    ''' <returns></returns>
    ''' <remarks>abdul-basit Kasim</remarks>
    Function DisplayStimulusSegment(ByVal imageStr As String) As String
        Return My.Computer.FileSystem.CombinePath(_lastDirData, imageStr & ".png")
    End Function

    ''' <summary>
    '''This event displays the AOI transition matrix, label the AOIs and draw trajectories.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Abdul-basit Kasim</remarks>
    ''' 
    Private Sub btnGetAOI_Click(sender As Object, e As EventArgs) Handles btnGetAOI.Click
        btnClearAll.Enabled = True
        If cboSS_aoi.Text.Trim = "" Then
            picSegment.Image = Nothing
        Else
            'check for fileNotFoundExceptoin
            Try
                picSegment.Image = Image.FromFile(DisplayStimulusSegment(cboSS_aoi.Text.Trim()))
            Catch ex As Exception
                MessageBox.Show("The path to the images folder is not set properly", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                picSegment.Image = Nothing
            End Try

        End If

        ListOfAoi = New List(Of aoiStructure)
        AOISortedList = New List(Of GazePoints)
        Dim xstart As Integer
        Dim ystart As Integer = 10
        Dim displayWidth As Integer = aoiPanel.Width - 10
        Dim displayHeight As Integer = aoiPanel.Height - 10

        aoiGfx = aoiPanel.CreateGraphics
        Dim nrow As Integer 'number of rows
        Dim ncol As Integer 'number of columns
        Dim y As Integer 'as counters

        Dim sgmt As String = cboSS_aoi.Text.Trim
        AOISortedList = vs.GetStimulusSegment(sgmt, gpointList)
        cnt = AOISortedList.Count

        _cnt = Get_AOI_LIST(AOISortedList)

        Dim aois As New List(Of String)

        aois = aoiNamelist

        lblAOI.Text = "There are " & _cnt.ToString & " AOIs"

        If _cnt > 10 Then
            aw = displayWidth / 8
            ncol = 4
            nrow = _cnt / ncol
        Else
            aw = displayWidth / 6
            ncol = 3
            nrow = _cnt / ncol
        End If


        aoiPanel.Refresh()

        Dim numAoi As Integer = _cnt

        If (_cnt Mod 3) = 0 Or (_cnt Mod 4 = 0) Then
            ah = displayHeight / (2 * nrow)
        Else
            nrow = nrow + 1
            ah = displayHeight / (2 * nrow)
        End If

        'creating the AOI rectangles
        Dim m As Integer = 0
        Dim AoiStruct As aoiStructure

        For x = 0 To nrow - 1
            If numAoi >= ncol Then
            Else
                ncol = ncol - numAoi
            End If

            xstart = 15
            For y = 0 To ncol - 1

                aoiGfx.FillRectangle(Brushes.Aqua, drRec(xstart, ystart, aw, ah))
                aoiGfx.DrawRectangle(Pens.Blue, drRec(xstart, ystart, aw, ah))
                If m = _cnt Then
                    Exit For
                End If
                write_AOI_names(aoiNamelist(m), xstart + 5, ystart + 5)
                AoiStruct.postion = New Point(xstart, ystart)
                AoiStruct.name = aoiNamelist(m).ToString
                ListOfAoi.Add(AoiStruct)

                m += 1
                numAoi -= 1

                xstart += 2 * aw

            Next
            ystart += ah * 2

        Next

        TransitionPoints = GetTransition(AOISortedList, ListOfAoi)

        DrawTrajectory()

        'the code below Generates array for the drawing of lines. this is for testing, must be deleted later
        '===============================================
        'Dim v As Integer = 0
        'Dim larray(TransitionPoints.Count) As Point
        'For v = 0 To TransitionPoints.Count - 1
        '    larray(v) = TransitionPoints(v)


        'Next

        'Dim gfxLines As Graphics
        'gfxLines = aoiPanel.CreateGraphics
        'gfxLines.DrawLines(Pens.Blue, larray)
        '================================================
    End Sub
    Dim TransitionPoints As List(Of Point)

    Private Sub cboSS_aoi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSS_aoi.SelectedIndexChanged
        btnGetAOI.Enabled = True
        lblAOI.Text = ""
    End Sub

    Dim Gfx As Graphics
    Sub write_AOI_names(ByVal aname As String, ByVal x As Integer, ByVal y As Integer)
        Gfx = aoiPanel.CreateGraphics
        Dim drawString As [String] = aname
        Dim strwidth As Single = 100.0F
        Dim strheight As Single = 90.0F
        ' Create font and brush. 
        Dim drawFont As New Font("Arial", 10)
        Dim drawBrush As New SolidBrush(Color.Blue)

        ' Create rectangle for drawing. 

        Dim drawRect As New RectangleF(x, y, strwidth, strheight)

        ' Draw rectangle to screen. 
        'Dim whitePen As New Pen(Color.White)
        '  g4.DrawRectangle(blackPen, x, y, strwidth, strheight)

        ' Set format of string. 
        Dim drawFormat As New StringFormat
        drawFormat.Alignment = StringAlignment.Center

        ' Draw string to screen.
        Gfx.DrawString(drawString, drawFont, drawBrush, _
        drawRect, drawFormat)
    End Sub

    ''' <summary>
    ''' this is a Structure used to create collection of transitions
    ''' </summary>
    ''' <remarks>By Abdul-Basit kasim</remarks>
    Public Structure gTransition
        Dim startPoint As Point
        Dim endPoint As Point
        Dim _name As String
    End Structure
    Dim t As gTransition
    Dim pointArray As List(Of Point)

    ''' <summary>
    ''' GetTransition function is used to create the AOI rectangles on the screen, 
    ''' proportionate to the number of aois
    ''' </summary>
    ''' <param name="gpl"> this parameter is the list of gaze points </param>
    ''' <param name="_aoi">this is the list of aoi per stimulus segment</param>
    ''' <returns></returns>
    ''' <remarks>By Abdul-Basit Kasim</remarks>

    Function GetTransition(ByVal gpl As List(Of GazePoints), ByVal _aoi As List(Of aoiStructure)) As List(Of Point)

        Dim k As Integer = _aoi.Count
        pointArray = New List(Of Point)
        Dim h As Integer = 0
        Dim ar As New List(Of String)
        For Each p In gpl
            ar.Add(p.aoi)
        Next
        Dim l As Integer = 0
        While h < gpl.Count
            l = 0

            'this while loop forms the transition

            While ar(h) <> _aoi(l).name
                If l < k Then
                    l += 1
                End If
            End While

            pointArray.Add(New Point(_aoi(l).postion.X + 0.5 * aw, _aoi(l).postion.Y + 0.5 * ah))
            h += 1
        End While


        Return pointArray
    End Function

    ''' <summary>
    ''' This function generates all the transtioins between AOIs
    ''' </summary>
    ''' 
    ''' <returns>returns a list of Transtion trajectories</returns>
    ''' <remarks>By Abdul-Basit kasim</remarks>


    Public Function GetTrajectory() As List(Of gTransition)

        Dim listOfPoints As New List(Of Point)
        listOfPoints = TransitionPoints
        Dim trajList As New List(Of gTransition)
        Dim listCount As Integer = listOfPoints.Count
        Dim i As Integer = 0
        Dim j As Integer = 0

        While listCount > 0

            Dim t As New gTransition

            If listCount = 1 Then
                t.startPoint = listOfPoints(0)
                t.endPoint = listOfPoints(0)
            ElseIf listCount Then
                j = i + 1
                t.startPoint = listOfPoints(i)
                t.endPoint = listOfPoints(j)
            End If
            trajList.Add(t)
            listCount -= 1
            i += 1
        End While
        Return trajList
    End Function

    ''' <summary>
    ''' This sub procedure draws the AOI's transition in tab 5 ( AOI TRANSITIONS )
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DrawTrajectory()

        'FURTHER PROCESSING SHOULD BE DONE HERE ABOUT AOI LOCATION
        ' Create a new custom pen. 
        Dim listTraj As New List(Of gTransition)
        listTraj = GetTrajectory()

        'Dim picturebox2 As New PictureBox
        Dim redPen As New Pen(Brushes.BlueViolet, 1.5F)
        ' Dim tbmp As New Bitmap(588, 410)

        ' Dim gfx As Drawing.Graphics = Drawing.Graphics.FromImage(tbmp)

        Dim gline As Graphics
        gline = aoiPanel.CreateGraphics
        ' Set the StartCap property.
        redPen.StartCap = Drawing2D.LineCap.Triangle
        ' Set the EndCap property.
        redPen.EndCap = Drawing2D.LineCap.ArrowAnchor
        Dim k As Integer = listTraj.Count
        Dim i As Integer = 0

        If chkCurves.Checked Then
            Dim f As Integer = TransitionPoints.Count
            Dim arr(f) As Point

            For i = 0 To f - 1

                arr(i) = TransitionPoints.Item(i)

            Next
            gline.DrawCurve(Pens.Red, arr, 0.3)

        Else
            While k > 0
                'gfx.DrawEllipse(redPen, Lst())
                gline.DrawLine(redPen, listTraj(i).startPoint, listTraj(i).endPoint)
                i += 1
                k -= 1
            End While
        End If

    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        picSegment.Image = Nothing
        cboSS_aoi.DataSource = Nothing
        aoiPanel.Refresh()
        btnClearAll.Enabled = False
    End Sub
#End Region


    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        clearanceInterval = Integer.Parse(TrackBar1.Value)
    End Sub
End Class
