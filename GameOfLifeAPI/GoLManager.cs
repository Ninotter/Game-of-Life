namespace GameOfLifeAPI
{
    public class GoLManager
    {
        public byte UnderPopulation { get; set; } = 2;
        public byte OverPopulation { get; set; } = 3;
        public byte Reproduction { get; set; } = 3;

        public GoLManager()
        {

        }
        public bool[,] CreateRandomArray(int rows, int columns)
        {
            bool[,] startArray = new bool[rows, columns];
            Random random = new Random();
            for (int i = 0; i < startArray.GetLength(0); i++)
            {
                for (int j = 0; j < startArray.GetLength(1); j++)
                {
                    startArray[i, j] = random.Next(0, 2) == 1;
                }
            }
            return startArray;
        }

        public bool[,] CreateArrayWithGlider(int rows, int columns)
        {
            bool[,] startArray = new bool[rows, columns];
            startArray[0, 1] = true;
            startArray[1, 2] = true;
            startArray[2, 0] = true;
            startArray[2, 1] = true;
            startArray[2, 2] = true;
            return startArray;
        }

        public bool[,] AddGlider(bool[,] currentArray, int x, int y)
        {
            bool[,] newArray = new bool[currentArray.GetLength(0), currentArray.GetLength(1)];
            for (int i = 0; i < currentArray.GetLength(0); i++)
            {
                for (int j = 0; j < currentArray.GetLength(1); j++)
                {
                    newArray[i, j] = currentArray[i, j];
                }
            }
            newArray[x, y] = true;
            newArray[x + 1, y + 2] = true;
            newArray[x + 2, y] = true;
            newArray[x + 2, y + 1] = true;
            newArray[x + 2, y + 2] = true;
            return newArray;
        }

        public bool[,] NextGeneration(bool[,] currentGeneration)
        {
            int rows = currentGeneration.GetLength(0);
            int columns = currentGeneration.GetLength(1);
            bool[,] nextGeneration = new bool[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    bool currentState = currentGeneration[i, j];
                    byte aliveNeighborCount = GetAliveNeighborCount(i, j, currentGeneration);
                    nextGeneration[i, j] = GetNextState(currentState, aliveNeighborCount);
                }
            }
            return nextGeneration;
        }

        private byte GetAliveNeighborCount(int i, int j, bool[,] currentGeneration)
        {
            int rows = currentGeneration.GetLength(0);
            int columns = currentGeneration.GetLength(1);
            byte aliveNeighborCount = 0;
            for (int k = i - 1; k <= i + 1; k++)
            {
                for (int l = j - 1; l <= j + 1; l++)
                {
                    if (k >= 0 && k < rows && l >= 0 && l < columns)
                    {
                        if (k != i || l != j)
                        {
                            if (currentGeneration[k, l])
                            {
                                aliveNeighborCount++;
                            }
                        }
                    }
                }
            }
            return aliveNeighborCount;
        }

        private bool GetNextState(bool currentState, byte aliveNeighborCount)
        {
            if (currentState)
            {
                if (aliveNeighborCount < UnderPopulation || aliveNeighborCount > OverPopulation)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (aliveNeighborCount == Reproduction)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
