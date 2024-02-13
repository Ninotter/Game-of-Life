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
            SuspendLayout();
            // 
            // labelConnecting
            // 
            labelConnecting.AutoSize = true;
            labelConnecting.Location = new Point(364, 31);
            labelConnecting.Name = "labelConnecting";
            labelConnecting.Size = new Size(93, 20);
            labelConnecting.TabIndex = 0;
            labelConnecting.Text = "Connecting...";
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelConnecting);
            Name = "ChatForm";
            Text = "ChatForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelConnecting;
    }
}