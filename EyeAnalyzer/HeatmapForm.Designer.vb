<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HeatmapForm
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
        Me.MainMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearFixationLocationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFixationLocationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportHeatmapsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeatmapPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.HeatmapsListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusTextBox = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SelectNoneButton = New System.Windows.Forms.Button()
        Me.SelectAllButton = New System.Windows.Forms.Button()
        Me.HeatmapGroupBox = New System.Windows.Forms.GroupBox()
        Me.AlphaTrackBar = New System.Windows.Forms.TrackBar()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadiusNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HighFixationsColorLabel = New System.Windows.Forms.Label()
        Me.MidFixationsColorLabel = New System.Windows.Forms.Label()
        Me.LowFixationsColorLabel = New System.Windows.Forms.Label()
        Me.ClearStimulusImageButton = New System.Windows.Forms.Button()
        Me.LoadStimulusImageButton = New System.Windows.Forms.Button()
        Me.StimulusImageTextBox = New System.Windows.Forms.TextBox()
        Me.TotalFixationsTextBox = New System.Windows.Forms.TextBox()
        Me.NumberOfSubjectsTextBox = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.MainSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MainOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.MainColorDialog = New System.Windows.Forms.ColorDialog()
        Me.MainMenuStrip.SuspendLayout()
        CType(Me.HeatmapPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.HeatmapGroupBox.SuspendLayout()
        CType(Me.AlphaTrackBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadiusNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Size = New System.Drawing.Size(1094, 28)
        Me.MainMenuStrip.TabIndex = 0
        Me.MainMenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearFixationLocationsToolStripMenuItem, Me.ImportFixationLocationsToolStripMenuItem, Me.ExportHeatmapsToolStripMenuItem, Me.ToolStripSeparator1, Me.CloseToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ClearFixationLocationsToolStripMenuItem
        '
        Me.ClearFixationLocationsToolStripMenuItem.Enabled = False
        Me.ClearFixationLocationsToolStripMenuItem.Name = "ClearFixationLocationsToolStripMenuItem"
        Me.ClearFixationLocationsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ClearFixationLocationsToolStripMenuItem.Text = "C&lear Fixation Locations"
        '
        'ImportFixationLocationsToolStripMenuItem
        '
        Me.ImportFixationLocationsToolStripMenuItem.Name = "ImportFixationLocationsToolStripMenuItem"
        Me.ImportFixationLocationsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ImportFixationLocationsToolStripMenuItem.Text = "&Import Fixation Locations"
        '
        'ExportHeatmapsToolStripMenuItem
        '
        Me.ExportHeatmapsToolStripMenuItem.Enabled = False
        Me.ExportHeatmapsToolStripMenuItem.Name = "ExportHeatmapsToolStripMenuItem"
        Me.ExportHeatmapsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ExportHeatmapsToolStripMenuItem.Text = "&Export Heatmap Images"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(243, 6)
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.CloseToolStripMenuItem.Text = "&Close"
        '
        'HeatmapPictureBox
        '
        Me.HeatmapPictureBox.BackColor = System.Drawing.Color.Black
        Me.HeatmapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HeatmapPictureBox.Location = New System.Drawing.Point(17, 32)
        Me.HeatmapPictureBox.Name = "HeatmapPictureBox"
        Me.HeatmapPictureBox.Size = New System.Drawing.Size(480, 274)
        Me.HeatmapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.HeatmapPictureBox.TabIndex = 2
        Me.HeatmapPictureBox.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(505, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Number of subjects:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(505, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Total fixation count:"
        '
        'HeatmapsListBox
        '
        Me.HeatmapsListBox.FormattingEnabled = True
        Me.HeatmapsListBox.Location = New System.Drawing.Point(6, 32)
        Me.HeatmapsListBox.Name = "HeatmapsListBox"
        Me.HeatmapsListBox.Size = New System.Drawing.Size(320, 225)
        Me.HeatmapsListBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(505, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 17)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Stimulus image:"
        '
        'StatusTextBox
        '
        Me.StatusTextBox.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusTextBox.Location = New System.Drawing.Point(6, 21)
        Me.StatusTextBox.Name = "StatusTextBox"
        Me.StatusTextBox.ReadOnly = True
        Me.StatusTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.StatusTextBox.Size = New System.Drawing.Size(1060, 111)
        Me.StatusTextBox.TabIndex = 7
        Me.StatusTextBox.Text = ""
        Me.StatusTextBox.WordWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SelectNoneButton)
        Me.GroupBox1.Controls.Add(Me.SelectAllButton)
        Me.GroupBox1.Controls.Add(Me.HeatmapsListBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 322)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stimulus Segments"
        '
        'SelectNoneButton
        '
        Me.SelectNoneButton.Enabled = False
        Me.SelectNoneButton.Location = New System.Drawing.Point(127, 274)
        Me.SelectNoneButton.Name = "SelectNoneButton"
        Me.SelectNoneButton.Size = New System.Drawing.Size(115, 32)
        Me.SelectNoneButton.TabIndex = 6
        Me.SelectNoneButton.Text = "Select None"
        Me.SelectNoneButton.UseVisualStyleBackColor = True
        '
        'SelectAllButton
        '
        Me.SelectAllButton.Enabled = False
        Me.SelectAllButton.Location = New System.Drawing.Point(6, 275)
        Me.SelectAllButton.Name = "SelectAllButton"
        Me.SelectAllButton.Size = New System.Drawing.Size(115, 32)
        Me.SelectAllButton.TabIndex = 6
        Me.SelectAllButton.Text = "Select All"
        Me.SelectAllButton.UseVisualStyleBackColor = True
        '
        'HeatmapGroupBox
        '
        Me.HeatmapGroupBox.Controls.Add(Me.AlphaTrackBar)
        Me.HeatmapGroupBox.Controls.Add(Me.Label8)
        Me.HeatmapGroupBox.Controls.Add(Me.Label4)
        Me.HeatmapGroupBox.Controls.Add(Me.RadiusNumericUpDown)
        Me.HeatmapGroupBox.Controls.Add(Me.Label7)
        Me.HeatmapGroupBox.Controls.Add(Me.Label6)
        Me.HeatmapGroupBox.Controls.Add(Me.Label5)
        Me.HeatmapGroupBox.Controls.Add(Me.HighFixationsColorLabel)
        Me.HeatmapGroupBox.Controls.Add(Me.MidFixationsColorLabel)
        Me.HeatmapGroupBox.Controls.Add(Me.LowFixationsColorLabel)
        Me.HeatmapGroupBox.Controls.Add(Me.ClearStimulusImageButton)
        Me.HeatmapGroupBox.Controls.Add(Me.LoadStimulusImageButton)
        Me.HeatmapGroupBox.Controls.Add(Me.StimulusImageTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.HeatmapPictureBox)
        Me.HeatmapGroupBox.Controls.Add(Me.TotalFixationsTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.NumberOfSubjectsTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.Label1)
        Me.HeatmapGroupBox.Controls.Add(Me.Label2)
        Me.HeatmapGroupBox.Controls.Add(Me.Label3)
        Me.HeatmapGroupBox.Enabled = False
        Me.HeatmapGroupBox.Location = New System.Drawing.Point(360, 31)
        Me.HeatmapGroupBox.Name = "HeatmapGroupBox"
        Me.HeatmapGroupBox.Size = New System.Drawing.Size(724, 322)
        Me.HeatmapGroupBox.TabIndex = 9
        Me.HeatmapGroupBox.TabStop = False
        Me.HeatmapGroupBox.Text = "Stimulus Heatmap"
        '
        'AlphaTrackBar
        '
        Me.AlphaTrackBar.LargeChange = 50
        Me.AlphaTrackBar.Location = New System.Drawing.Point(620, 251)
        Me.AlphaTrackBar.Maximum = 255
        Me.AlphaTrackBar.Name = "AlphaTrackBar"
        Me.AlphaTrackBar.Size = New System.Drawing.Size(85, 56)
        Me.AlphaTrackBar.SmallChange = 20
        Me.AlphaTrackBar.TabIndex = 16
        Me.AlphaTrackBar.TickFrequency = 20
        Me.AlphaTrackBar.TickStyle = System.Windows.Forms.TickStyle.None
        Me.AlphaTrackBar.Value = 255
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(505, 251)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(109, 17)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Fixation opacity:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(505, 219)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Fixation radius:"
        '
        'RadiusNumericUpDown
        '
        Me.RadiusNumericUpDown.Location = New System.Drawing.Point(648, 219)
        Me.RadiusNumericUpDown.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.RadiusNumericUpDown.Name = "RadiusNumericUpDown"
        Me.RadiusNumericUpDown.Size = New System.Drawing.Size(57, 22)
        Me.RadiusNumericUpDown.TabIndex = 13
        Me.RadiusNumericUpDown.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(505, 192)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 17)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "High fixations color:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(505, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(151, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Medium fixations color:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(505, 153)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(127, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Low fixations color:"
        '
        'HighFixationsColorLabel
        '
        Me.HighFixationsColorLabel.BackColor = System.Drawing.Color.Black
        Me.HighFixationsColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HighFixationsColorLabel.Location = New System.Drawing.Point(682, 193)
        Me.HighFixationsColorLabel.Name = "HighFixationsColorLabel"
        Me.HighFixationsColorLabel.Size = New System.Drawing.Size(23, 19)
        Me.HighFixationsColorLabel.TabIndex = 9
        '
        'MidFixationsColorLabel
        '
        Me.MidFixationsColorLabel.BackColor = System.Drawing.Color.Black
        Me.MidFixationsColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MidFixationsColorLabel.Location = New System.Drawing.Point(682, 173)
        Me.MidFixationsColorLabel.Name = "MidFixationsColorLabel"
        Me.MidFixationsColorLabel.Size = New System.Drawing.Size(23, 19)
        Me.MidFixationsColorLabel.TabIndex = 9
        '
        'LowFixationsColorLabel
        '
        Me.LowFixationsColorLabel.BackColor = System.Drawing.Color.Black
        Me.LowFixationsColorLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LowFixationsColorLabel.Location = New System.Drawing.Point(682, 153)
        Me.LowFixationsColorLabel.Name = "LowFixationsColorLabel"
        Me.LowFixationsColorLabel.Size = New System.Drawing.Size(23, 19)
        Me.LowFixationsColorLabel.TabIndex = 9
        '
        'ClearStimulusImageButton
        '
        Me.ClearStimulusImageButton.Location = New System.Drawing.Point(684, 109)
        Me.ClearStimulusImageButton.Name = "ClearStimulusImageButton"
        Me.ClearStimulusImageButton.Size = New System.Drawing.Size(21, 23)
        Me.ClearStimulusImageButton.TabIndex = 8
        Me.ClearStimulusImageButton.Text = "X"
        Me.ClearStimulusImageButton.UseVisualStyleBackColor = True
        '
        'LoadStimulusImageButton
        '
        Me.LoadStimulusImageButton.Location = New System.Drawing.Point(648, 109)
        Me.LoadStimulusImageButton.Name = "LoadStimulusImageButton"
        Me.LoadStimulusImageButton.Size = New System.Drawing.Size(30, 23)
        Me.LoadStimulusImageButton.TabIndex = 8
        Me.LoadStimulusImageButton.Text = "..."
        Me.LoadStimulusImageButton.UseVisualStyleBackColor = True
        '
        'StimulusImageTextBox
        '
        Me.StimulusImageTextBox.Enabled = False
        Me.StimulusImageTextBox.Location = New System.Drawing.Point(508, 110)
        Me.StimulusImageTextBox.Name = "StimulusImageTextBox"
        Me.StimulusImageTextBox.Size = New System.Drawing.Size(134, 22)
        Me.StimulusImageTextBox.TabIndex = 7
        '
        'TotalFixationsTextBox
        '
        Me.TotalFixationsTextBox.Enabled = False
        Me.TotalFixationsTextBox.Location = New System.Drawing.Point(645, 48)
        Me.TotalFixationsTextBox.Name = "TotalFixationsTextBox"
        Me.TotalFixationsTextBox.Size = New System.Drawing.Size(60, 22)
        Me.TotalFixationsTextBox.TabIndex = 7
        '
        'NumberOfSubjectsTextBox
        '
        Me.NumberOfSubjectsTextBox.Enabled = False
        Me.NumberOfSubjectsTextBox.Location = New System.Drawing.Point(645, 20)
        Me.NumberOfSubjectsTextBox.Name = "NumberOfSubjectsTextBox"
        Me.NumberOfSubjectsTextBox.Size = New System.Drawing.Size(60, 22)
        Me.NumberOfSubjectsTextBox.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.StatusTextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 359)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1072, 138)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Status"
        '
        'HeatmapForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1094, 504)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.HeatmapGroupBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "HeatmapForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Generate Heatmaps"
        Me.MainMenuStrip.ResumeLayout(False)
        Me.MainMenuStrip.PerformLayout()
        CType(Me.HeatmapPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.HeatmapGroupBox.ResumeLayout(False)
        Me.HeatmapGroupBox.PerformLayout()
        CType(Me.AlphaTrackBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadiusNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MainMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportFixationLocationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HeatmapPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ExportHeatmapsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HeatmapsListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents StatusTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents HeatmapGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents LoadStimulusImageButton As System.Windows.Forms.Button
    Friend WithEvents StimulusImageTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalFixationsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NumberOfSubjectsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents MainSaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MainOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ClearFixationLocationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearStimulusImageButton As System.Windows.Forms.Button
    Friend WithEvents SelectNoneButton As System.Windows.Forms.Button
    Friend WithEvents SelectAllButton As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents HighFixationsColorLabel As System.Windows.Forms.Label
    Friend WithEvents MidFixationsColorLabel As System.Windows.Forms.Label
    Friend WithEvents LowFixationsColorLabel As System.Windows.Forms.Label
    Friend WithEvents RadiusNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AlphaTrackBar As System.Windows.Forms.TrackBar
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MainColorDialog As System.Windows.Forms.ColorDialog
End Class
