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

        public void MoveRight()
        {
            if (this.posX != this.width - 1)
            {
                if (this[this.posX + 1, this.posY] == Chars.diamond)
                {
                    // Process sound = new Process();
                    // sound.EnableRaisingEvents = false; 
                    // sound.StartInfo.FileName = "powershell";
                    // sound.StartInfo.Arguments = "-c (New-Object Media.SoundPlayer 'C:/Users/User/Downloads/BoulderDash/Crystall.wav').PlaySync();";
                    // sound.Start();

                    this[this.posX + 1, this.posY] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.points++;
                    if (this.points == this.numberOfDiamonds)
                    {
                        this.gameEnded = true;
                    }
                    this.posX += 1;
                }
                else if (this[this.posX + 1, this.posY] == Chars.stone)
                {
                    if (this.posX != this.width - 2 && this[this.posX + 2, this.posY] != Chars.stone && this[this.posX + 2, this.posY] != Chars.diamond)
                    {
                        this[this.posX + 2, this.posY] = Chars.stone;
                        this[this.posX + 1, this.posY] = Chars.player;
                        this[this.posX, this.posY] = ' ';
                        this.posX += 1;
                    }
                }
                else if (this[this.posX + 1, this.posY] == Chars.sand || this[this.posX + 1, this.posY] == ' ')
                {
                    this[this.posX + 1, this.posY] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.posX += 1;
                }
            }
        }

        public void MoveLeft()
        {
            if (this.posX != 0)
            {
                if (this[this.posX - 1, this.posY] == Chars.diamond)
                {
                    // Process sound = new Process();
                    // sound.EnableRaisingEvents = false; 
                    // sound.StartInfo.FileName = "powershell";
                    // sound.StartInfo.Arguments = "-c (New-Object Media.SoundPlayer 'C:/Users/User/Downloads/BoulderDash/Crystall.wav').PlaySync();";
                    // sound.Start();

                    this[this.posX - 1, this.posY] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.points++;
                    if (this.points == this.numberOfDiamonds)
                    {
                        this.gameEnded = true;
                    }
                    this.posX -= 1;
                }
                else if (this[this.posX - 1, this.posY] == Chars.stone)
                {
                    if (this.posX != 1 && this[this.posX - 2, this.posY] != Chars.stone && this[this.posX - 2, this.posY] != Chars.diamond)
                    {
                        this[this.posX - 2, this.posY] = Chars.stone;
                        this[this.posX - 1, this.posY] = Chars.player;
                        this[this.posX, this.posY] = ' ';
                        this.posX -= 1;
                    }
                }
                else if (this[this.posX - 1, this.posY] == Chars.sand || this[this.posX - 1, this.posY] == ' ')
                {
                    this[this.posX - 1, this.posY] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.posX -= 1;
                }
            }
        }

        public void MoveUp()
        {
            if (this.posY != 0)
            {
                if (this[this.posX, this.posY - 1] == Chars.diamond)
                {
                    // Process sound = new Process();
                    // sound.EnableRaisingEvents = false; 
                    // sound.StartInfo.FileName = "powershell";
                    // sound.StartInfo.Arguments = "-c (New-Object Media.SoundPlayer 'C:/Users/User/Downloads/BoulderDash/Crystall.wav').PlaySync();";
                    // sound.Start();

                    this[this.posX, this.posY - 1] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.points++;
                    if (this.points == this.numberOfDiamonds)
                    {
                        this.gameEnded = true;
                    }
                    this.posY -= 1;
                }
                else if (this[this.posX, this.posY - 1] == Chars.stone)
                {
                    if (this.posY != 1 && this[this.posX, this.posY - 2] != Chars.stone && this[this.posX, this.posY - 2] != Chars.diamond)
                    {
                        this[this.posX, this.posY - 2] = Chars.stone;
                        this[this.posX, this.posY - 1] = Chars.player;
                        this[this.posX, this.posY] = ' ';
                        this.posY -= 1;
                    }
                }
                else if (this[this.posX, this.posY - 1] == Chars.sand || this[this.posX, this.posY - 1] == ' ')
                {
                    this[this.posX, this.posY - 1] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.posY -= 1;
                }
            }
        }

        public void MoveDown()
        {
            if (this.posY != this.height - 1)
            {
                if (this[this.posX, this.posY + 1] == Chars.diamond)
                {
                    // Process sound = new Process();
                    // sound.EnableRaisingEvents = false; 
                    // sound.StartInfo.FileName = "powershell";
                    // sound.StartInfo.Arguments = "-c (New-Object Media.SoundPlayer 'C:/Users/User/Downloads/BoulderDash/Crystall.wav').PlaySync();";
                    // sound.Start();

                    this[this.posX, this.posY + 1] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.points++;
                    if (this.points == this.numberOfDiamonds)
                    {
                        this.gameEnded = true;
                    }
                    this.posY += 1;
                }
                else if (this[this.posX, this.posY + 1] == Chars.stone)
                {
                    if (this.posY != this.height - 2 && this[this.posX, this.posY + 2] != Chars.stone && this[this.posX, this.posY + 2] != Chars.diamond)
                    {
                        this[this.posX, this.posY + 2] = Chars.stone;
                        this[this.posX, this.posY + 1] = Chars.player;
                        this[this.posX, this.posY] = ' ';
                        this.posY += 1;
                    }
                }
                else if (this[this.posX, this.posY + 1] == Chars.sand || this[this.posX, this.posY + 1] == ' ')
                {
                    this[this.posX, this.posY + 1] = Chars.player;
                    this[this.posX, this.posY] = ' ';
                    this.posY += 1;
                }
            }
        }

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