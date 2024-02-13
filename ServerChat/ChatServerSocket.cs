using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerChat
{
    internal class ChatServerSocket
    {
        private Socket _socket;
        internal int Id { get; set; }
        internal bool Stopped { get; set; } = false;
        internal ChatServerSocket(Socket clientSocket)
        {
            _socket = clientSocket;
        }

        internal void Listen()
        {
            Task.Run(async () => { 
                while (!Stopped) { 
                    byte[] buffer = new byte[1024];
                    await _socket.ReceiveAsync(buffer, SocketFlags.None);
                    Log.Write(LogType.Warning, "Received message");
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                    Log.Write(LogType.Info, $"Message received from Task {this.Id} : {message}");
                }
            });
        }

        internal void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            _socket.SendAsync(messageBytes, SocketFlags.None);
        }
    }
}
