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

        public Game(int width = 12, int height = 12, char[,] field = null, int points = -1, int numberOfDiamonds = -1)
        {
            if (field != null)
            {
                this.field = field;
                this.oldField = (char[,])this.field.Clone();

                this.points = points;
                this.numberOfDiamonds = numberOfDiamonds;

                this.width = field.GetLength(0);
                this.height = field.GetLength(1);

                for (int i = 0; i < this.width; i++)
                {
                    for (int j = 0; j < this.height; j++)
                    {
                        if (this[i, j] == Chars.player)
                        {
                            this.posX = i;
                            this.posY = j;

                            break;
                        }
                    }
                }

                return;
            }

            this.field = new char[this.width, this.height];
            Random rnd = new Random();
            this.numberOfDiamonds = 0;
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    int random = rnd.Next(0, 100);
                    if (random < 80 || (i == this.posX && j == this.posY))
                    {
                        this[i, j] = Chars.sand;
                    }
                    else if (random < 90)
                    {
                        this[i, j] = Chars.stone;
                    }
                    else
                    {
                        this[i, j] = Chars.diamond;
                        this.numberOfDiamonds++;
                    }
                }
            }

            this[this.posX, this.posY] = Chars.player;

            this.oldField = (char[,])this.field.Clone();
        }

        public char this[int x, int y]
        {
            get
            {
                return this.field[x, y];
            }

            set
            {
                this.field[x, y] = value;
            }
        }

    }
}