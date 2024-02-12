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
    internal partial class LoginForm : Form
    {
        private User user;
        private IGameOfLifeHost host;

        internal LoginForm(IGameOfLifeHost hostForm)
        {
            InitializeComponent();
            host = hostForm;
        }

        private void buttonConfirmLogin_Click(object sender, EventArgs e)
        {
            try
            {
                DbConnect db = new DbConnect(Program.SqliteDbPath);
                user = db.Login(textBox1.Text, textBox2.Text);
                host.Underpopulation = user.Underpopulation;
                host.Overpopulation = user.Overpopulation;
                host.Reproduction = user.Reproduction;
                host.StartGame();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Could not login : {ex.Message}");
            }
        }
    }
}
