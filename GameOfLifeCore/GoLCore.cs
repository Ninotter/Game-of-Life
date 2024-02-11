using System;

namespace GameOfLifeCore
{
    public static class GoLCore
    {
        public static bool[,] CreateRandomArray(int rows, int columns)
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

        public static bool[,] NextGeneration(bool[,] currentGeneration)
        { 
            int rows = currentGeneration.GetLength(0);
            int columns = currentGeneration.GetLength(1);
            bool[,] nextGeneration = new bool[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    bool currentState = currentGeneration[i, j];
                    byte aliveNeighborCount = GetAliveNeighborCount(i,j, currentGeneration);
                    nextGeneration[i,j] = GetNextState(currentState, aliveNeighborCount);
                }
            }
            return nextGeneration;
        }

        private static byte GetAliveNeighborCount(int i, int j, bool[,] currentGeneration)
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

        private static bool GetNextState(bool currentState, byte aliveNeighborCount)
        {
            if (currentState)
            {
                if (aliveNeighborCount < 2)
                {
                    return false;
                }
                if (aliveNeighborCount > 3)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (aliveNeighborCount == 3)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
