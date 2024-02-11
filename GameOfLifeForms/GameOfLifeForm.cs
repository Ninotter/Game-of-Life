using GameOfLifeCore;

namespace GameOfLifeForms
{
    public partial class GameOfLifeForm : Form
    {
        public GameOfLifeForm()
        {
            InitializeComponent();
        }


        private void GameOfLifeForm_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(StartGame);
            thread.Start();
        }

        private void StartGame(object? obj)
        {
            bool[,] startArray = GoLCore.CreateRandomArray(180, 100);
            while (true)
            {
                ReplaceBitmap(CreateConwayBitmap(startArray));
                startArray = GoLCore.NextGeneration(startArray);
                //Thread.Sleep(50);
            }
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

        private void GameOfLifeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}