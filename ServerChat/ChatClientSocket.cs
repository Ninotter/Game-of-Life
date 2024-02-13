using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat
{
    public class ChatClientSocket
    {
        private const int SERVER_PORT = 11_000;
        private const string SERVER_IP = "127.0.0.1";
        private Socket client;

        public ChatClientSocket()
        {
            IPAddress serverIpAdress = IPAddress.Parse(SERVER_IP);
            client = new(
                serverIpAdress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
        }

        public async void Connect()
        {
            Task tsk = new Task( async() => { 
                await client.ConnectAsync(SERVER_IP, SERVER_PORT);
            });
            await tsk.WaitAsync(TimeSpan.FromSeconds(10)); // tries for 10 seconds
            if (tsk.IsCompleted)
            {
                return;
            }
            else
            {
                throw new TimeoutException("Couldnt access socket.");
            }
        }


        public void Listen()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    await client.ReceiveAsync(buffer, SocketFlags.None);
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                }
            });
        }
        public void SendMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.SendAsync(messageBytes, SocketFlags.None);
        }
    }
}
