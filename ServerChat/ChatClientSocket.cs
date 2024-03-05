using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat
{
    /// <summary>
    /// Client side of the chat application
    /// Connects to the server and sends/receives messages
    /// </summary>
    public class ChatClientSocket
    {
        private const int SERVER_PORT = 11_000;
        private const string SERVER_IP = "127.0.0.1";
        private Socket client;

        public bool IsConnected => client != null && client.Connected;

        /// <summary>
        /// Event that is triggered whenever a message is received
        /// </summary>
        /// <param name="message">Message received</param>
        public delegate void MessageReceived(string message);
        public MessageReceived? OnMessageReceived;

        /// <summary>
        /// Event that is triggered whenever a message is sent
        /// </summary>
        /// <param name="message">Message sent</param>
        public delegate void MessageSent(string message);
        public MessageSent? OnMessageSent;
        
        /// <summary>
        /// Event that is triggered when the client is disconnecting
        /// </summary>
        public delegate void Disconnecting();
        public Disconnecting? OnDisconnect;

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
            //Starts a listening thread
            Task.Run(async () =>
            {
                while (IsConnected) //While the client is connected (Note : it never disconnects for some reason)
                {
                    byte[] buffer = new byte[1024];
                    await client.ReceiveAsync(buffer, SocketFlags.None); //Blocks until a message is received
                    string message = Encoding.UTF8.GetString(buffer).Trim('\0');
                    OnMessageReceived?.Invoke(message); //Triggers the message received event
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
            OnMessageSent?.Invoke(message); //Triggers the message sent event
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Client is not connected.");
            }
            OnDisconnect?.Invoke(); //Triggers the disconnecting event
            client.Disconnect(false);
        }
    }
}
