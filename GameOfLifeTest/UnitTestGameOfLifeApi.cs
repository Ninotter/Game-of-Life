using GameOfLifeAPI;
namespace GameOfLifeTest
{
    [TestClass]
    public class UnitTestGameOfLifeApi
    {
        /// <summary>
        /// Tests the Game of Life with a 2x2 array
        /// The generation should never change with a 2x2 array of all alive cells
        /// </summary>
        [TestMethod]
        public void Test2by2Array()
        {
            GoLManager goLManager = new GoLManager();
            goLManager.UnderPopulation = 2;
            goLManager.OverPopulation = 3;
            goLManager.Reproduction = 3;
            bool[,] startArray = new bool[2, 2];
            startArray[0, 0] = true;
            startArray[0, 1] = true;
            startArray[1, 0] = true;
            startArray[1, 1] = true;
            bool[,] nextGeneration = goLManager.NextGeneration(startArray);
            for (int i = 0; i < startArray.GetLength(0); i++)
            {
                for (int j = 0; j < startArray.GetLength(1); j++)
                {
                    Assert.AreEqual(startArray[i, j], nextGeneration[i, j]);
                }
            }
        }
    }
}