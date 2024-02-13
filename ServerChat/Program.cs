using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerChat;
using System.Runtime.CompilerServices;

Log.Write(LogType.Info, "Start : ");

List<ChatClient> clients = new List<ChatClient>();
const int SERVER_PORT = 11_000;

Log.Write(LogType.Info, "Starting up dispatcher socket...");
//Server socket
IPAddress serverIpAdress = IPAddress.Parse($"127.0.0.1");
IPEndPoint serverIpEndPoint = new(serverIpAdress, SERVER_PORT);
Socket serverListener = new(serverIpAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
serverListener.Bind(serverIpEndPoint);
serverListener.Listen(5);

Log.Write(LogType.Info, "Server socket started.");

Task.Run(() =>
{
    Thread.Sleep(2000);
    FakeTestClient();
    Thread.Sleep(5000);
    FakeTestClient();
});


while (true)
{
    //Listen for incoming connections
    Log.Write(LogType.Info, "Waiting for incoming connections...");
    Socket clientSocket = await serverListener.AcceptAsync();
    Log.Write(LogType.Info, $"Client {clientSocket.AddressFamily} connected.");

    ChatClient client = new ChatClient(clientSocket);
    clients.Add(client);
    client.Id = clients.Count;
    client.Listen();
}

async void FakeTestClient()
{
    using Socket client = new(
    serverIpEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);

    await client.ConnectAsync(serverIpEndPoint);
    while (true)
    {
        // Send message.
        var message = "TESTTTT";
        var messageBytes = Encoding.UTF8.GetBytes(message);
        _ = await client.SendAsync(messageBytes, SocketFlags.None);
        Thread.Sleep(2000);
    }
}