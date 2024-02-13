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

        public bool IsConnected => client != null && client.Connected;

        public delegate void MessageReceived(string message);
        public MessageReceived? OnMessageReceived;

        public delegate void MessageSent(string message);
        public MessageSent? OnMessageSent;

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
            await client.ConnectAsync(SERVER_IP, SERVER_PORT);
        }


        public void Listen()
        {
            if(!IsConnected)
            {
                throw new InvalidOperationException("Client is not connected.");
            }
            Task.Run(async () =>
            {
                while (IsConnected)
                {
                    byte[] buffer = new byte[1024];
                    await client.ReceiveAsync(buffer, SocketFlags.None);
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                    OnMessageReceived?.Invoke(message);
                }
            });
        }

        public void SendMessage(string message)
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Client is not connected.");
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            client.SendAsync(messageBytes, SocketFlags.None);
            OnMessageSent?.Invoke(message);
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Client is not connected.");
            }
            client.Disconnect(false);
        }
    }
}
