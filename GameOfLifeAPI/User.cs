using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeAPI
{
    public record User
    {
        public string Username { get; init; }
        public byte Overpopulation { get; init; }
        public byte Underpopulation { get; init; }
        public byte Reproduction { get; init; }
    }
}
