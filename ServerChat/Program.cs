using System.Net.Sockets;
using System.Net;
using System.Text;
using ServerChat;

Log.Write(LogType.Info, "Start : ");

//List of connected clients
List<ChatServerSocket> clients = new List<ChatServerSocket>();
//Custom server port
const int SERVER_PORT = 11_000;

Log.Write(LogType.Info, "Starting up dispatcher socket...");
//Server socket
IPAddress serverIpAdress = IPAddress.Parse($"127.0.0.1");
IPEndPoint serverIpEndPoint = new(serverIpAdress, SERVER_PORT);
Socket serverListener = new(serverIpAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
serverListener.Bind(serverIpEndPoint);
//Listen to up to 5 clients
serverListener.Listen(5);

Log.Write(LogType.Info, "Server socket started.");

//Task.Run(() =>
//{
//    Thread.Sleep(2000);
//    FakeTestClient();
//    Thread.Sleep(5000);
//    FakeTestClient();
//});

while (true)
{
    //Listen for incoming connections
    Log.Write(LogType.Info, "Waiting for incoming connections...");
    Socket serverSocket = await serverListener.AcceptAsync();
    Log.Write(LogType.Info, $"Client {serverSocket.AddressFamily} connected.");

    ChatServerSocket newClient = new ChatServerSocket(serverSocket); //Create new client
    newClient.OnDisconnected += (client) => //Adds a listener to the client's disconnect event
    {
        Log.Write(LogType.Info, $"Client {client.Id} disconnected.");
        clients.Remove(client);
    };
    foreach (var c in clients) //Broadcasts the new client to all other clients
    {
        c.clients.Add(newClient);
    }
    newClient.clients.AddRange(clients); //Adds all other clients to the new client
    clients.Add(newClient); //Adds the new client to the list
    newClient.Id = clients.Count;
    newClient.Listen();
}


//Debug test method
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