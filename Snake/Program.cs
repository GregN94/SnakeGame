using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;

namespace Snake
{
    class Program
    {
        static int score;
        static bool gameOver;

        static void HandleSnakeBerryCollision(Snake snake, Berry berry)
        {
            if (snake.Head.Collide(berry.Body))
            {
                snake.Eat();
                score++;
                berry.NewBerry();
            }
        }

        static void Main()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            Berry berry = new Berry();

            Snake snake = new Snake(Direction.Right);

            DrawBorders();

            while (true)
            {
                score -= snake.EatTail();

                ClearConsole(Console.WindowWidth, Console.WindowHeight);

                gameOver |= (snake.Head.XPos == Console.WindowWidth - 1 || snake.Head.XPos == 0 || snake.Head.YPos == Console.WindowHeight - 1 || snake.Head.YPos == 0);

                HandleSnakeBerryCollision(snake, berry);

                berry.Body.Draw();

                DrawSnake(snake);

                if (gameOver)
                {
                    break;
                }

                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= 200)
                {
                    var direction = ReadKeyboard(snake.Direction);
                    snake.Direction = direction;
                }

                snake.Move();
            }
            GameOver();
        }

        static void GameOver()
        {
            bool closeWindow = false;

            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
            Console.WriteLine($"Game over, Score: {score}");
            Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
            while (!closeWindow)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Enter)
                {
                    closeWindow = true;
                }
            }
        }

        static Direction ReadKeyboard(Direction snakeDirection)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && snakeDirection != Direction.Down)
                {
                    snakeDirection = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && snakeDirection != Direction.Up)
                {
                    snakeDirection = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && snakeDirection != Direction.Right)
                {
                    snakeDirection = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && snakeDirection != Direction.Left)
                {
                    snakeDirection = Direction.Right;
                }
            }

            return snakeDirection;
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

        static void DrawSnake(Snake snake)
        {
            for (int i = 0; i < snake.Body.Count; i++)
            {
                snake.Body[i].Draw();
            }

            snake.Head.Draw();
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