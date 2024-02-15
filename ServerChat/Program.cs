using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerChat;

Log.Write(LogType.Info, "Start : ");

List<ChatServerSocket> clients = new List<ChatServerSocket>();
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
    //Thread.Sleep(2000);
    //FakeTestClient();
    //Thread.Sleep(5000);
    //FakeTestClient();
});

while (true)
{
    //Listen for incoming connections
    Log.Write(LogType.Info, "Waiting for incoming connections...");
    Socket serverSocket = await serverListener.AcceptAsync();
    Log.Write(LogType.Info, $"Client {serverSocket.AddressFamily} connected.");

    ChatServerSocket newClient = new ChatServerSocket(serverSocket);
    newClient.OnDisconnected += (client) =>
    {
        Log.Write(LogType.Info, $"Client {client.Id} disconnected.");
        clients.Remove(client);
    };
    foreach (var c in clients)
    {
        c.clients.Add(newClient);
    }
    newClient.clients.AddRange(clients);
    clients.Add(newClient);
    newClient.Id = clients.Count;
    newClient.Listen();
}


async void FakeTestClient()
{
    ChatClientSocket client = new();
    client.Connect();
    Task.Run(async () => {
        while (true)
        {
            // Send message.
            var message = "TESTTTT";
            client.SendMessage(message);
            Thread.Sleep(2000);
        }
    });
}