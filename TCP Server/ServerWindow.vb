Imports System.Threading

Public Class ServerWindow
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.  
    End Sub
    Public Sub Run()
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ServeMe), Main.cts.Token)
    End Sub
    Public Sub ServeMe(ByVal obj As Object)
        Dim token As CancellationToken = CType(obj, CancellationToken)
        While Not token.IsCancellationRequested
            If Not IsNothing(Me) Then
                Application.Run(Me)
            End If
            Threading.Thread.Sleep(10)
        End While
    End Sub
    Public Sub AddMessageToTextBox(messagetoAdd As String)
        If (IsNothing(Me)) Then
            Exit Sub
        End If

        If (IsNothing(Me.ActiveControl)) Then
            Exit Sub
        End If

        Me.Invoke(Sub()
                      Me.ListView1.Items.Add(messagetoAdd)
                  End Sub)
    End Sub
    Private Sub ServerWindow_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Invoke(Sub()
                      Me.ListView1.Items.Clear()
                      Me.ListView1.Columns(0).Text = ""
                      Me.ListView1.Columns(0).Width = Me.ListView1.Width
                  End Sub)
    End Sub
    Private Sub FormIsClosing(sender As Object, e As EventArgs) Handles Me.Closing
        StopWorking()
    End Sub
    Public Sub StopWorking()
        Main.cts.Cancel()
    End Sub
End Class
