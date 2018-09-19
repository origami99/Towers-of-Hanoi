using System;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Core
{
    public static class Input
    {
        private static int discsCount = 0;

        public static int DiscsCount
        {
            get
            {
                return discsCount;
            }
            private set
            {
                if (discsCount == 0)
                    throw new ArgumentException($"Discs count is not set yet. Invoke the ReadInt() method first");

                if (value > Constants.MAX_PEGS)
                    throw new ArgumentException($"Input number must be between {Constants.MIN_PEGS} and {Constants.MAX_PEGS}. Currernt value is {value}");

                discsCount = value;
            }
        }

        public static void ReadInt()
        {
            Console.WriteLine($"Input number between {Constants.MIN_PEGS} and {Constants.MAX_PEGS}:");

            int n = int.Parse(Console.ReadLine());

            DiscsCount = n;
        }
    }
}
