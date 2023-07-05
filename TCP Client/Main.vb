
Imports Microsoft.SqlServer
Imports SuperSimpleTcp
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Module Main
    Public MainCTS As CancellationTokenSource
    Public WorkCTS As CancellationTokenSource
    Public Property client As SimpleTcpClient
    Public Property MainWindow As ChatWindow
    Public Property ClientConnected As Boolean
    Public Sub Main()
        MainWindow = New ChatWindow()
        MainCTS = New CancellationTokenSource()
        While MainCTS.IsCancellationRequested = False
            WorkCTS = New CancellationTokenSource()
            ClientConnected = False
            Application.EnableVisualStyles()
            MainWindow.Run()

            While MainWindow.Visible = False
                Threading.Thread.Sleep(100)
            End While

            ' once we have connected send hello...
            While ClientConnected = False
                If (MainCTS.IsCancellationRequested = True) Then
                    WorkCTS.Cancel()
                    Exit While
                End If

                If (WorkCTS.IsCancellationRequested = True) Then
                    MainCTS.Cancel()
                    Exit While
                End If
                Threading.Thread.Sleep(100)
            End While

            ' once we have disconnected...
            While ClientConnected = True
                If WorkCTS.IsCancellationRequested = True Then
                    DisconnectFromServer()
                    Debug.WriteLine("Ended session.")
                End If
                Threading.Thread.Sleep(100)
            End While
        End While
    End Sub
    Public Sub DisconnectFromServer()
        MainWindow.ChangeButton1Text(IIf(client.IsConnected, "Disconnect...", "Connect..."))
        client.Disconnect()
    End Sub

    Public Sub ConnectToServer()
        Dim ipToConnect As String = Nothing
        ipToConnect = MainWindow.GetIPToConnect()
        client = New SimpleTcpClient(ipToConnect)

        ' set events
        AddHandler client.Events.Connected, AddressOf Connected
        AddHandler client.Events.Disconnected, AddressOf Disconnected
        AddHandler client.Events.DataReceived, AddressOf DataReceived

        ' let's go!
        Try
            client.ConnectWithRetries(1000)
        Catch ex As TimeoutException
            Debug.WriteLine(ex.Message)
            Debug.WriteLine(ex.StackTrace)
            MainWindow.AddMessageToTextBox(ex.Message())
        End Try
        MainWindow.ChangeButton1Text(IIf(client.IsConnected, "Disconnect...", "Connect..."))
        If client.IsConnected Then
            ClientConnected = True
        End If
    End Sub
    Public Sub GUICancelled()
        WorkCTS.Cancel()
    End Sub
    Public Sub Connected(sender As Object, e As ConnectionEventArgs)
        Debug.WriteLine($"*** Server {e.IpPort} connected")
        MainWindow.AddMessageToTextBox($"*** Server {e.IpPort} connected")
        client.Send("Hello, server!")
        MainWindow.ChangeButton1Text(IIf(client.IsConnected, "Disconnect...", "Connect..."))
        ClientConnected = True
    End Sub

    Public Sub Disconnected(sender As Object, e As ConnectionEventArgs)
        Debug.WriteLine($"*** Server {e.IpPort} disconnected")
        MainWindow.AddMessageToTextBox($"*** Server {e.IpPort} disconnected")
        MainWindow.ChangeButton1Text(IIf(client.IsConnected, "Disconnect...", "Connect..."))
        ClientConnected = False
    End Sub
    Public Sub DataReceived(sender As Object, e As DataReceivedEventArgs)
        Console.WriteLine($"[{e.IpPort}] {UTF8Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}")
        MainWindow.AddMessageToTextBox($"[{e.IpPort}] {UTF8Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}")
    End Sub
    Public Sub SendMessageToServer()
        Dim TextToSend As String = Nothing
        TextToSend = MainWindow.GetTextToSend()
        client.Send(TextToSend)
        MainWindow.AddMessageToTextBox(TextToSend)
    End Sub
End Module
