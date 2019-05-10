using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Berry
    {
        public Pixel Body { get; set; }

        private Random random = new Random();

        public Berry()
        {
            Body = new Pixel(random.Next(1, Console.WindowWidth - 2), random.Next(1, Console.WindowHeight - 2), ConsoleColor.Cyan);
        }

        public void NewBerry()
        {
            Body.XPos = random.Next(1, Console.WindowWidth - 2);
            Body.YPos = random.Next(1, Console.WindowHeight - 2);
        }
    }
}
