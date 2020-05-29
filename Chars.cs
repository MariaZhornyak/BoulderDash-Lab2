using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BoulderdashLab2
{
    class Chars
    {
        public static readonly char player = 'I';
        public static readonly char sand = '.';
        public static readonly char stone = 'o';
        public static readonly char diamond = 'D';

        public static readonly ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        public static readonly ConsoleColor defaultForegroundColor = ConsoleColor.White;

        public static readonly Dictionary<char, ConsoleColor> blockColors = new Dictionary<char, ConsoleColor>()
        {
            { player, ConsoleColor.Red },
            { sand, ConsoleColor.Yellow },
            { stone, ConsoleColor.DarkGray },
            { diamond, ConsoleColor.Cyan },
            { ' ', ConsoleColor.Black }
        };
    }
}