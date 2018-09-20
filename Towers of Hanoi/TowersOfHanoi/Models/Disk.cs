using System;

namespace TowersOfHanoi.Models
{
    public class Disk
    {
        public Disk(int size, ConsoleColor color)
        {
            this.Size = size;
            this.Color = color;
        }

        public int Size { get; set; }

        public ConsoleColor Color { get; set; }
    }
}
