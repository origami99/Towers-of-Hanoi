using System;
using System.Collections.Generic;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Globals;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Visualization
{
    public class ViewConsole
    {
        private static readonly int pegRow = 3;

        private static readonly int pegHeight = LocalDataBase.DiskCounts + 1;

        private readonly Position leftPegUpPivot = new Position(pegRow, Constants.MAX_PEGS + 1);

        private readonly Position middlePegUpPivot = new Position(pegRow, Constants.MAX_PEGS * 2 + 3);

        private readonly Position rightPegUpPivot = new Position(pegRow, Constants.MAX_PEGS * 4 + 5);

        public void Print()
        {
            DrawPegs();
        }

        private void DrawPegs()
        {
            List<Position> pegPivots = new List<Position>()
            {
                this.leftPegUpPivot,
                this.middlePegUpPivot,
                this.rightPegUpPivot
            };

            foreach (Position pivot in pegPivots)
            {
                for (int row = pivot.Row; row <= pivot.Row + pegHeight; row++)
                {
                    Console.SetCursorPosition(pivot.Col, row);
                    Console.Write(Chars.VERTICAL_STICK);
                }
            }
        }
    }
}
