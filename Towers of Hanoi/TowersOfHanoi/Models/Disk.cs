using System;
using TowersOfHanoi.Contracts;

namespace TowersOfHanoi.Models
{
    public class Disk : IDisk
    {
        public Disk(int size, ConsoleColor color)
        {
            this.Size = size;
            this.Color = color;
        }

        public int Size { get; }

        public ConsoleColor Color { get; }
    }
}
