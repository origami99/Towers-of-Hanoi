using System;
using TowersOfHanoi.Globals;

namespace TowersOfHanoi.Models
{
    public class Position
    {
        private int row;
        private int col;

        public Position(Position position)
        {
            this.Row = position.Row;
            this.Col = position.Col;
        }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row
        {
            get
            {
                return this.row;
            }
            set
            {
                if (value < 0 || value >= Constants.CONSOLE_HEIGHT)
                    throw new ArgumentOutOfRangeException($"The row cannot be {value}. It must be between 0 and {Constants.CONSOLE_HEIGHT}");

                this.row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.col;
            }
            set
            {
                if (value < 0 || value >= Constants.CONSOLE_WIDTH)
                    throw new ArgumentOutOfRangeException($"The col cannot be {value}. It must be between 0 and {Constants.CONSOLE_WIDTH}");

                this.col = value;
            }
        }
    }
}
