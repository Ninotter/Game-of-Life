using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameOfLifeForms
{
    internal class GameOfLifeParser
    {
        public static bool ContainsRule(string str)
        {
            Regex rgx = new Regex(@"^Rule\[\d,\d,\d]$");
            return rgx.IsMatch(str);
        }

        public static (byte, byte, byte) ParseRule(string str)
        {
            Regex rgx = new Regex(@"\d");
            MatchCollection matches = rgx.Matches(str);
            byte[] rule = new byte[3];
            for (int i = 0; i < 3; i++)
            {
                rule[i] = byte.Parse(matches[i].Value);
            }
            return (rule[0], rule[1], rule[2]);
        }
    }
}
