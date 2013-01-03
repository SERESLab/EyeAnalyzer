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
        Me.ImportFixationLocationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeatmapPictureBox = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExportHeatmapsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SegmentsListBox = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.StatusTextBox = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.HeatmapGroupBox = New System.Windows.Forms.GroupBox()
        Me.NumberOfSubjectsTextBox = New System.Windows.Forms.TextBox()
        Me.TotalFixationsTextBox = New System.Windows.Forms.TextBox()
        Me.StimulusImageTextBox = New System.Windows.Forms.TextBox()
        Me.LoadStimulusImageButton = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.MainSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MainOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ClearFixationLocationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenuStrip.SuspendLayout()
        CType(Me.HeatmapPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.HeatmapGroupBox.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Size = New System.Drawing.Size(931, 28)
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
        'ImportFixationLocationsToolStripMenuItem
        '
        Me.ImportFixationLocationsToolStripMenuItem.Name = "ImportFixationLocationsToolStripMenuItem"
        Me.ImportFixationLocationsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ImportFixationLocationsToolStripMenuItem.Text = "&Import Fixation Locations"
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
        Me.HeatmapPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.HeatmapPictureBox.Location = New System.Drawing.Point(6, 20)
        Me.HeatmapPictureBox.Name = "HeatmapPictureBox"
        Me.HeatmapPictureBox.Size = New System.Drawing.Size(320, 180)
        Me.HeatmapPictureBox.TabIndex = 2
        Me.HeatmapPictureBox.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(344, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Number of subjects:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(344, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Total fixation count:"
        '
        'ExportHeatmapsToolStripMenuItem
        '
        Me.ExportHeatmapsToolStripMenuItem.Name = "ExportHeatmapsToolStripMenuItem"
        Me.ExportHeatmapsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ExportHeatmapsToolStripMenuItem.Text = "&Export Heatmaps"
        '
        'SegmentsListBox
        '
        Me.SegmentsListBox.FormattingEnabled = True
        Me.SegmentsListBox.Location = New System.Drawing.Point(6, 20)
        Me.SegmentsListBox.Name = "SegmentsListBox"
        Me.SegmentsListBox.Size = New System.Drawing.Size(320, 174)
        Me.SegmentsListBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(344, 158)
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
        Me.StatusTextBox.Size = New System.Drawing.Size(896, 111)
        Me.StatusTextBox.TabIndex = 7
        Me.StatusTextBox.Text = ""
        Me.StatusTextBox.WordWrap = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SegmentsListBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 31)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 214)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stimulus Segments"
        '
        'HeatmapGroupBox
        '
        Me.HeatmapGroupBox.Controls.Add(Me.LoadStimulusImageButton)
        Me.HeatmapGroupBox.Controls.Add(Me.StimulusImageTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.TotalFixationsTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.NumberOfSubjectsTextBox)
        Me.HeatmapGroupBox.Controls.Add(Me.HeatmapPictureBox)
        Me.HeatmapGroupBox.Controls.Add(Me.Label1)
        Me.HeatmapGroupBox.Controls.Add(Me.Label2)
        Me.HeatmapGroupBox.Controls.Add(Me.Label3)
        Me.HeatmapGroupBox.Enabled = False
        Me.HeatmapGroupBox.Location = New System.Drawing.Point(360, 31)
        Me.HeatmapGroupBox.Name = "HeatmapGroupBox"
        Me.HeatmapGroupBox.Size = New System.Drawing.Size(560, 214)
        Me.HeatmapGroupBox.TabIndex = 9
        Me.HeatmapGroupBox.TabStop = False
        Me.HeatmapGroupBox.Text = "Stimulus Heatmap"
        '
        'NumberOfSubjectsTextBox
        '
        Me.NumberOfSubjectsTextBox.Enabled = False
        Me.NumberOfSubjectsTextBox.Location = New System.Drawing.Point(484, 21)
        Me.NumberOfSubjectsTextBox.Name = "NumberOfSubjectsTextBox"
        Me.NumberOfSubjectsTextBox.Size = New System.Drawing.Size(60, 22)
        Me.NumberOfSubjectsTextBox.TabIndex = 7
        '
        'TotalFixationsTextBox
        '
        Me.TotalFixationsTextBox.Enabled = False
        Me.TotalFixationsTextBox.Location = New System.Drawing.Point(484, 49)
        Me.TotalFixationsTextBox.Name = "TotalFixationsTextBox"
        Me.TotalFixationsTextBox.Size = New System.Drawing.Size(60, 22)
        Me.TotalFixationsTextBox.TabIndex = 7
        '
        'StimulusImageTextBox
        '
        Me.StimulusImageTextBox.Enabled = False
        Me.StimulusImageTextBox.Location = New System.Drawing.Point(347, 178)
        Me.StimulusImageTextBox.Name = "StimulusImageTextBox"
        Me.StimulusImageTextBox.Size = New System.Drawing.Size(161, 22)
        Me.StimulusImageTextBox.TabIndex = 7
        '
        'LoadStimulusImageButton
        '
        Me.LoadStimulusImageButton.Location = New System.Drawing.Point(514, 177)
        Me.LoadStimulusImageButton.Name = "LoadStimulusImageButton"
        Me.LoadStimulusImageButton.Size = New System.Drawing.Size(30, 23)
        Me.LoadStimulusImageButton.TabIndex = 8
        Me.LoadStimulusImageButton.Text = "..."
        Me.LoadStimulusImageButton.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.StatusTextBox)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 251)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(908, 138)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Status"
        '
        'ClearFixationLocationsToolStripMenuItem
        '
        Me.ClearFixationLocationsToolStripMenuItem.Enabled = False
        Me.ClearFixationLocationsToolStripMenuItem.Name = "ClearFixationLocationsToolStripMenuItem"
        Me.ClearFixationLocationsToolStripMenuItem.Size = New System.Drawing.Size(246, 24)
        Me.ClearFixationLocationsToolStripMenuItem.Text = "C&lear Fixation Locations"
        '
        'HeatmapForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(931, 401)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.HeatmapGroupBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MainMenuStrip = Me.MainMenuStrip
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
    Friend WithEvents SegmentsListBox As System.Windows.Forms.CheckedListBox
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
End Class
