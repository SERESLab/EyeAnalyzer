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
        Me.VideoPictureBox = New System.Windows.Forms.PictureBox()
        Me.AoiGroupBox = New System.Windows.Forms.GroupBox()
        Me.CopyAoiButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.AoiNameTextBox = New System.Windows.Forms.TextBox()
        Me.DeleteAoiButton = New System.Windows.Forms.Button()
        Me.AddAoiButton = New System.Windows.Forms.Button()
        Me.AoiListBox = New System.Windows.Forms.ListBox()
        Me.MainMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenStudyDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeatmapsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateNewHeatmapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsGroupBox = New System.Windows.Forms.GroupBox()
        Me.ProcessFixationsButton = New System.Windows.Forms.Button()
        Me.FixationDurationTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.MainStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.VideoFileStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.XmlFileStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SegmentsGroupBox = New System.Windows.Forms.GroupBox()
        Me.SegmentEndButton = New System.Windows.Forms.Button()
        Me.SegmentStartButton = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SegmentEndTextBox = New System.Windows.Forms.TextBox()
        Me.SegmentStartTextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SegmentNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AddSegmentButton = New System.Windows.Forms.Button()
        Me.DeleteSegmentButton = New System.Windows.Forms.Button()
        Me.SegmentsListBox = New System.Windows.Forms.ListBox()
        Me.VideoPositionTrackBar = New System.Windows.Forms.TrackBar()
        Me.VideoPositionUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.VideoGroupBox = New System.Windows.Forms.GroupBox()
        Me.VideoPositionLabel = New System.Windows.Forms.Label()
        Me.SaveScreenshotButton = New System.Windows.Forms.Button()
        Me.MainOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.RedrawTimer = New System.Windows.Forms.Timer(Me.components)
        Me.MainSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        CType(Me.VideoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AoiGroupBox.SuspendLayout()
        Me.MainMenuStrip.SuspendLayout()
        Me.SettingsGroupBox.SuspendLayout()
        Me.MainStatusStrip.SuspendLayout()
        Me.SegmentsGroupBox.SuspendLayout()
        CType(Me.VideoPositionTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VideoPositionUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VideoGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'VideoPictureBox
        '
        Me.VideoPictureBox.BackColor = System.Drawing.Color.Black
        Me.VideoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.VideoPictureBox.Location = New System.Drawing.Point(6, 21)
        Me.VideoPictureBox.Name = "VideoPictureBox"
        Me.VideoPictureBox.Size = New System.Drawing.Size(640, 360)
        Me.VideoPictureBox.TabIndex = 0
        Me.VideoPictureBox.TabStop = False
        '
        'AoiGroupBox
        '
        Me.AoiGroupBox.Controls.Add(Me.CopyAoiButton)
        Me.AoiGroupBox.Controls.Add(Me.Label7)
        Me.AoiGroupBox.Controls.Add(Me.AoiNameTextBox)
        Me.AoiGroupBox.Controls.Add(Me.DeleteAoiButton)
        Me.AoiGroupBox.Controls.Add(Me.AddAoiButton)
        Me.AoiGroupBox.Controls.Add(Me.AoiListBox)
        Me.AoiGroupBox.Enabled = False
        Me.AoiGroupBox.Location = New System.Drawing.Point(16, 217)
        Me.AoiGroupBox.Name = "AoiGroupBox"
        Me.AoiGroupBox.Size = New System.Drawing.Size(302, 226)
        Me.AoiGroupBox.TabIndex = 1
        Me.AoiGroupBox.TabStop = False
        Me.AoiGroupBox.Text = "AOIs"
        '
        'CopyAoiButton
        '
        Me.CopyAoiButton.Enabled = False
        Me.CopyAoiButton.Location = New System.Drawing.Point(171, 58)
        Me.CopyAoiButton.Name = "CopyAoiButton"
        Me.CopyAoiButton.Size = New System.Drawing.Size(119, 28)
        Me.CopyAoiButton.TabIndex = 5
        Me.CopyAoiButton.Text = "Copy Selected"
        Me.CopyAoiButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 17)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Name:"
        '
        'AoiNameTextBox
        '
        Me.AoiNameTextBox.Location = New System.Drawing.Point(61, 30)
        Me.AoiNameTextBox.Name = "AoiNameTextBox"
        Me.AoiNameTextBox.Size = New System.Drawing.Size(104, 22)
        Me.AoiNameTextBox.TabIndex = 3
        '
        'DeleteAoiButton
        '
        Me.DeleteAoiButton.Enabled = False
        Me.DeleteAoiButton.Location = New System.Drawing.Point(171, 106)
        Me.DeleteAoiButton.Name = "DeleteAoiButton"
        Me.DeleteAoiButton.Size = New System.Drawing.Size(119, 23)
        Me.DeleteAoiButton.TabIndex = 2
        Me.DeleteAoiButton.Text = "Delete Selected"
        Me.DeleteAoiButton.UseVisualStyleBackColor = True
        '
        'AddAoiButton
        '
        Me.AddAoiButton.Enabled = False
        Me.AddAoiButton.Location = New System.Drawing.Point(171, 29)
        Me.AddAoiButton.Name = "AddAoiButton"
        Me.AddAoiButton.Size = New System.Drawing.Size(119, 23)
        Me.AddAoiButton.TabIndex = 1
        Me.AddAoiButton.Text = "Add New"
        Me.AddAoiButton.UseVisualStyleBackColor = True
        '
        'AoiListBox
        '
        Me.AoiListBox.FormattingEnabled = True
        Me.AoiListBox.ItemHeight = 16
        Me.AoiListBox.Location = New System.Drawing.Point(9, 58)
        Me.AoiListBox.Name = "AoiListBox"
        Me.AoiListBox.Size = New System.Drawing.Size(156, 164)
        Me.AoiListBox.TabIndex = 0
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HeatmapsToolStripMenuItem})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Size = New System.Drawing.Size(1279, 28)
        Me.MainMenuStrip.TabIndex = 1
        Me.MainMenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenStudyDataToolStripMenuItem, Me.ToolStripSeparator1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'OpenStudyDataToolStripMenuItem
        '
        Me.OpenStudyDataToolStripMenuItem.Name = "OpenStudyDataToolStripMenuItem"
        Me.OpenStudyDataToolStripMenuItem.Size = New System.Drawing.Size(188, 24)
        Me.OpenStudyDataToolStripMenuItem.Text = "&Load Study Data"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(185, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(188, 24)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'HeatmapsToolStripMenuItem
        '
        Me.HeatmapsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GenerateNewHeatmapToolStripMenuItem})
        Me.HeatmapsToolStripMenuItem.Name = "HeatmapsToolStripMenuItem"
        Me.HeatmapsToolStripMenuItem.Size = New System.Drawing.Size(89, 24)
        Me.HeatmapsToolStripMenuItem.Text = "Heat&maps"
        '
        'GenerateNewHeatmapToolStripMenuItem
        '
        Me.GenerateNewHeatmapToolStripMenuItem.Name = "GenerateNewHeatmapToolStripMenuItem"
        Me.GenerateNewHeatmapToolStripMenuItem.Size = New System.Drawing.Size(238, 24)
        Me.GenerateNewHeatmapToolStripMenuItem.Text = "&Generate New Heatmap"
        '
        'SettingsGroupBox
        '
        Me.SettingsGroupBox.Controls.Add(Me.ProcessFixationsButton)
        Me.SettingsGroupBox.Controls.Add(Me.FixationDurationTextBox)
        Me.SettingsGroupBox.Controls.Add(Me.Label1)
        Me.SettingsGroupBox.Location = New System.Drawing.Point(1037, 31)
        Me.SettingsGroupBox.Name = "SettingsGroupBox"
        Me.SettingsGroupBox.Size = New System.Drawing.Size(234, 97)
        Me.SettingsGroupBox.TabIndex = 3
        Me.SettingsGroupBox.TabStop = False
        Me.SettingsGroupBox.Text = "Processing Settings"
        '
        'ProcessFixationsButton
        '
        Me.ProcessFixationsButton.Enabled = False
        Me.ProcessFixationsButton.Location = New System.Drawing.Point(36, 57)
        Me.ProcessFixationsButton.Name = "ProcessFixationsButton"
        Me.ProcessFixationsButton.Size = New System.Drawing.Size(188, 23)
        Me.ProcessFixationsButton.TabIndex = 3
        Me.ProcessFixationsButton.Text = "Process Fixations"
        Me.ProcessFixationsButton.UseVisualStyleBackColor = True
        '
        'FixationDurationTextBox
        '
        Me.FixationDurationTextBox.Location = New System.Drawing.Point(162, 29)
        Me.FixationDurationTextBox.Name = "FixationDurationTextBox"
        Me.FixationDurationTextBox.Size = New System.Drawing.Size(62, 22)
        Me.FixationDurationTextBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fixation Duration (ms):"
        '
        'MainStatusStrip
        '
        Me.MainStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MainStatusLabel, Me.VideoFileStatusLabel, Me.XmlFileStatusLabel})
        Me.MainStatusStrip.Location = New System.Drawing.Point(0, 492)
        Me.MainStatusStrip.Name = "MainStatusStrip"
        Me.MainStatusStrip.Size = New System.Drawing.Size(1279, 29)
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
        Me.MainStatusLabel.Size = New System.Drawing.Size(1220, 24)
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
        Me.VideoFileStatusLabel.Size = New System.Drawing.Size(22, 24)
        Me.VideoFileStatusLabel.Text = "..."
        '
        'XmlFileStatusLabel
        '
        Me.XmlFileStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.XmlFileStatusLabel.Name = "XmlFileStatusLabel"
        Me.XmlFileStatusLabel.Size = New System.Drawing.Size(22, 24)
        Me.XmlFileStatusLabel.Text = "..."
        '
        'SegmentsGroupBox
        '
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentEndButton)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentStartButton)
        Me.SegmentsGroupBox.Controls.Add(Me.Label5)
        Me.SegmentsGroupBox.Controls.Add(Me.AoiGroupBox)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentEndTextBox)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentStartTextBox)
        Me.SegmentsGroupBox.Controls.Add(Me.Label4)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentNameTextBox)
        Me.SegmentsGroupBox.Controls.Add(Me.Label3)
        Me.SegmentsGroupBox.Controls.Add(Me.AddSegmentButton)
        Me.SegmentsGroupBox.Controls.Add(Me.DeleteSegmentButton)
        Me.SegmentsGroupBox.Controls.Add(Me.SegmentsListBox)
        Me.SegmentsGroupBox.Enabled = False
        Me.SegmentsGroupBox.Location = New System.Drawing.Point(692, 31)
        Me.SegmentsGroupBox.Name = "SegmentsGroupBox"
        Me.SegmentsGroupBox.Size = New System.Drawing.Size(329, 453)
        Me.SegmentsGroupBox.TabIndex = 5
        Me.SegmentsGroupBox.TabStop = False
        Me.SegmentsGroupBox.Text = "Stimulus Segments"
        '
        'SegmentEndButton
        '
        Me.SegmentEndButton.Location = New System.Drawing.Point(290, 57)
        Me.SegmentEndButton.Name = "SegmentEndButton"
        Me.SegmentEndButton.Size = New System.Drawing.Size(28, 23)
        Me.SegmentEndButton.TabIndex = 8
        Me.SegmentEndButton.Text = "..."
        Me.SegmentEndButton.UseVisualStyleBackColor = True
        '
        'SegmentStartButton
        '
        Me.SegmentStartButton.Location = New System.Drawing.Point(142, 57)
        Me.SegmentStartButton.Name = "SegmentStartButton"
        Me.SegmentStartButton.Size = New System.Drawing.Size(28, 23)
        Me.SegmentStartButton.TabIndex = 8
        Me.SegmentStartButton.Text = "..."
        Me.SegmentStartButton.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(176, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 17)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "End:"
        '
        'SegmentEndTextBox
        '
        Me.SegmentEndTextBox.Enabled = False
        Me.SegmentEndTextBox.Location = New System.Drawing.Point(216, 56)
        Me.SegmentEndTextBox.Name = "SegmentEndTextBox"
        Me.SegmentEndTextBox.Size = New System.Drawing.Size(68, 22)
        Me.SegmentEndTextBox.TabIndex = 6
        '
        'SegmentStartTextBox
        '
        Me.SegmentStartTextBox.Enabled = False
        Me.SegmentStartTextBox.Location = New System.Drawing.Point(68, 56)
        Me.SegmentStartTextBox.Name = "SegmentStartTextBox"
        Me.SegmentStartTextBox.Size = New System.Drawing.Size(68, 22)
        Me.SegmentStartTextBox.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 17)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Start:"
        '
        'SegmentNameTextBox
        '
        Me.SegmentNameTextBox.Location = New System.Drawing.Point(68, 27)
        Me.SegmentNameTextBox.Name = "SegmentNameTextBox"
        Me.SegmentNameTextBox.Size = New System.Drawing.Size(125, 22)
        Me.SegmentNameTextBox.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Name:"
        '
        'AddSegmentButton
        '
        Me.AddSegmentButton.Enabled = False
        Me.AddSegmentButton.Location = New System.Drawing.Point(199, 26)
        Me.AddSegmentButton.Name = "AddSegmentButton"
        Me.AddSegmentButton.Size = New System.Drawing.Size(119, 23)
        Me.AddSegmentButton.TabIndex = 2
        Me.AddSegmentButton.Text = "Add New"
        Me.AddSegmentButton.UseVisualStyleBackColor = True
        '
        'DeleteSegmentButton
        '
        Me.DeleteSegmentButton.Enabled = False
        Me.DeleteSegmentButton.Location = New System.Drawing.Point(199, 86)
        Me.DeleteSegmentButton.Name = "DeleteSegmentButton"
        Me.DeleteSegmentButton.Size = New System.Drawing.Size(119, 23)
        Me.DeleteSegmentButton.TabIndex = 1
        Me.DeleteSegmentButton.Text = "Delete Selected"
        Me.DeleteSegmentButton.UseVisualStyleBackColor = True
        '
        'SegmentsListBox
        '
        Me.SegmentsListBox.FormattingEnabled = True
        Me.SegmentsListBox.ItemHeight = 16
        Me.SegmentsListBox.Location = New System.Drawing.Point(16, 88)
        Me.SegmentsListBox.MultiColumn = True
        Me.SegmentsListBox.Name = "SegmentsListBox"
        Me.SegmentsListBox.Size = New System.Drawing.Size(177, 100)
        Me.SegmentsListBox.TabIndex = 0
        '
        'VideoPositionTrackBar
        '
        Me.VideoPositionTrackBar.LargeChange = 10
        Me.VideoPositionTrackBar.Location = New System.Drawing.Point(6, 387)
        Me.VideoPositionTrackBar.Maximum = 100
        Me.VideoPositionTrackBar.Name = "VideoPositionTrackBar"
        Me.VideoPositionTrackBar.Size = New System.Drawing.Size(337, 56)
        Me.VideoPositionTrackBar.TabIndex = 6
        '
        'VideoPositionUpDown
        '
        Me.VideoPositionUpDown.Location = New System.Drawing.Point(500, 387)
        Me.VideoPositionUpDown.Name = "VideoPositionUpDown"
        Me.VideoPositionUpDown.Size = New System.Drawing.Size(146, 22)
        Me.VideoPositionUpDown.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(349, 389)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(145, 17)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Current Position (ms):"
        '
        'VideoGroupBox
        '
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionLabel)
        Me.VideoGroupBox.Controls.Add(Me.SaveScreenshotButton)
        Me.VideoGroupBox.Controls.Add(Me.VideoPictureBox)
        Me.VideoGroupBox.Controls.Add(Me.Label6)
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionUpDown)
        Me.VideoGroupBox.Controls.Add(Me.VideoPositionTrackBar)
        Me.VideoGroupBox.Enabled = False
        Me.VideoGroupBox.Location = New System.Drawing.Point(12, 31)
        Me.VideoGroupBox.Name = "VideoGroupBox"
        Me.VideoGroupBox.Size = New System.Drawing.Size(661, 453)
        Me.VideoGroupBox.TabIndex = 9
        Me.VideoGroupBox.TabStop = False
        Me.VideoGroupBox.Text = "Video"
        '
        'VideoPositionLabel
        '
        Me.VideoPositionLabel.AutoSize = True
        Me.VideoPositionLabel.Location = New System.Drawing.Point(349, 406)
        Me.VideoPositionLabel.Name = "VideoPositionLabel"
        Me.VideoPositionLabel.Size = New System.Drawing.Size(64, 17)
        Me.VideoPositionLabel.TabIndex = 10
        Me.VideoPositionLabel.Text = "00:00:00"
        '
        'SaveScreenshotButton
        '
        Me.SaveScreenshotButton.Location = New System.Drawing.Point(500, 420)
        Me.SaveScreenshotButton.Name = "SaveScreenshotButton"
        Me.SaveScreenshotButton.Size = New System.Drawing.Size(146, 23)
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
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 521)
        Me.Controls.Add(Me.VideoGroupBox)
        Me.Controls.Add(Me.SegmentsGroupBox)
        Me.Controls.Add(Me.MainStatusStrip)
        Me.Controls.Add(Me.SettingsGroupBox)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fixation Measurement Tool"
        CType(Me.VideoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AoiGroupBox.ResumeLayout(False)
        Me.AoiGroupBox.PerformLayout()
        Me.MainMenuStrip.ResumeLayout(False)
        Me.MainMenuStrip.PerformLayout()
        Me.SettingsGroupBox.ResumeLayout(False)
        Me.SettingsGroupBox.PerformLayout()
        Me.MainStatusStrip.ResumeLayout(False)
        Me.MainStatusStrip.PerformLayout()
        Me.SegmentsGroupBox.ResumeLayout(False)
        Me.SegmentsGroupBox.PerformLayout()
        CType(Me.VideoPositionTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VideoPositionUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VideoGroupBox.ResumeLayout(False)
        Me.VideoGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VideoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents AoiGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DeleteAoiButton As System.Windows.Forms.Button
    Friend WithEvents AddAoiButton As System.Windows.Forms.Button
    Friend WithEvents AoiListBox As System.Windows.Forms.ListBox
    Friend WithEvents MainMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HeatmapsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AoiNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SettingsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ProcessFixationsButton As System.Windows.Forms.Button
    Friend WithEvents FixationDurationTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MainStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents MainStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SegmentsGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SegmentNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AddSegmentButton As System.Windows.Forms.Button
    Friend WithEvents DeleteSegmentButton As System.Windows.Forms.Button
    Friend WithEvents SegmentsListBox As System.Windows.Forms.ListBox
    Friend WithEvents SegmentStartButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SegmentStartTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VideoPositionTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents VideoPositionUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SegmentEndButton As System.Windows.Forms.Button
    Friend WithEvents SegmentEndTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents VideoGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents OpenStudyDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GenerateNewHeatmapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveScreenshotButton As System.Windows.Forms.Button
    Friend WithEvents CopyAoiButton As System.Windows.Forms.Button
    Friend WithEvents MainOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents VideoFileStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents XmlFileStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents RedrawTimer As System.Windows.Forms.Timer
    Friend WithEvents MainSaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents VideoPositionLabel As System.Windows.Forms.Label

End Class
