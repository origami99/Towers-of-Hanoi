using System;

namespace TowersOfHanoi.Contracts
{
    public interface IDisk
    {
        int Size { get; }

        ConsoleColor Color { get; }
    }
}
