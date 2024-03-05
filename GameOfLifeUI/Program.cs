using GameOfLifeAPI;

/////CLI version of the Game of Life
/////Early tests, use GameOfLifeForms for the final version

bool[,] currentGeneration = new bool[25, 100];
Random rnd = new Random();
for (int i = 0; i < currentGeneration.GetLength(0); i++)
{
    for (int j = 0; j < currentGeneration.GetLength(1); j++)
    {
        currentGeneration[i, j] = rnd.Next(0, 2) == 1;
    }
}

while (true)
{
    Show(currentGeneration);
    GoLManager gc = new GoLManager();
    currentGeneration = gc.NextGeneration(currentGeneration);
    Console.ReadKey();
}

void Show(bool[,] cells)
{
    Console.Clear();
    for (int i = 0; i < cells.GetLength(0); i++)
    {
        for (int j = 0; j < cells.GetLength(1); j++)
        {
            Console.Write(cells[i, j] ? "O" : " ");
        }
        Console.WriteLine();
    }
}
