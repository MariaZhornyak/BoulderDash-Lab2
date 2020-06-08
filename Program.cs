using System;
using System.IO;
using System.Diagnostics;

namespace BoulderdashLab2
{
    class Program
    {

        static bool ShowGameMenu(Game game)   // returns true if user wants to exit, otherwise false
        {
            Console.BackgroundColor = Chars.defaultBackgroundColor;
            Console.ForegroundColor = Chars.defaultForegroundColor;

            Console.Clear();
            Console.WriteLine("1: Return back to the game");
            Console.WriteLine("2: Exit and save the game");
            Console.WriteLine("3: Just exit");

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        return false;
                    case ConsoleKey.D2:
                        while (true)
                        {
                            Console.CursorVisible = true;

                            Console.Clear();
                            Console.Write("Please, enter the name of the game to save it: ");
                            string GameFileName = $"SavedGames/{Console.ReadLine()}.txt";

                            Console.CursorVisible = false;

                            if (!File.Exists(GameFileName))
                            {
                                using (StreamWriter file = new StreamWriter(GameFileName))
                                {
                                    file.Write(game.GameString);
                                }

                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("There already exists saved game with this name. So what do you want to do?\n");
                                Console.WriteLine("1: Save game with this name anyway");
                                Console.WriteLine("2: Fuck, go back and let me change the game name");

                                switch (Console.ReadKey(true).Key)
                                {
                                    case ConsoleKey.D1:
                                        using (StreamWriter file = new StreamWriter(GameFileName))
                                        {
                                            file.Write(game.GameString);
                                        }

                                        return true;
                                    case ConsoleKey.D2:
                                        continue;
                                }
                            }
                        }

                        return true;
                    case ConsoleKey.D3:
                        return true;
                }
            }
        }


        static void PlayNewGame(Game game = null)
        {
            Console.Clear();

            if (game == null)
            {
                game = new Game();
            }

            game.PrintField();
            while (true)
            {
                if (game.gameEnded)
                {
                    Console.WriteLine("\nCongratulations, you won! Press any key to continue.");
                    Console.ReadKey(true);
                    break;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.RightArrow:
                        game.MoveRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        game.MoveLeft();
                        break;
                    case ConsoleKey.UpArrow:
                        game.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        game.MoveDown();
                        break;
                    case ConsoleKey.Escape:
                        switch (ShowGameMenu(game))
                        {
                            case true:
                                return;
                            case false:
                                game.PrintField();
                                break;
                        }
                        break;
                }

                game.UpdateField();
            }

            Console.Clear();
        }


        static void LoadSavedGame()
        {
            while (true)
            {
                Console.CursorVisible = true;

                Console.Clear();
                Console.Write("Please enter the name of the game to load: ");
                string GameFileName = $"SavedGames/{Console.ReadLine()}.txt";

                Console.CursorVisible = false;

                if (File.Exists(GameFileName))
                {
                    int points;
                    int numberOfDiamonds;
                    int width;
                    int height;
                    char[,] field;
                    using (StreamReader file = new StreamReader(GameFileName))
                    {
                        points = Convert.ToInt32(file.ReadLine().Split(' ')[1]);
                        numberOfDiamonds = Convert.ToInt32(file.ReadLine().Split(' ')[1]);
                        width = Convert.ToInt32(file.ReadLine().Split(' ')[1]);
                        height = Convert.ToInt32(file.ReadLine().Split(' ')[1]);

                        field = new char[width, height];
                        for (int i = 0; i < height; i++)
                        {
                            string row = file.ReadLine();
                            for (int j = 0; j < width; j++)
                            {
                                field[j, i] = row[j];
                            }
                        }
                    }

                    Game game = new Game(width, height, field, points, numberOfDiamonds);

                    PlayNewGame(game);

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("There is no saved game with this name. So what do you want to do?\n");
                    Console.WriteLine("1: Enter another name");
                    Console.WriteLine("2: Back to main menu");

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            continue;
                        case ConsoleKey.D2:
                            return;
                    }
                }
            }
        }


        static void OpenSettings()
        {
            Console.Clear();

            Console.CursorVisible = true;

            while (true)
            {
                try
                {
                    Console.Write("Width: ");
                    Game.DefaultWidth = Convert.ToInt32(Console.ReadLine());

                    break;
                }
                catch{}
            }

            while (true)
            {
                try
                {
                    Console.Write("Height: ");
                    Game.DefaultHeight = Convert.ToInt32(Console.ReadLine());

                    break;
                }
                catch{}
            }

            Console.CursorVisible = false;
        }
        static void Main(string[] args)
        {
            Start:

            Console.BackgroundColor = Chars.defaultBackgroundColor;
            Console.ForegroundColor = Chars.defaultForegroundColor;

            Console.Clear();

            Console.WriteLine("1: Start a new game");
            Console.WriteLine("2: Resume a saved game");
            Console.WriteLine("3: Game settings");
            Console.WriteLine("4: Exit");

            Console.CursorVisible = false;

            ReadKeyLabel:

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    PlayNewGame();
                    break;
                case ConsoleKey.D2:
                    LoadSavedGame();
                    break;
                case ConsoleKey.D3:
                    OpenSettings();
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    goto ReadKeyLabel;
            }

            goto Start;
        }
    }
}
