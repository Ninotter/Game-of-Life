using GameOfLifeAPI;
using GameOfLifeForms.Forms.Chat;

namespace GameOfLifeForms.Forms
{
    internal partial class GameOfLifeForm : Form, IGameOfLifeHost
    {
        Thread refreshImageThread;

        private int SleepSpeed { get; set; }

        private GoLManager GoLManager { get; set; }

        bool[,] golArray;

        List<Action> awaitingEvents = new List<Action>();

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
                golArray = GoLManager.CreateRandomArray(180, 100);
                try
                {
                    while (true)
                    {
                        ReplaceBitmap(CreateConwayBitmap(golArray));
                        golArray = GoLManager.NextGeneration(golArray);
                        HandleAwaitingEvents();
                        Thread.Sleep(SleepSpeed);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    // Do nothing
                }
            });
            refreshImageThread = t;
            refreshImageThread.Start();
        }

        private void HandleAwaitingEvents()
        {
            foreach (Action action in awaitingEvents)
            {
                action.Invoke();
            }
            awaitingEvents.Clear();
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

        private void OpenChatForm()
        {
            ChatForm chatForm = new ChatForm();
            chatForm.Show();
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

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChatForm();
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            SleepSpeed = 1000 - trackBarSpeed.Value * 100;
        }

        private void pictureBoxGame_Click(object sender, EventArgs e)
        {
            //get x and y of the click
            int x = ((MouseEventArgs)e).X / 9;
            int y = ((MouseEventArgs)e).Y / 9;

            Action action = () =>
            {
                try
                {
                    golArray = GoLManager.AddGlider(golArray, x, y);
                }
                catch
                {
                    // empty on purpose
                }
            };
            awaitingEvents.Add(action);
        }
    }
}