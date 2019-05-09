using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            var random = new Random();

            var score = 5;

            var head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
            var berry = new Pixel(random.Next(1, Console.WindowWidth - 2), random.Next(1, Console.WindowHeight - 2), ConsoleColor.Cyan);

            var body = new List<Pixel>();

            var currentMovement = Direction.Right;

            var gameOver = false;

            DrawBorder();

            while (true)
            {
                ClearConsole(Console.WindowWidth, Console.WindowHeight);

                gameOver |= (head.XPos == Console.WindowWidth - 1 || head.XPos == 0 || head.YPos == Console.WindowHeight - 1 || head.YPos == 0);


                if (berry.XPos == head.XPos && berry.YPos == head.YPos)
                {
                    score++;
                    berry = new Pixel(random.Next(1, Console.WindowWidth - 2), random.Next(1, Console.WindowHeight - 2), ConsoleColor.Cyan);
                }

                for (int i = 0; i < body.Count; i++)
                {
                    body[i].Draw();
                    gameOver |= (body[i].XPos == head.XPos && body[i].YPos == head.YPos);
                }

                if (gameOver)
                {
                    break;
                }

                head.Draw();
                berry.Draw();

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 200)
                {
                    currentMovement = ReadMovement(currentMovement);
                }

                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                switch (currentMovement)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                }

                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }
            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
            Console.WriteLine($"Game over, Score: {score - 5}");
            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
            Console.ReadKey();
        }

        static Direction ReadMovement(Direction movement)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && movement != Direction.Down)
                {
                    movement = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && movement != Direction.Up)
                {
                    movement = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && movement != Direction.Right)
                {
                    movement = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && movement != Direction.Left)
                {
                    movement = Direction.Right;
                }
            }

            return movement;
        }

        private static void ClearConsole(int screenWidth, int screenHeight)
        {
            var blackLine = string.Join("", new byte[screenWidth - 2].Select(b => " ").ToArray());
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < screenHeight - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
        }

        static void DrawBorder()
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

        struct Pixel
        {
            public Pixel(int xPos, int yPos, ConsoleColor color)
            {
                XPos = xPos;
                YPos = yPos;
                this.color = color;
            }
            public int XPos { get; set; }
            public int YPos { get; set; }

            private readonly ConsoleColor color;

            public void Draw()
            {
                Console.SetCursorPosition(XPos, YPos);
                Console.ForegroundColor = color;
                Console.Write("■");
                Console.SetCursorPosition(0, 0);
            }
        }

        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }
    }
}