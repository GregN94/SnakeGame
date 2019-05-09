using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    class Snake
    {
        public List<Pixel> Body { get; set; }
        public Pixel Head { get; set; }

        public int Length { get; set; }
        

        public Direction Direction { get; set; }

        public Snake(Direction direction)
        {
            Direction = direction;
            Body = new List<Pixel>();
            Head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
            Length = 5;
        }


        public int EatTail()
        {
            int eatenPixels = 0;
            var index = Body.FindIndex(i => i.Collide(Head));
            if (index != -1)
            {
                Body.RemoveRange(0, index + 1);
                eatenPixels = Length - Body.Count;
                Length = Body.Count;
            }

            return eatenPixels;
        }


        public void Eat()
        {
            Length++;
        }

        public void Move()
        {
            Body.Add(new Pixel(Head.XPos, Head.YPos, ConsoleColor.Green));

            switch (Direction)
            {
                case Direction.Up:
                    Head.YPos--;
                    break;
                case Direction.Down:
                    Head.YPos++;
                    break;
                case Direction.Left:
                    Head.XPos--;
                    break;
                case Direction.Right:
                    Head.XPos++;
                    break;
            }

            if (Body.Count > Length)
            {
                Body.RemoveAt(0);
            }
        }
    }
}
