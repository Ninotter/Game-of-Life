using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeForms
{
    /// <summary>
    /// Interface for any object that can host a Game of Life
    /// </summary>
    public interface IGameOfLifeHost
    {
        byte Underpopulation { get; set; }
        byte Overpopulation { get; set; }
        byte Reproduction { get; set; }

        void StartGame();
    }
}
