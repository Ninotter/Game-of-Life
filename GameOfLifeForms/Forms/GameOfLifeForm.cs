using GameOfLifeAPI;
using GameOfLifeForms.Forms;

namespace GameOfLifeForms
{
    internal partial class GameOfLifeForm : Form, IGameOfLifeHost
    {
        Thread refreshImageThread;

        private GoLManager GoLManager { get; set; }

        public byte Underpopulation { get => GoLManager.UnderPopulation; set { GoLManager.UnderPopulation = value; } }
        public byte Overpopulation { get => GoLManager.OverPopulation; set { GoLManager.OverPopulation = value; } }
        public byte Reproduction { get => GoLManager.Reproduction; set { GoLManager.Reproduction = value; } }

        public GameOfLifeForm()
        {
            InitializeComponent();
            GoLManager = new GoLManager();
        }

        private void GameOfLifeForm_Load(object sender, EventArgs e)
        {
            StartGame();
        }

        public void StartGame()
        {
            refreshImageThread?.Interrupt();
            Thread t = new Thread(() =>
            {
                bool[,] startArray = GoLManager.CreateRandomArray(180, 100);
                try
                {
                    while (true)
                    {
                        ReplaceBitmap(CreateConwayBitmap(startArray));
                        startArray = GoLManager.NextGeneration(startArray);
                    }
                }catch(ThreadInterruptedException)
                {
                    // Do nothing
                }
            });
            refreshImageThread = t;
            refreshImageThread.Start();
        }

        private void ReplaceBitmap(Bitmap bitmap)
        {
            this.Invoke(new Action(() =>
            {
                pictureBoxGame.Image = bitmap;
            }));
        }

        private Bitmap CreateConwayBitmap(bool[,] array)
        {
            Bitmap bitmap = new Bitmap(array.GetLength(0) * 9, array.GetLength(1) * 9);
            Random rnd = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            bitmap.SetPixel(i * 9 + k, j * 9 + l, array[i, j] ? Color.White : Color.Black);
                        }
                    }
                }
            }
            return bitmap;
        }

        private void OpenLoginForm()
        {
            LoginForm loginForm = new LoginForm(this);
            loginForm.ShowDialog();
        }
        private void OpenRegisterForm()
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLoginForm();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRegisterForm();
        }

        private void GameOfLifeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshImageThread?.Interrupt();
        }
    }
}