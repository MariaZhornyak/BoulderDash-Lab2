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

        public string GameString
        {
            get
            {
                string s = "";

                s += $"Points: {this.points}\nNumberOfDiamonds: {this.numberOfDiamonds}\nWidth: {this.width}\nHeight: {this.height}\n";

                for (int i = 0; i < this.width; i++)
                {
                    for (int j = 0; j < this.height; j++)
                    {
                        s += this[j, i];
                    }
                    s += '\n';
                }

                return s;
            }
        }

        public void MovePlayer(int dX, int dY)
        {
            int newPosX = this.posX + dX;
            int newPosY = this.posY + dY;
            int afterNewPosX = this.posX + 2 * dX;
            int afterNewPosY = this.posY + 2 * dY;
            if (newPosX < 0 || newPosX > this.width - 1 || newPosY < 0 || newPosY > this.height - 1)
            {
                return;
            }

            if (this[newPosX, newPosY] == Chars.diamond)
            {
                this.points++;
                if (this.points == this.numberOfDiamonds)
                {
                    this.gameEnded = true;
                }
            }
            else if (this[newPosX, newPosY] == Chars.stone)
            {
                try
                {
                    char afterNextSymbol = this[afterNewPosX, afterNewPosY];
                    if (afterNextSymbol == Chars.stone || afterNextSymbol == Chars.diamond)
                    {
                        return;
                    }
                    else
                    {
                        this[afterNewPosX, afterNewPosY] = Chars.stone;
                    }
                }
                catch
                {
                    return;
                }
            }

            this[newPosX, newPosY] = Chars.player;
            this[this.posX, this.posY] = ' ';
            this.posX = newPosX;
            this.posY = newPosY;
        }

        public void MoveLeft() => MovePlayer(-1, 0);
        public void MoveRight() => MovePlayer(1, 0);
        public void MoveUp() => MovePlayer(0, -1);
        public void MoveDown() => MovePlayer(0, 1);

        public void PrintField()
        {
            Console.Clear();

            Console.WriteLine($"Your points: {this.points}");
            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    Console.ForegroundColor = Chars.blockColors[this[j, i]];
                    Console.Write(this[j, i]);
                    Console.ForegroundColor = Chars.defaultForegroundColor;
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nPress ESC to exit the game");
        }

        public void UpdateField()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Your points: {this.points}");

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (this[j, i] != this.oldField[j, i])
                    {
                        Console.SetCursorPosition(j, i + 1);
                        Console.ForegroundColor = Chars.blockColors[this[j, i]];
                        Console.Write(this[j, i]);
                        Console.ForegroundColor = Chars.defaultForegroundColor;

                        this.oldField[j, i] = this[j, i];
                    }
                }
            }

            Console.SetCursorPosition(0, this.height + 1);
        }

    }
}