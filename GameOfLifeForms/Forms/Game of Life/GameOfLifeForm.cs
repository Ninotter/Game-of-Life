using GameOfLifeAPI;
using GameOfLifeForms.Forms.Chat;

namespace GameOfLifeForms.Forms
{
    internal partial class GameOfLifeForm : Form, IGameOfLifeHost
    {
        /// <summary>
        /// Thread that refreshes the image of the game of life
        /// </summary>
        Thread refreshImageThread;

        /// <summary>
        /// Sets the time to wait between each generation
        /// </summary>
        private int SleepSpeed { get; set; }

        /// <summary>
        /// Game of life manager
        /// </summary>
        private GoLManager GoLManager { get; set; }

        /// <summary>
        /// Array storing the state of the game of life
        /// </summary>
        bool[,] golArray;

        /// <summary>
        /// Manages the list of events awaiting to be executed
        /// Such as creating a glider at a specific position
        /// </summary>
        List<Action> awaitingEvents = new List<Action>();

        // IGameOfLifeHost implementation
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
            // Stop the previous game
            refreshImageThread?.Interrupt();
            // Start a new game
            Thread t = new Thread(() =>
            {
                golArray = GoLManager.CreateRandomArray(180, 100);
                try
                {
                    while (true)
                    {
                        //Create a bitmap from the current array and replace the current image
                        ReplaceBitmap(CreateConwayBitmap(golArray));
                        //Calculate the next generation
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

        /// <summary>
        /// Creates a bitmap from the game of life array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Kills image generation thread on close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameOfLifeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshImageThread?.Interrupt();
        }

        private void chatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChatForm();
        }

        /// <summary>
        /// Triggers whenever the slider is moved
        /// Changes the speed of the game of life according to the slider value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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