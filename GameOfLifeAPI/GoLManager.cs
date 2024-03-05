namespace GameOfLifeAPI
{
    /// <summary>
    /// Game of Life game manager
    /// Manages game rule and handles future iterations
    /// </summary>
    public class GoLManager
    {
        public byte UnderPopulation { get; set; } = 2;
        public byte OverPopulation { get; set; } = 3;
        public byte Reproduction { get; set; } = 3;

        public GoLManager()
        {

        }

        /// <summary>
        /// Create a random 2 dimensional array of booleans
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a glider to the current array to the specified position
        /// Will throw an exception if the glider is out of bounds
        /// </summary>
        /// <param name="currentArray"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the glider is out of bounds</exception>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the next generation of the generation in parameter
        /// </summary>
        /// <param name="currentGeneration"></param>
        /// <returns>Next generation</returns>
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

        /// <summary>
        /// Private method used to get the number of living neighbors of a cell
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="currentGeneration"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Private method used to get the next state of a cell considering its current state and the number of living neighbors
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="aliveNeighborCount"></param>
        /// <returns></returns>
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
