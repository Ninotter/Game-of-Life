using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerChat
{
    /// <summary>
    /// Server side of the chat application
    /// Should have only one instance running (implement singleton + mutex?)
    /// </summary>
    internal class ChatServerSocket
    {
        private Socket clientSocket;
        public List<ChatServerSocket> clients = new List<ChatServerSocket>();
        internal int Id { get; set; }

        public delegate void Disconnected(ChatServerSocket serverSocket);
        public Disconnected? OnDisconnected;

        internal ChatServerSocket(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        /// <summary>
        /// Listening thread
        /// NOTE: Once a client disconnects, the loop doesn't stop, tried to fix it but couldn't
        /// </summary>
        internal void Listen()
        {
            //Starts a listening thread
            Task.Run(async () => { 
                while (clientSocket.Connected) //Never stops listening
                { 
                    byte[] buffer = new byte[1024];
                    await clientSocket.ReceiveAsync(buffer, SocketFlags.None); //Blocks thread until a message is received
                    Log.Write(LogType.Warning, "Received message");
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                    Log.Write(LogType.Info, $"Message received from Task {this.Id} : {message}");
                    BroadcastMessage(message); //Broadcast received message to all clients
                }
                OnClientDisconnected();
            });
        }

        internal void BroadcastMessage(string message)
        {
            foreach (var client in clients)
            {
                client.SendMessage(message);
            }
        }

        private void OnClientDisconnected()
        {
            clientSocket.Disconnect(false);
            clients.Remove(this);
            clientSocket.Close();
            OnDisconnected?.Invoke(this);
        }

        internal void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            clientSocket.SendAsync(messageBytes, SocketFlags.None);
        }
    }
}
