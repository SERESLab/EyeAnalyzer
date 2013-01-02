''' <summary>
''' Form for constructing and exporting heatmaps from fixations.
''' </summary>
Public Class HeatmapForm

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub HeatmapForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        StatusTextBox.Text = "No fixations loaded."
    End Sub
End Class