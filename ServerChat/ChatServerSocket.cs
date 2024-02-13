using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerChat
{
    internal class ChatServerSocket
    {
        private Socket clientSocket;
        public List<ChatServerSocket> clients = new List<ChatServerSocket>();
        internal int Id { get; set; }
        internal bool Stopped { get; set; } = false;
        internal ChatServerSocket(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        internal void Listen()
        {
            Task.Run(async () => { 
                while (!Stopped || !clientSocket.Connected) { 
                    byte[] buffer = new byte[1024];
                    await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
                    Log.Write(LogType.Warning, "Received message");
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                    Log.Write(LogType.Info, $"Message received from Task {this.Id} : {message}");
                    BroadcastMessage(message);
                }
            });
        }

        internal void BroadcastMessage(string message)
        {
            foreach (var client in clients)
            {
                client.SendMessage(message);
            }
        }

        internal void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            clientSocket.SendAsync(messageBytes, SocketFlags.None);
        }
    }
}
