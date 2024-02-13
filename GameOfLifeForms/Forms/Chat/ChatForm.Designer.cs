namespace GameOfLifeForms.Forms.Chat
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelConnecting = new Label();
            textBoxChatInput = new TextBox();
            panelChatHistory = new Panel();
            SuspendLayout();
            // 
            // labelConnecting
            // 
            labelConnecting.AutoSize = true;
            labelConnecting.Location = new Point(350, 176);
            labelConnecting.Name = "labelConnecting";
            labelConnecting.Size = new Size(93, 20);
            labelConnecting.TabIndex = 0;
            labelConnecting.Text = "Connecting...";
            // 
            // textBoxChatInput
            // 
            textBoxChatInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxChatInput.Location = new Point(12, 346);
            textBoxChatInput.Multiline = true;
            textBoxChatInput.Name = "textBoxChatInput";
            textBoxChatInput.Size = new Size(776, 92);
            textBoxChatInput.TabIndex = 1;
            textBoxChatInput.Visible = false;
            textBoxChatInput.KeyDown += textBoxChatInput_KeyDown;
            textBoxChatInput.KeyPress += textBoxChatInput_KeyPress;
            // 
            // panelChatHistory
            // 
            panelChatHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelChatHistory.Location = new Point(12, 12);
            panelChatHistory.Name = "panelChatHistory";
            panelChatHistory.Size = new Size(776, 328);
            panelChatHistory.TabIndex = 2;
            panelChatHistory.Visible = false;
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelConnecting);
            Controls.Add(panelChatHistory);
            Controls.Add(textBoxChatInput);
            Name = "ChatForm";
            Text = "ChatForm";
            FormClosing += ChatForm_FormClosing;
            Load += ChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelConnecting;
        private TextBox textBoxChatInput;
        private Panel panelChatHistory;
    }
}