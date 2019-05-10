using System;
using System.Diagnostics;
using System.Linq;

namespace Snake
{
    class Program
    {
        static int score;
        static bool gameOver;

        static void Main()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            Pixel berry = new Pixel(Console.WindowWidth / 3, Console.WindowHeight / 3, ConsoleColor.Cyan);
            Pixel snake = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);

            DrawBorders();

            while (true)
            {
                ClearConsole(Console.WindowWidth, Console.WindowHeight);

                gameOver |= (snake.XPos == Console.WindowWidth - 1 || snake.XPos == 0 || snake.YPos == Console.WindowHeight - 1 || snake.YPos == 0);

                berry.Draw();
                snake.Draw();

                if (gameOver)
                {
                    break;
                }

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 200)
                {
                    ReadKeyboard();
                }
            }
        }

        static void ReadKeyboard()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                }

            }
        }

        private static void ClearConsole(int screenWidth, int screenHeight)
        {
            var blackLine = string.Join("", new byte[screenWidth - 2].Select(b => "-").ToArray());
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i < screenHeight - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
        }

        static void DrawBorders()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");

                Console.SetCursorPosition(i, Console.WindowHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");

                Console.SetCursorPosition(Console.WindowWidth - 1, i);
                Console.Write("■");
            }
        }

    }
}