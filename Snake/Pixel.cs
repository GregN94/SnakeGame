using System;

namespace Snake
{
    class Pixel
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

        public bool Collide(Pixel pixel)
        {
            if (pixel == null)
                return false;

            return pixel.XPos == XPos && pixel.YPos == YPos;
        }
    }
}
