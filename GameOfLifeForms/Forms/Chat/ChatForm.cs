using ServerChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeForms.Forms.Chat
{
    public partial class ChatForm : Form
    {
        private ChatClientSocket _client;

        public ChatForm()
        {
            InitializeComponent();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            _client = new ChatClientSocket();
            _client.OnMessageReceived += OnMessageReceived; //Starts listening to message event
            Task.Run(() => TryConnect()); //Starts connection thread
        }

        /// <summary>
        /// Triggers when a message is received
        /// </summary>
        /// <param name="message"></param>
        private void OnMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnMessageReceived(message)));
                return;
            }
            AddExternalMessage(message);
        }

        /// <summary>
        /// When connection is established, show chat
        /// </summary>
        private void ShowChat()
        {
            labelConnecting.Visible = false;
            textBoxChatInput.Visible = true;
            panelChatHistory.Visible = true;
        }

        /// <summary>
        /// Try to connect to client socket
        /// </summary>
        private void TryConnect()
        {
            try
            {
                _client.Connect();
                Invoke(new Action(ShowChat));
                _client.Listen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sends a message to socket client
        /// Adds the message to the chat history
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            _client.SendMessage(message);
            AddOwnMessage(message);
        }

        /// <summary>
        /// Adds a message sent by this client to the chat history
        /// </summary>
        /// <param name="message"></param>
        private void AddOwnMessage(string message)
        {
            //Adds a panelMessage to the right of the panel
            PanelMessage panelMessage = new PanelMessage(message);
            panelChatHistory.Controls.Add(panelMessage);
            panelMessage.Anchor = AnchorStyles.Right;
            panelMessage.Width = panelChatHistory.Width - 40;
            panelMessage.Location = new Point(panelChatHistory.Width - panelMessage.Width - 20, panelChatHistory.Controls.Count * 40);
        }

        /// <summary>
        /// Adds a message received from another client to the chat history
        /// </summary>
        /// <param name="message"></param>
        private void AddExternalMessage(string message)
        {
            //Adds a panelMessage to the left of the panel
            PanelMessage panelMessage = new PanelMessage(message);
            panelChatHistory.Controls.Add(panelMessage);
            panelMessage.Anchor = AnchorStyles.Left;
            panelMessage.Width = panelChatHistory.Width - 40;
            panelMessage.Location = new Point(20, panelChatHistory.Controls.Count * 40);
        }

        /// <summary>
        /// Sends message when enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SendMessage(textBoxChatInput.Text);
                textBoxChatInput.Text = "";
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Blocks new lines from being inserted whenever enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxChatInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if key = enter
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        /// <summary>
        /// Disconnects client when form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _client?.Disconnect();
            }
            catch
            {
                // empty on purpose
            }
        }

    }
}
