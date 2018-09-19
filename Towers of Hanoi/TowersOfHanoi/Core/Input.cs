using System;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Core
{
    public class Input
    {
        private static int discsCount = 0;

        public int DiscsCount
        {
            get
            {
                return this.discsCount;
            }
            private set
            {
                if (discsCount == 0)
                    throw new ArgumentException($"Discs count is not set yet. Invoke the ReadInt() method first");

                if (value > Constants.MAX_PEGS)
                    throw new ArgumentException($"Input number must be between {Constants.MIN_PEGS} and {Constants.MAX_PEGS}. Currernt value is {value}");

                this.discsCount = value;
            }
        }

        public void ReadInt()
        {
            Console.WriteLine($"Input number between {Constants.MIN_PEGS} and {Constants.MAX_PEGS}:");

            int n = int.Parse(Console.ReadLine());

            this.DiscsCount = n;
        }
    }
}
