<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ServerWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        SuspendLayout()
        ' 
        ' ListView1
        ' 
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1})
        ListView1.HeaderStyle = ColumnHeaderStyle.None
        ListView1.Location = New Point(12, 56)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(620, 353)
        ListView1.TabIndex = 1
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' ServerWindow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(671, 450)
        Controls.Add(ListView1)
        Name = "ServerWindow"
        Text = "Chat Window"
        ResumeLayout(False)
    End Sub
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
End Class
