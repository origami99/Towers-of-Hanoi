using System;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Core
{
    public class InputFromConsole : IInputData
    {
        private int discsCount = 0;

        public int DiscsCount
        {
            get
            {
                if (discsCount == 0)
                    throw new ArgumentException($"Discs count is not set yet. Invoke the ReadInt() method first");

                return discsCount;
            }
            private set
            {
                if (value > Constants.MAX_PEGS)
                    throw new ArgumentException($"Input number must be between {Constants.MIN_PEGS} and {Constants.MAX_PEGS}. Currernt value is {value}");

                discsCount = value;
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
