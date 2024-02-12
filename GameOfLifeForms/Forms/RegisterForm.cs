using GameOfLifeAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeForms.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "Username" || textBoxPassword.Text == "Password")
            {
                MessageBox.Show("Please enter a username and password");
                return;
            }

            try
            {
                DbConnect db = new DbConnect(Program.SqliteDbPath);
                db.Register(textBoxUsername.Text, textBoxPassword.Text, (int)numericUpDownUnderpopulation.Value, (int)numericUpDownOverpopulation.Value, (int)numericUpDownReproduction.Value);
                MessageBox.Show("User registered");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not register : {ex.Message}");
            }
        }
    }
}
