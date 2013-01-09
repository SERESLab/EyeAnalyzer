<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.VideoPictureBox = New System.Windows.Forms.PictureBox()
        Me.AoiGroupBox = New System.Windows.Forms.GroupBox()
        Me.AoiHeightUpDown = New System.Windows.Forms.NumericUpDown()
        Me.AoiWidthUpDown = New System.Windows.Forms.NumericUpDown()
        Me.AoiYUpDown = New System.Windows.Forms.NumericUpDown()
        Me.AoiXUpDown = New System.Windows.Forms.NumericUpDown()
        Me.AoiNonexclusiveCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.AoiNameTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteAoiButton = New System.Windows.Forms.Button()
        Me.AddRenameAoiButton = New System.Windows.Forms.Button()
        Me.AoiListBox = New System.Windows.Forms.ListBox()
        Me.AoiColorLabel = New System.Windows.Forms.Label()
        Me.NewAoiColorLabel = New System.Windows.Forms.Label()
        Me.NonExclusiveAoiColorLabel = New System.Windows.Forms.Label()
        Me.SelectedAoiColorLabel = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.MainMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenStudyDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImportStimulusSegmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportStimulusSegmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeatmapsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateNewHeatmapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsGroupBox = New System.Windows.Forms.GroupBox()
        Me.MeasureFixationsGroupBox = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FixationDurationTextBox = New System.Windows.Forms.TextBox()
        Me.SaveFixationDurationsCheckBox = New System.Windows.Forms.CheckBox()
        Me.SaveFixationCountsCheckBox = New System.Windows.Forms.CheckBox()
        Me.MeasureFixationsCheckBox = New System.Windows.Forms.CheckBox()
        Me.SaveFixationLocationsCheckBox = New System.Windows.Forms.CheckBox()
        Me.SaveSegmentDurationsCheckBox = New System.Windows.Forms.CheckBox()
        Me.SaveCalibrationErrorCheckBox = New System.Windows.Forms.CheckBox()
        Me.ProcessButton = New System.Windows.Forms.Button()
        Me.MainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.MainStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.VideoFileStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.XmlFileStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SegmentsGroupBox = New System.Windows.Forms.GroupBox()
        Me.SegmentEndLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.UnselectSegmentButton = New System.Windows.Forms.Button()
        Me.SegmentStartLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.SegmentEndButton = New System.Windows.Forms.Button()
        Me.SegmentStartButton = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SegmentNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddUpdateSegmentButton = New System.Windows.Forms.Button()
        Me.DeleteSegmentButton = New System.Windows.Forms.Button()
        Me.SegmentsListBox = New System.Windows.Forms.ListBox()
        Me.VideoPositionTrackBar = New System.Windows.Forms.TrackBar()
        Me.VideoPositionUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.VideoGroupBox = New System.Windows.Forms.GroupBox()
        Me.VideoPanel = New System.Windows.Forms.Panel()
        Me.VideoActualSizeCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.VideoPositionLabel = New System.Windows.Forms.Label()
        Me.SaveScreenshotButton = New System.Windows.Forms.Button()
        Me.MainOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.RedrawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MainSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MainColorDialog = New System.Windows.Forms.ColorDialog()
        Me.DisplaySettingsGroupBox = New System.Windows.Forms.GroupBox()
        Me.AoiOpacityTrackBar = New System.Windows.Forms.TrackBar()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.AxWindowsMediaPlayer1 = New AxWMPLib.AxWindowsMediaPlayer()
        CType(Me.VideoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AoiGroupBox.SuspendLayout()
        CType(Me.AoiHeightUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AoiWidthUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AoiYUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AoiXUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenuStrip.SuspendLayout()
        Me.SettingsGroupBox.SuspendLayout()
        Me.MeasureFixationsGroupBox.SuspendLayout()
        Me.MainStatusStrip.SuspendLayout()
        Me.SegmentsGroupBox.SuspendLayout()
        CType(Me.VideoPositionTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VideoPositionUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VideoGroupBox.SuspendLayout()
        Me.VideoPanel.SuspendLayout()
        Me.DisplaySettingsGroupBox.SuspendLayout()
        CType(Me.AoiOpacityTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VideoPictureBox
        '
        Me.VideoPictureBox.BackColor = System.Drawing.Color.Black
        Me.VideoPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.VideoPictureBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoPictureBox.Name = "VideoPictureBox"
        Me.VideoPictureBox.Size = New System.Drawing.Size(640, 360)
        Me.VideoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.VideoPictureBox.TabIndex = 0
        Me.VideoPictureBox.TabStop = False
        '
        'AoiGroupBox
        '
        Me.AoiGroupBox.Controls.Add(Me.AoiHeightUpDown)
        Me.AoiGroupBox.Controls.Add(Me.AoiWidthUpDown)
        Me.AoiGroupBox.Controls.Add(Me.AoiYUpDown)
        Me.AoiGroupBox.Controls.Add(Me.AoiXUpDown)
        Me.AoiGroupBox.Controls.Add(Me.AoiNonexclusiveCheckBox)
        Me.AoiGroupBox.Controls.Add(Me.Label14)
        Me.AoiGroupBox.Controls.Add(Me.Label13)
        Me.AoiGroupBox.Controls.Add(Me.Label12)
        Me.AoiGroupBox.Controls.Add(Me.Label11)
        Me.AoiGroupBox.Controls.Add(Me.Label7)
        Me.AoiGroupBox.Controls.Add(Me.AoiNameTextBox)
        Me.AoiGroupBox.Controls.Add(Me.DeleteAoiButton)
        Me.AoiGroupBox.Controls.Add(Me.AddRenameAoiButton)
        Me.AoiGroupBox.Controls.Add(Me.AoiListBox)
        Me.AoiGroupBox.Enabled = False
        Me.AoiGroupBox.Location = New System.Drawing.Point(12, 202)
        Me.AoiGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiGroupBox.Name = "AoiGroupBox"
        Me.AoiGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiGroupBox.Size = New System.Drawing.Size(240, 180)
        Me.AoiGroupBox.TabIndex = 1
        Me.AoiGroupBox.TabStop = False
        Me.AoiGroupBox.Text = "AOIs"
        '
        'AoiHeightUpDown
        '
        Me.AoiHeightUpDown.Enabled = False
        Me.AoiHeightUpDown.Location = New System.Drawing.Point(182, 113)
        Me.AoiHeightUpDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiHeightUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.AoiHeightUpDown.Name = "AoiHeightUpDown"
        Me.AoiHeightUpDown.Size = New System.Drawing.Size(54, 20)
        Me.AoiHeightUpDown.TabIndex = 14
        Me.AoiHeightUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'AoiWidthUpDown
        '
        Me.AoiWidthUpDown.Enabled = False
        Me.AoiWidthUpDown.Location = New System.Drawing.Point(182, 90)
        Me.AoiWidthUpDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiWidthUpDown.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.AoiWidthUpDown.Name = "AoiWidthUpDown"
        Me.AoiWidthUpDown.Size = New System.Drawing.Size(54, 20)
        Me.AoiWidthUpDown.TabIndex = 14
        Me.AoiWidthUpDown.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'AoiYUpDown
        '
        Me.AoiYUpDown.Enabled = False
        Me.AoiYUpDown.Location = New System.Drawing.Point(182, 67)
        Me.AoiYUpDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiYUpDown.Name = "AoiYUpDown"
        Me.AoiYUpDown.Size = New System.Drawing.Size(54, 20)
        Me.AoiYUpDown.TabIndex = 14
        '
        'AoiXUpDown
        '
        Me.AoiXUpDown.Enabled = False
        Me.AoiXUpDown.Location = New System.Drawing.Point(182, 45)
        Me.AoiXUpDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiXUpDown.Name = "AoiXUpDown"
        Me.AoiXUpDown.Size = New System.Drawing.Size(54, 20)
        Me.AoiXUpDown.TabIndex = 14
        '
        'AoiNonexclusiveCheckBox
        '
        Me.AoiNonexclusiveCheckBox.AutoSize = True
        Me.AoiNonexclusiveCheckBox.Location = New System.Drawing.Point(147, 144)
        Me.AoiNonexclusiveCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiNonexclusiveCheckBox.Name = "AoiNonexclusiveCheckBox"
        Me.AoiNonexclusiveCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AoiNonexclusiveCheckBox.Size = New System.Drawing.Size(93, 17)
        Me.AoiNonexclusiveCheckBox.TabIndex = 13
        Me.AoiNonexclusiveCheckBox.Text = "Non-exclusive"
        Me.AoiNonexclusiveCheckBox.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(137, 113)
        Me.Label14.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Height:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(141, 90)
        Me.Label13.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Width:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(161, 67)
        Me.Label12.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Y:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(161, 45)
        Me.Label11.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 13)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "X:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 20)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Name:"
        '
        'AoiNameTextBox
        '
        Me.AoiNameTextBox.Location = New System.Drawing.Point(46, 17)
        Me.AoiNameTextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiNameTextBox.Name = "AoiNameTextBox"
        Me.AoiNameTextBox.Size = New System.Drawing.Size(97, 20)
        Me.AoiNameTextBox.TabIndex = 3
        '
        'DeleteAoiButton
        '
        Me.DeleteAoiButton.Enabled = False
        Me.DeleteAoiButton.Location = New System.Drawing.Point(7, 150)
        Me.DeleteAoiButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DeleteAoiButton.Name = "DeleteAoiButton"
        Me.DeleteAoiButton.Size = New System.Drawing.Size(98, 24)
        Me.DeleteAoiButton.TabIndex = 2
        Me.DeleteAoiButton.Text = "Delete Selected"
        Me.DeleteAoiButton.UseVisualStyleBackColor = True
        '
        'AddRenameAoiButton
        '
        Me.AddRenameAoiButton.Enabled = False
        Me.AddRenameAoiButton.Location = New System.Drawing.Point(146, 15)
        Me.AddRenameAoiButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AddRenameAoiButton.Name = "AddRenameAoiButton"
        Me.AddRenameAoiButton.Size = New System.Drawing.Size(89, 23)
        Me.AddRenameAoiButton.TabIndex = 1
        Me.AddRenameAoiButton.Text = "Add New"
        Me.AddRenameAoiButton.UseVisualStyleBackColor = True
        '
        'AoiListBox
        '
        Me.AoiListBox.FormattingEnabled = True
        Me.AoiListBox.Location = New System.Drawing.Point(7, 45)
        Me.AoiListBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiListBox.Name = "AoiListBox"
        Me.AoiListBox.Size = New System.Drawing.Size(118, 95)
        Me.AoiListBox.TabIndex = 0
        '
        'AoiColorLabel
        '
        Me.AoiColorLabel.BackColor = System.Drawing.Color.Blue
        Me.AoiColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.AoiColorLabel.Location = New System.Drawing.Point(11, 29)
        Me.AoiColorLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AoiColorLabel.Name = "AoiColorLabel"
        Me.AoiColorLabel.Size = New System.Drawing.Size(15, 15)
        Me.AoiColorLabel.TabIndex = 8
        '
        'NewAoiColorLabel
        '
        Me.NewAoiColorLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.NewAoiColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.NewAoiColorLabel.Location = New System.Drawing.Point(11, 15)
        Me.NewAoiColorLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.NewAoiColorLabel.Name = "NewAoiColorLabel"
        Me.NewAoiColorLabel.Size = New System.Drawing.Size(15, 15)
        Me.NewAoiColorLabel.TabIndex = 8
        '
        'NonExclusiveAoiColorLabel
        '
        Me.NonExclusiveAoiColorLabel.BackColor = System.Drawing.Color.Green
        Me.NonExclusiveAoiColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.NonExclusiveAoiColorLabel.Location = New System.Drawing.Point(11, 44)
        Me.NonExclusiveAoiColorLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.NonExclusiveAoiColorLabel.Name = "NonExclusiveAoiColorLabel"
        Me.NonExclusiveAoiColorLabel.Size = New System.Drawing.Size(15, 15)
        Me.NonExclusiveAoiColorLabel.TabIndex = 8
        '
        'SelectedAoiColorLabel
        '
        Me.SelectedAoiColorLabel.BackColor = System.Drawing.Color.Maroon
        Me.SelectedAoiColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SelectedAoiColorLabel.Location = New System.Drawing.Point(11, 58)
        Me.SelectedAoiColorLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SelectedAoiColorLabel.Name = "SelectedAoiColorLabel"
        Me.SelectedAoiColorLabel.Size = New System.Drawing.Size(15, 15)
        Me.SelectedAoiColorLabel.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(31, 29)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Exclusive AOI"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(31, 44)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(95, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Non-exclusive AOI"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(31, 58)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Selected AOI"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(31, 15)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "New AOI"
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HeatmapsToolStripMenuItem})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MainMenuStrip.Size = New System.Drawing.Size(1187, 24)
        Me.MainMenuStrip.TabIndex = 1
        Me.MainMenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenStudyDataToolStripMenuItem, Me.ToolStripSeparator2, Me.ImportStimulusSegmentsToolStripMenuItem, Me.ExportStimulusSegmentsToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'OpenStudyDataToolStripMenuItem
        '
        Me.OpenStudyDataToolStripMenuItem.Name = "OpenStudyDataToolStripMenuItem"
        Me.OpenStudyDataToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.OpenStudyDataToolStripMenuItem.Text = "&Load Study Data"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(211, 6)
        '
        'ImportStimulusSegmentsToolStripMenuItem
        '
        Me.ImportStimulusSegmentsToolStripMenuItem.Enabled = False
        Me.ImportStimulusSegmentsToolStripMenuItem.Name = "ImportStimulusSegmentsToolStripMenuItem"
        Me.ImportStimulusSegmentsToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ImportStimulusSegmentsToolStripMenuItem.Text = "&Import Stimulus Segments"
        '
        'ExportStimulusSegmentsToolStripMenuItem
        '
        Me.ExportStimulusSegmentsToolStripMenuItem.Enabled = False
        Me.ExportStimulusSegmentsToolStripMenuItem.Name = "ExportStimulusSegmentsToolStripMenuItem"
        Me.ExportStimulusSegmentsToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ExportStimulusSegmentsToolStripMenuItem.Text = "&Export Stimulus Segments"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(211, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'HeatmapsToolStripMenuItem
        '
        Me.HeatmapsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateNewHeatmapToolStripMenuItem})
        Me.HeatmapsToolStripMenuItem.Name = "HeatmapsToolStripMenuItem"
        Me.HeatmapsToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.HeatmapsToolStripMenuItem.Text = "Heat&maps"
        '
        'GenerateNewHeatmapToolStripMenuItem
        '
        Me.GenerateNewHeatmapToolStripMenuItem.Name = "GenerateNewHeatmapToolStripMenuItem"
        Me.GenerateNewHeatmapToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.GenerateNewHeatmapToolStripMenuItem.Text = "&Generate Heatmaps"
        '
        'SettingsGroupBox
        '
        Me.SettingsGroupBox.Controls.Add(Me.MeasureFixationsGroupBox)
        Me.SettingsGroupBox.Controls.Add(Me.MeasureFixationsCheckBox)
        Me.SettingsGroupBox.Controls.Add(Me.SaveFixationLocationsCheckBox)
        Me.SettingsGroupBox.Controls.Add(Me.SaveSegmentDurationsCheckBox)
        Me.SettingsGroupBox.Controls.Add(Me.SaveCalibrationErrorCheckBox)
        Me.SettingsGroupBox.Controls.Add(Me.ProcessButton)
        Me.SettingsGroupBox.Location = New System.Drawing.Point(794, 25)
        Me.SettingsGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SettingsGroupBox.Name = "SettingsGroupBox"
        Me.SettingsGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SettingsGroupBox.Size = New System.Drawing.Size(164, 223)
        Me.SettingsGroupBox.TabIndex = 3
        Me.SettingsGroupBox.TabStop = False
        Me.SettingsGroupBox.Text = "Processing Settings"
        '
        'MeasureFixationsGroupBox
        '
        Me.MeasureFixationsGroupBox.Controls.Add(Me.Label1)
        Me.MeasureFixationsGroupBox.Controls.Add(Me.FixationDurationTextBox)
        Me.MeasureFixationsGroupBox.Controls.Add(Me.SaveFixationDurationsCheckBox)
        Me.MeasureFixationsGroupBox.Controls.Add(Me.SaveFixationCountsCheckBox)
        Me.MeasureFixationsGroupBox.Location = New System.Drawing.Point(7, 60)
        Me.MeasureFixationsGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MeasureFixationsGroupBox.Name = "MeasureFixationsGroupBox"
        Me.MeasureFixationsGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MeasureFixationsGroupBox.Size = New System.Drawing.Size(149, 83)
        Me.MeasureFixationsGroupBox.TabIndex = 6
        Me.MeasureFixationsGroupBox.TabStop = False
        Me.MeasureFixationsGroupBox.Text = "Fixations"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 19)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Minimum fixation (ms):"
        '
        'FixationDurationTextBox
        '
        Me.FixationDurationTextBox.Location = New System.Drawing.Point(119, 17)
        Me.FixationDurationTextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.FixationDurationTextBox.Name = "FixationDurationTextBox"
        Me.FixationDurationTextBox.Size = New System.Drawing.Size(24, 20)
        Me.FixationDurationTextBox.TabIndex = 2
        Me.FixationDurationTextBox.Text = "60"
        '
        'SaveFixationDurationsCheckBox
        '
        Me.SaveFixationDurationsCheckBox.AutoSize = True
        Me.SaveFixationDurationsCheckBox.Checked = True
        Me.SaveFixationDurationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveFixationDurationsCheckBox.Location = New System.Drawing.Point(7, 61)
        Me.SaveFixationDurationsCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveFixationDurationsCheckBox.Name = "SaveFixationDurationsCheckBox"
        Me.SaveFixationDurationsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SaveFixationDurationsCheckBox.Size = New System.Drawing.Size(100, 14)
        Me.SaveFixationDurationsCheckBox.TabIndex = 4
        Me.SaveFixationDurationsCheckBox.Text = "Save fixation durations"
        Me.SaveFixationDurationsCheckBox.UseVisualStyleBackColor = True
        '
        'SaveFixationCountsCheckBox
        '
        Me.SaveFixationCountsCheckBox.AutoSize = True
        Me.SaveFixationCountsCheckBox.Checked = True
        Me.SaveFixationCountsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveFixationCountsCheckBox.Location = New System.Drawing.Point(7, 39)
        Me.SaveFixationCountsCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveFixationCountsCheckBox.Name = "SaveFixationCountsCheckBox"
        Me.SaveFixationCountsCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SaveFixationCountsCheckBox.Size = New System.Drawing.Size(92, 14)
        Me.SaveFixationCountsCheckBox.TabIndex = 4
        Me.SaveFixationCountsCheckBox.Text = "Save fixation counts"
        Me.SaveFixationCountsCheckBox.UseVisualStyleBackColor = True
        '
        'MeasureFixationsCheckBox
        '
        Me.MeasureFixationsCheckBox.AutoSize = True
        Me.MeasureFixationsCheckBox.Checked = True
        Me.MeasureFixationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MeasureFixationsCheckBox.Location = New System.Drawing.Point(14, 40)
        Me.MeasureFixationsCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MeasureFixationsCheckBox.Name = "MeasureFixationsCheckBox"
        Me.MeasureFixationsCheckBox.Size = New System.Drawing.Size(81, 14)
        Me.MeasureFixationsCheckBox.TabIndex = 5
        Me.MeasureFixationsCheckBox.Text = "Measure fixations"
        Me.MeasureFixationsCheckBox.UseVisualStyleBackColor = True
        '
        'SaveFixationLocationsCheckBox
        '
        Me.SaveFixationLocationsCheckBox.AutoSize = True
        Me.SaveFixationLocationsCheckBox.Checked = True
        Me.SaveFixationLocationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveFixationLocationsCheckBox.Location = New System.Drawing.Point(14, 17)
        Me.SaveFixationLocationsCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveFixationLocationsCheckBox.Name = "SaveFixationLocationsCheckBox"
        Me.SaveFixationLocationsCheckBox.Size = New System.Drawing.Size(99, 14)
        Me.SaveFixationLocationsCheckBox.TabIndex = 5
        Me.SaveFixationLocationsCheckBox.Text = "Save fixation locations"
        Me.SaveFixationLocationsCheckBox.UseVisualStyleBackColor = True
        '
        'SaveSegmentDurationsCheckBox
        '
        Me.SaveSegmentDurationsCheckBox.AutoSize = True
        Me.SaveSegmentDurationsCheckBox.Checked = True
        Me.SaveSegmentDurationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveSegmentDurationsCheckBox.Location = New System.Drawing.Point(14, 151)
        Me.SaveSegmentDurationsCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveSegmentDurationsCheckBox.Name = "SaveSegmentDurationsCheckBox"
        Me.SaveSegmentDurationsCheckBox.Size = New System.Drawing.Size(105, 14)
        Me.SaveSegmentDurationsCheckBox.TabIndex = 5
        Me.SaveSegmentDurationsCheckBox.Text = "Save segment durations"
        Me.SaveSegmentDurationsCheckBox.UseVisualStyleBackColor = True
        '
        'SaveCalibrationErrorCheckBox
        '
        Me.SaveCalibrationErrorCheckBox.AutoSize = True
        Me.SaveCalibrationErrorCheckBox.Checked = True
        Me.SaveCalibrationErrorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.SaveCalibrationErrorCheckBox.Location = New System.Drawing.Point(14, 173)
        Me.SaveCalibrationErrorCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveCalibrationErrorCheckBox.Name = "SaveCalibrationErrorCheckBox"
        Me.SaveCalibrationErrorCheckBox.Size = New System.Drawing.Size(94, 14)
        Me.SaveCalibrationErrorCheckBox.TabIndex = 5
        Me.SaveCalibrationErrorCheckBox.Text = "Save calibration error"
        Me.SaveCalibrationErrorCheckBox.UseVisualStyleBackColor = True
        '
        'ProcessButton
        '
        Me.ProcessButton.Enabled = False
        Me.ProcessButton.Location = New System.Drawing.Point(14, 195)
        Me.ProcessButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.ProcessButton.Name = "ProcessButton"
        Me.ProcessButton.Size = New System.Drawing.Size(137, 24)
        Me.ProcessButton.TabIndex = 3
        Me.ProcessButton.Text = "Process Eye Data"
        Me.ProcessButton.UseVisualStyleBackColor = True
        '
        'MainStatusStrip
        '
        Me.MainStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainStatusLabel, Me.VideoFileStatusLabel, Me.XmlFileStatusLabel})
        Me.MainStatusStrip.Location = New System.Drawing.Point(0, 423)
        Me.MainStatusStrip.Name = "MainStatusStrip"
        Me.MainStatusStrip.Padding = New System.Windows.Forms.Padding(1, 0, 10, 0)
        Me.MainStatusStrip.Size = New System.Drawing.Size(1187, 24)
        Me.MainStatusStrip.SizingGrip = False
        Me.MainStatusStrip.TabIndex = 4
        Me.MainStatusStrip.Text = "StatusStrip1"
        '
        'MainStatusLabel
        '
        Me.MainStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.MainStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.MainStatusLabel.Name = "MainStatusLabel"
        Me.MainStatusLabel.Size = New System.Drawing.Size(1136, 19)
        Me.MainStatusLabel.Spring = True
        Me.MainStatusLabel.Text = "..."
        Me.MainStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'VideoFileStatusLabel
        '
        Me.VideoFileStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.VideoFileStatusLabel.Name = "VideoFileStatusLabel"
        Me.VideoFileStatusLabel.Size = New System.Drawing.Size(20, 19)
        Me.VideoFileStatusLabel.Text = "..."
        '
        'XmlFileStatusLabel
        '
        Me.XmlFileStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.XmlFileStatusLabel.Name = "XmlFileStatusLabel"
        Me.XmlFileStatusLabel.Size = New System.Drawing.Size(20, 19)
        Me.XmlFileStatusLabel.Text = "..."
        '
        'SegmentsGroupBox
        '
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentEndLinkLabel)
        Me.SegmentsGroupBox.Controls.Add(Me.UnselectSegmentButton)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentStartLinkLabel)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentEndButton)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentStartButton)
        Me.SegmentsGroupBox.Controls.Add(Me.Label5)
        Me.SegmentsGroupBox.Controls.Add(Me.AoiGroupBox)
        Me.SegmentsGroupBox.Controls.Add(Me.Label4)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentNameTextBox)
        Me.SegmentsGroupBox.Controls.Add(Me.Label3)
        Me.SegmentsGroupBox.Controls.Add(Me.AddUpdateSegmentButton)
        Me.SegmentsGroupBox.Controls.Add(Me.DeleteSegmentButton)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentsListBox)
        Me.SegmentsGroupBox.Enabled = False
        Me.SegmentsGroupBox.Location = New System.Drawing.Point(519, 25)
        Me.SegmentsGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentsGroupBox.Name = "SegmentsGroupBox"
        Me.SegmentsGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentsGroupBox.Size = New System.Drawing.Size(262, 391)
        Me.SegmentsGroupBox.TabIndex = 5
        Me.SegmentsGroupBox.TabStop = False
        Me.SegmentsGroupBox.Text = "Stimulus Segments"
        '
        'SegmentEndLinkLabel
        '
        Me.SegmentEndLinkLabel.AutoSize = True
        Me.SegmentEndLinkLabel.Enabled = False
        Me.SegmentEndLinkLabel.Location = New System.Drawing.Point(49, 63)
        Me.SegmentEndLinkLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SegmentEndLinkLabel.Name = "SegmentEndLinkLabel"
        Me.SegmentEndLinkLabel.Size = New System.Drawing.Size(49, 13)
        Me.SegmentEndLinkLabel.TabIndex = 10
        Me.SegmentEndLinkLabel.TabStop = True
        Me.SegmentEndLinkLabel.Text = "00:00:00"
        '
        'UnselectSegmentButton
        '
        Me.UnselectSegmentButton.Enabled = False
        Me.UnselectSegmentButton.Location = New System.Drawing.Point(163, 46)
        Me.UnselectSegmentButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.UnselectSegmentButton.Name = "UnselectSegmentButton"
        Me.UnselectSegmentButton.Size = New System.Drawing.Size(89, 24)
        Me.UnselectSegmentButton.TabIndex = 9
        Me.UnselectSegmentButton.Text = "Unselect"
        Me.UnselectSegmentButton.UseVisualStyleBackColor = True
        '
        'SegmentStartLinkLabel
        '
        Me.SegmentStartLinkLabel.AutoSize = True
        Me.SegmentStartLinkLabel.Enabled = False
        Me.SegmentStartLinkLabel.Location = New System.Drawing.Point(49, 38)
        Me.SegmentStartLinkLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SegmentStartLinkLabel.Name = "SegmentStartLinkLabel"
        Me.SegmentStartLinkLabel.Size = New System.Drawing.Size(49, 13)
        Me.SegmentStartLinkLabel.TabIndex = 10
        Me.SegmentStartLinkLabel.TabStop = True
        Me.SegmentStartLinkLabel.Text = "00:00:00"
        '
        'SegmentEndButton
        '
        Me.SegmentEndButton.Location = New System.Drawing.Point(115, 63)
        Me.SegmentEndButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentEndButton.Name = "SegmentEndButton"
        Me.SegmentEndButton.Size = New System.Drawing.Size(21, 19)
        Me.SegmentEndButton.TabIndex = 8
        Me.SegmentEndButton.Text = "..."
        Me.SegmentEndButton.UseVisualStyleBackColor = True
        '
        'SegmentStartButton
        '
        Me.SegmentStartButton.Location = New System.Drawing.Point(115, 40)
        Me.SegmentStartButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentStartButton.Name = "SegmentStartButton"
        Me.SegmentStartButton.Size = New System.Drawing.Size(21, 19)
        Me.SegmentStartButton.TabIndex = 8
        Me.SegmentStartButton.Text = "..."
        Me.SegmentStartButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 65)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "End:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 38)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Start:"
        '
        'SegmentNameTextBox
        '
        Me.SegmentNameTextBox.Location = New System.Drawing.Point(51, 17)
        Me.SegmentNameTextBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentNameTextBox.Name = "SegmentNameTextBox"
        Me.SegmentNameTextBox.Size = New System.Drawing.Size(108, 20)
        Me.SegmentNameTextBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Name:"
        '
        'AddUpdateSegmentButton
        '
        Me.AddUpdateSegmentButton.Enabled = False
        Me.AddUpdateSegmentButton.Location = New System.Drawing.Point(163, 17)
        Me.AddUpdateSegmentButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AddUpdateSegmentButton.Name = "AddUpdateSegmentButton"
        Me.AddUpdateSegmentButton.Size = New System.Drawing.Size(89, 22)
        Me.AddUpdateSegmentButton.TabIndex = 2
        Me.AddUpdateSegmentButton.Text = "Add New"
        Me.AddUpdateSegmentButton.UseVisualStyleBackColor = True
        '
        'DeleteSegmentButton
        '
        Me.DeleteSegmentButton.Enabled = False
        Me.DeleteSegmentButton.Location = New System.Drawing.Point(163, 173)
        Me.DeleteSegmentButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DeleteSegmentButton.Name = "DeleteSegmentButton"
        Me.DeleteSegmentButton.Size = New System.Drawing.Size(89, 24)
        Me.DeleteSegmentButton.TabIndex = 1
        Me.DeleteSegmentButton.Text = "Delete Selected"
        Me.DeleteSegmentButton.UseVisualStyleBackColor = True
        '
        'SegmentsListBox
        '
        Me.SegmentsListBox.FormattingEnabled = True
        Me.SegmentsListBox.Location = New System.Drawing.Point(12, 87)
        Me.SegmentsListBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SegmentsListBox.Name = "SegmentsListBox"
        Me.SegmentsListBox.Size = New System.Drawing.Size(241, 82)
        Me.SegmentsListBox.TabIndex = 0
        '
        'VideoPositionTrackBar
        '
        Me.VideoPositionTrackBar.LargeChange = 10
        Me.VideoPositionTrackBar.Location = New System.Drawing.Point(4, 317)
        Me.VideoPositionTrackBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoPositionTrackBar.Maximum = 100
        Me.VideoPositionTrackBar.Name = "VideoPositionTrackBar"
        Me.VideoPositionTrackBar.Size = New System.Drawing.Size(253, 45)
        Me.VideoPositionTrackBar.TabIndex = 6
        Me.VideoPositionTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'VideoPositionUpDown
        '
        Me.VideoPositionUpDown.Location = New System.Drawing.Point(375, 317)
        Me.VideoPositionUpDown.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoPositionUpDown.Name = "VideoPositionUpDown"
        Me.VideoPositionUpDown.ReadOnly = True
        Me.VideoPositionUpDown.Size = New System.Drawing.Size(86, 20)
        Me.VideoPositionUpDown.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(286, 317)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Current position:"
        '
        'VideoGroupBox
        '
        Me.VideoGroupBox.Controls.Add(Me.VideoPanel)
        Me.VideoGroupBox.Controls.Add(Me.VideoActualSizeCheckBox)
        Me.VideoGroupBox.Controls.Add(Me.Label2)
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionLabel)
        Me.VideoGroupBox.Controls.Add(Me.SaveScreenshotButton)
        Me.VideoGroupBox.Controls.Add(Me.Label6)
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionUpDown)
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionTrackBar)
        Me.VideoGroupBox.Enabled = False
        Me.VideoGroupBox.Location = New System.Drawing.Point(9, 25)
        Me.VideoGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoGroupBox.Name = "VideoGroupBox"
        Me.VideoGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoGroupBox.Size = New System.Drawing.Size(496, 391)
        Me.VideoGroupBox.TabIndex = 9
        Me.VideoGroupBox.TabStop = False
        Me.VideoGroupBox.Text = "Video Preview"
        '
        'VideoPanel
        '
        Me.VideoPanel.AutoScroll = True
        Me.VideoPanel.BackColor = System.Drawing.Color.Black
        Me.VideoPanel.Controls.Add(Me.VideoPictureBox)
        Me.VideoPanel.Location = New System.Drawing.Point(4, 17)
        Me.VideoPanel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoPanel.Name = "VideoPanel"
        Me.VideoPanel.Size = New System.Drawing.Size(480, 292)
        Me.VideoPanel.TabIndex = 13
        '
        'VideoActualSizeCheckBox
        '
        Me.VideoActualSizeCheckBox.AutoSize = True
        Me.VideoActualSizeCheckBox.Location = New System.Drawing.Point(4, 367)
        Me.VideoActualSizeCheckBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.VideoActualSizeCheckBox.Name = "VideoActualSizeCheckBox"
        Me.VideoActualSizeCheckBox.Size = New System.Drawing.Size(132, 17)
        Me.VideoActualSizeCheckBox.TabIndex = 12
        Me.VideoActualSizeCheckBox.Text = "Full-size video preview"
        Me.VideoActualSizeCheckBox.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(465, 317)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "ms"
        '
        'VideoPositionLabel
        '
        Me.VideoPositionLabel.AutoSize = True
        Me.VideoPositionLabel.Location = New System.Drawing.Point(322, 331)
        Me.VideoPositionLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VideoPositionLabel.Name = "VideoPositionLabel"
        Me.VideoPositionLabel.Size = New System.Drawing.Size(49, 13)
        Me.VideoPositionLabel.TabIndex = 10
        Me.VideoPositionLabel.Text = "00:00:00"
        '
        'SaveScreenshotButton
        '
        Me.SaveScreenshotButton.Location = New System.Drawing.Point(375, 353)
        Me.SaveScreenshotButton.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.SaveScreenshotButton.Name = "SaveScreenshotButton"
        Me.SaveScreenshotButton.Size = New System.Drawing.Size(110, 26)
        Me.SaveScreenshotButton.TabIndex = 9
        Me.SaveScreenshotButton.Text = "Save Screenshot"
        Me.SaveScreenshotButton.UseVisualStyleBackColor = True
        '
        'MainOpenFileDialog
        '
        Me.MainOpenFileDialog.FileName = "MainOpenFileDialog"
        '
        'RedrawTimer
        '
        Me.RedrawTimer.Interval = 33
        '
        'MainColorDialog
        '
        Me.MainColorDialog.FullOpen = True
        Me.MainColorDialog.SolidColorOnly = True
        '
        'DisplaySettingsGroupBox
        '
        Me.DisplaySettingsGroupBox.Controls.Add(Me.AoiOpacityTrackBar)
        Me.DisplaySettingsGroupBox.Controls.Add(Me.Label16)
        Me.DisplaySettingsGroupBox.Controls.Add(Me.GroupBox2)
        Me.DisplaySettingsGroupBox.Enabled = False
        Me.DisplaySettingsGroupBox.Location = New System.Drawing.Point(794, 254)
        Me.DisplaySettingsGroupBox.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DisplaySettingsGroupBox.Name = "DisplaySettingsGroupBox"
        Me.DisplaySettingsGroupBox.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DisplaySettingsGroupBox.Size = New System.Drawing.Size(164, 162)
        Me.DisplaySettingsGroupBox.TabIndex = 15
        Me.DisplaySettingsGroupBox.TabStop = False
        Me.DisplaySettingsGroupBox.Text = "AOI Display Settings"
        '
        'AoiOpacityTrackBar
        '
        Me.AoiOpacityTrackBar.Location = New System.Drawing.Point(71, 102)
        Me.AoiOpacityTrackBar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.AoiOpacityTrackBar.Maximum = 255
        Me.AoiOpacityTrackBar.Name = "AoiOpacityTrackBar"
        Me.AoiOpacityTrackBar.Size = New System.Drawing.Size(85, 45)
        Me.AoiOpacityTrackBar.TabIndex = 11
        Me.AoiOpacityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.AoiOpacityTrackBar.Value = 170
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(4, 102)
        Me.Label16.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 13)
        Me.Label16.TabIndex = 10
        Me.Label16.Text = "AOI Opacity"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.AoiColorLabel)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.NewAoiColorLabel)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.NonExclusiveAoiColorLabel)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.SelectedAoiColorLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 17)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(152, 82)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Colors"
        '
        'AxWindowsMediaPlayer1
        '
        Me.AxWindowsMediaPlayer1.Enabled = True
        Me.AxWindowsMediaPlayer1.Location = New System.Drawing.Point(994, 104)
        Me.AxWindowsMediaPlayer1.Name = "AxWindowsMediaPlayer1"
        Me.AxWindowsMediaPlayer1.OcxState = CType(resources.GetObject("AxWindowsMediaPlayer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxWindowsMediaPlayer1.Size = New System.Drawing.Size(75, 23)
        Me.AxWindowsMediaPlayer1.TabIndex = 16
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1187, 447)
        Me.Controls.Add(Me.AxWindowsMediaPlayer1)
        Me.Controls.Add(Me.DisplaySettingsGroupBox)
        Me.Controls.Add(Me.VideoGroupBox)
        Me.Controls.Add(Me.SegmentsGroupBox)
        Me.Controls.Add(Me.MainStatusStrip)
        Me.Controls.Add(Me.SettingsGroupBox)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Eye Data Processing Tool"
        CType(Me.VideoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AoiGroupBox.ResumeLayout(False)
        Me.AoiGroupBox.PerformLayout()
        CType(Me.AoiHeightUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AoiWidthUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AoiYUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AoiXUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenuStrip.ResumeLayout(False)
        Me.MainMenuStrip.PerformLayout()
        Me.SettingsGroupBox.ResumeLayout(False)
        Me.SettingsGroupBox.PerformLayout()
        Me.MeasureFixationsGroupBox.ResumeLayout(False)
        Me.MeasureFixationsGroupBox.PerformLayout()
        Me.MainStatusStrip.ResumeLayout(False)
        Me.MainStatusStrip.PerformLayout()
        Me.SegmentsGroupBox.ResumeLayout(False)
        Me.SegmentsGroupBox.PerformLayout()
        CType(Me.VideoPositionTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VideoPositionUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VideoGroupBox.ResumeLayout(False)
        Me.VideoGroupBox.PerformLayout()
        Me.VideoPanel.ResumeLayout(False)
        Me.VideoPanel.PerformLayout()
        Me.DisplaySettingsGroupBox.ResumeLayout(False)
        Me.DisplaySettingsGroupBox.PerformLayout()
        CType(Me.AoiOpacityTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.AxWindowsMediaPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VideoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents AoiGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DeleteAoiButton As System.Windows.Forms.Button
    Friend WithEvents AddRenameAoiButton As System.Windows.Forms.Button
    Friend WithEvents AoiListBox As System.Windows.Forms.ListBox
    Friend WithEvents MainMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HeatmapsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AoiNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SettingsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ProcessButton As System.Windows.Forms.Button
    Friend WithEvents FixationDurationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MainStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MainStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SegmentsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SegmentNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AddUpdateSegmentButton As System.Windows.Forms.Button
    Friend WithEvents DeleteSegmentButton As System.Windows.Forms.Button
    Friend WithEvents SegmentsListBox As System.Windows.Forms.ListBox
    Friend WithEvents SegmentStartButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents VideoPositionTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents VideoPositionUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SegmentEndButton As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents VideoGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents OpenStudyDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateNewHeatmapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveScreenshotButton As System.Windows.Forms.Button
    Friend WithEvents MainOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VideoFileStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents XmlFileStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents RedrawTimer As System.Windows.Forms.Timer
    Friend WithEvents MainSaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents VideoPositionLabel As System.Windows.Forms.Label
    Friend WithEvents UnselectSegmentButton As System.Windows.Forms.Button
    Friend WithEvents SegmentStartLinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents SegmentEndLinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents VideoActualSizeCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents VideoPanel As System.Windows.Forms.Panel
    Friend WithEvents MainColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents AoiColorLabel As System.Windows.Forms.Label
    Friend WithEvents NewAoiColorLabel As System.Windows.Forms.Label
    Friend WithEvents SelectedAoiColorLabel As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents AoiHeightUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AoiWidthUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AoiYUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AoiXUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AoiNonexclusiveCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents NonExclusiveAoiColorLabel As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ImportStimulusSegmentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportStimulusSegmentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplaySettingsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents AoiOpacityTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SaveFixationDurationsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFixationCountsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveCalibrationErrorCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveSegmentDurationsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents MeasureFixationsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents MeasureFixationsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SaveFixationLocationsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents AxWindowsMediaPlayer1 As AxWMPLib.AxWindowsMediaPlayer

End Class
