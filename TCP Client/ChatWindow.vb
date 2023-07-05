Imports System.Security.Cryptography.X509Certificates
Imports System.Threading

Public Class ChatWindow
    Public Connected As Boolean
    Public CTSOld As CancellationTokenSource
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        ' Pass the token to the cancelable operation.
        Me.Connected = False
        CTSOld = Nothing
    End Sub
    Public Sub Run()
        If (IsNothing(CTSOld)) Then
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf ServeMe), Main.WorkCTS.Token)
            CTSOld = Main.WorkCTS
            Exit Sub
        End If

        If (Main.WorkCTS.Token.WaitHandle.SafeWaitHandle.DangerousGetHandle <> CTSOld.Token.WaitHandle.SafeWaitHandle.DangerousGetHandle) Then
            Exit Sub
        End If
    End Sub
    Public Sub ServeMe(ByVal obj As Object)
        Dim token As CancellationToken = CType(obj, CancellationToken)
        While Not token.IsCancellationRequested
            If (Not IsNothing(Me)) And (Not Me.IsDisposed) Then
                Application.Run(Me)
            End If
            Threading.Thread.Sleep(10)
        End While
    End Sub
    Private Sub EnterKeyPressed(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If (IsNothing(e)) Then
            Exit Sub
        End If
        If e.KeyCode <> Keys.Enter Then
            Exit Sub
        End If

        Main.SendMessageToServer()
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If TextBox2.Text Like "Server Address" Then
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Connect or disconnec to server
        If IsNothing(Main.client) Then
            Button1.Text = "Disconnect..."
            Me.Connected = False
            ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Main.ConnectToServer), Main.WorkCTS.Token)

        Else
            Button1.Text = IIf(Main.client.IsConnected, "Disconnect...", "Connect...")
            Me.Connected = client.IsConnected

            If (Me.Connected) Then
                ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Main.client.Disconnect), Main.WorkCTS.Token)
            Else
                ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Main.ConnectToServer), Main.WorkCTS.Token)
            End If
        End If
    End Sub
    Public Sub ChangeButton1Text(TextToChange As String)
        If IsNothing(Me) Then
            Exit Sub
        End If
        If IsNothing(Me.Controls) Then
            Exit Sub
        End If

        Me.Invoke(Sub()
                      Button1.Text = TextToChange
                  End Sub)
    End Sub
    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        If TextBox1.Text Like "Type to chat..." Then
            TextBox1.Text = ""
        End If
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
    Private Sub ServerWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Invoke(Sub()

                      Me.ListView1.Items.Clear()
                      Me.ListView1.Columns(0).Text = ""
                      Me.ListView1.Columns(0).Width = Me.ListView1.Width

                  End Sub)
    End Sub

    Public Function GetTextToSend()
        Dim text As String = Nothing
        text = Me.Invoke(Function()
                             Return TextBox1.Text
                         End Function)
        Me.Invoke(Sub()
                      TextBox1.Text = ""
                  End Sub)
        Return text
    End Function
    Public Function GetIPToConnect()
        Dim text As String = Nothing
        text = Me.Invoke(Function()
                             Return TextBox2.Text
                         End Function)
        Return text
    End Function

    Private Sub FormIsClosing(sender As Object, e As EventArgs) Handles Me.Closed
        StopWorking()
    End Sub
    Public Sub StopWorking()
        Main.MainCTS.Cancel()
    End Sub

End Class
