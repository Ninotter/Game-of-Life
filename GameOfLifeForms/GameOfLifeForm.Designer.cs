﻿namespace GameOfLifeForms
{
    partial class GameOfLifeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBoxGame = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGame).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxGame
            // 
            pictureBoxGame.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxGame.BackColor = Color.Black;
            pictureBoxGame.Location = new Point(0, 0);
            pictureBoxGame.Name = "pictureBoxGame";
            pictureBoxGame.Size = new Size(800, 450);
            pictureBoxGame.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxGame.TabIndex = 0;
            pictureBoxGame.TabStop = false;
            // 
            // GameOfLifeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBoxGame);
            Name = "GameOfLifeForm";
            Text = "Game of Life";
            WindowState = FormWindowState.Maximized;
            FormClosing += GameOfLifeForm_FormClosing;
            Load += GameOfLifeForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxGame).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxGame;
    }
}