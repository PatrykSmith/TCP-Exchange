Imports System.Text
Imports System.Threading
Imports Microsoft.SqlServer
Imports SuperSimpleTcp

Public Module Main
    Public Property MainWindow As ServerWindow
    Public Property server As SimpleTcpServer

    Public cts As CancellationTokenSource
    Public Sub Main()
        ' instantiate
        cts = New CancellationTokenSource
        Application.EnableVisualStyles()
        MainWindow = New ServerWindow()
        MainWindow.Run()

        While MainWindow.Visible = False
            Threading.Thread.Sleep(100)
        End While

        server = New SimpleTcpServer("127.0.0.1:9000")

        ' set events
        AddHandler server.Events.ClientConnected, AddressOf ClientConnected
        AddHandler server.Events.ClientDisconnected, AddressOf ClientDisconnected
        AddHandler server.Events.DataReceived, AddressOf DataReceived

        ' let's go!
        server.Start()

        ' once a client has connected...

        While cts.IsCancellationRequested = False And server.Connections < 1
            Threading.Thread.Sleep(100)
        End While

        ' once we have connected send hello...
        While cts.IsCancellationRequested = False
            Threading.Thread.Sleep(100)
        End While

        cts.Dispose()
    End Sub

    Public Sub ClientConnected(sender As Object, e As ConnectionEventArgs)
        Debug.WriteLine($"[{e.IpPort}] connected")
        MainWindow.AddMessageToTextBox($"[{e.IpPort}] client connected")
        SendMessageToClients($"Hello, [{e.IpPort}]!", e.IpPort)
    End Sub
    Public Sub ClientDisconnected(sender As Object, e As ConnectionEventArgs)
        Debug.WriteLine($"[{e.IpPort}] disconnected: {e.Reason}")
        MainWindow.AddMessageToTextBox($"[{e.IpPort}] client disconnected: {e.Reason}")
    End Sub

    Public Sub DataReceived(sender As Object, e As DataReceivedEventArgs)
        Debug.WriteLine($"[{e.IpPort}]: {UTF8Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}")
        MainWindow.AddMessageToTextBox($"[{e.IpPort}]: {UTF8Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}")
    End Sub
    Public Sub SendMessageToClients(MessageToSend As String, IpPortToSend As String)
        MainWindow.AddMessageToTextBox("SERVER: " & MessageToSend)
        server.Send(IpPortToSend, MessageToSend)
    End Sub
End Module
