using System;

namespace BoulderdashLab2
{
    class Game
    {
        public char[,] field;
        public char[,] oldField;
        public int points = 0;
        public int numberOfDiamonds;
        public bool gameEnded = false;
        public int posX = 0;
        public int posY = 0;
        public int width = Game.DefaultWidth;
        public int height = Game.DefaultHeight;
        public static int DefaultWidth = 12;
        public static int DefaultHeight = 12;

    }
}