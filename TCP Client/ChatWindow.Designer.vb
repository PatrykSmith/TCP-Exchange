<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChatWindow
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
        TextBox1 = New TextBox()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        Button1 = New Button()
        TextBox2 = New TextBox()
        SuspendLayout()
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 415)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(620, 23)
        TextBox1.TabIndex = 0
        TextBox1.Text = "Type to chat..."' 
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
        ' Button1
        ' 
        Button1.Location = New Point(193, 24)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 2
        Button1.Text = "Connect..."
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(12, 25)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(175, 23)
        TextBox2.TabIndex = 4
        TextBox2.Text = "127.0.0.1:9000"' 
        ' ChatWindow
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(671, 450)
        Controls.Add(TextBox2)
        Controls.Add(Button1)
        Controls.Add(ListView1)
        Controls.Add(TextBox1)
        Name = "ChatWindow"
        Text = "Chat Window"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ColumnHeader1 As ColumnHeader
End Class
