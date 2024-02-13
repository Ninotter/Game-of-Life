namespace GameOfLifeForms.Forms
{
    partial class RegisterForm
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
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            numericUpDownUnderpopulation = new NumericUpDown();
            numericUpDownOverpopulation = new NumericUpDown();
            numericUpDownReproduction = new NumericUpDown();
            labelUnderpopulation = new Label();
            labelOverpopulation = new Label();
            labelReproduction = new Label();
            buttonRegister = new Button();
            labelUsername = new Label();
            labelPassword = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUnderpopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownOverpopulation).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownReproduction).BeginInit();
            SuspendLayout();
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(154, 64);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(125, 27);
            textBoxUsername.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(478, 64);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(125, 27);
            textBoxPassword.TabIndex = 1;
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // numericUpDownUnderpopulation
            // 
            numericUpDownUnderpopulation.Location = new Point(69, 208);
            numericUpDownUnderpopulation.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownUnderpopulation.Name = "numericUpDownUnderpopulation";
            numericUpDownUnderpopulation.Size = new Size(150, 27);
            numericUpDownUnderpopulation.TabIndex = 2;
            numericUpDownUnderpopulation.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // numericUpDownOverpopulation
            // 
            numericUpDownOverpopulation.Location = new Point(288, 208);
            numericUpDownOverpopulation.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownOverpopulation.Name = "numericUpDownOverpopulation";
            numericUpDownOverpopulation.Size = new Size(150, 27);
            numericUpDownOverpopulation.TabIndex = 3;
            numericUpDownOverpopulation.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // numericUpDownReproduction
            // 
            numericUpDownReproduction.Location = new Point(515, 208);
            numericUpDownReproduction.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownReproduction.Name = "numericUpDownReproduction";
            numericUpDownReproduction.Size = new Size(150, 27);
            numericUpDownReproduction.TabIndex = 4;
            numericUpDownReproduction.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // labelUnderpopulation
            // 
            labelUnderpopulation.AutoSize = true;
            labelUnderpopulation.Location = new Point(69, 185);
            labelUnderpopulation.Name = "labelUnderpopulation";
            labelUnderpopulation.Size = new Size(122, 20);
            labelUnderpopulation.TabIndex = 5;
            labelUnderpopulation.Text = "Underpopulation";
            // 
            // labelOverpopulation
            // 
            labelOverpopulation.AutoSize = true;
            labelOverpopulation.Location = new Point(288, 185);
            labelOverpopulation.Name = "labelOverpopulation";
            labelOverpopulation.Size = new Size(113, 20);
            labelOverpopulation.TabIndex = 6;
            labelOverpopulation.Text = "Overpopulation";
            // 
            // labelReproduction
            // 
            labelReproduction.AutoSize = true;
            labelReproduction.Location = new Point(515, 185);
            labelReproduction.Name = "labelReproduction";
            labelReproduction.Size = new Size(99, 20);
            labelReproduction.TabIndex = 7;
            labelReproduction.Text = "Reproduction";
            // 
            // buttonRegister
            // 
            buttonRegister.Location = new Point(307, 314);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(94, 29);
            buttonRegister.TabIndex = 8;
            buttonRegister.Text = "Register";
            buttonRegister.UseVisualStyleBackColor = true;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(154, 41);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(75, 20);
            labelUsername.TabIndex = 9;
            labelUsername.Text = "Username";
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(478, 41);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(70, 20);
            labelPassword.TabIndex = 10;
            labelPassword.Text = "Password";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelPassword);
            Controls.Add(labelUsername);
            Controls.Add(buttonRegister);
            Controls.Add(labelReproduction);
            Controls.Add(labelOverpopulation);
            Controls.Add(labelUnderpopulation);
            Controls.Add(numericUpDownReproduction);
            Controls.Add(numericUpDownOverpopulation);
            Controls.Add(numericUpDownUnderpopulation);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxUsername);
            Name = "RegisterForm";
            Text = "Register";
            ((System.ComponentModel.ISupportInitialize)numericUpDownUnderpopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownOverpopulation).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownReproduction).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxUsername;
        private TextBox textBoxPassword;
        private NumericUpDown numericUpDownUnderpopulation;
        private NumericUpDown numericUpDownOverpopulation;
        private NumericUpDown numericUpDownReproduction;
        private Label labelUnderpopulation;
        private Label labelOverpopulation;
        private Label labelReproduction;
        private Button buttonRegister;
        private Label labelUsername;
        private Label labelPassword;
    }
}