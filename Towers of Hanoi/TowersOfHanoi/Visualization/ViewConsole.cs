using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TowersOfHanoi.Common;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Globals;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Visualization
{
    public static class ViewConsole
    {
        private static readonly int pegRow = 3;

        private static readonly int pegHeight = LocalDataBase.DiskCounts + 1;

        private static List<Position> PegUpPivots = new List<Position>()
        {
            new Position(pegRow, Constants.MAX_PEGS + 1),
            new Position(pegRow, Constants.MAX_PEGS * 3 + 3),
            new Position(pegRow, Constants.MAX_PEGS * 5 + 5)
        };

        private static List<List<int>> PegDisks = new List<List<int>>()
        {
            Enumerable.Range(1, LocalDataBase.DiskCounts).Reverse().ToList(),
            new List<int>(),
            new List<int>()
        };

        public static void Print()
        {
            DrawPegs();
            DrawDisks();
        }

        private static void DrawPegs()
        {
            foreach (Position pivot in PegUpPivots)
            {
                for (int row = pivot.Row; row < pivot.Row + pegHeight; row++)
                {
                    Console.SetCursorPosition(pivot.Col, row);
                    Console.Write(Chars.VERTICAL_STICK);
                }
            }

            Console.WriteLine();
        }

        private static void DrawDisks()
        {
            foreach (Step step in LocalDataBase.Steps)
            {
                int element = PegDisks[(int)step.Source].Last();
                PegDisks[(int)step.Source].RemoveAt(PegDisks[(int)step.Source].Count - 1);

                PegDisks[(int)step.Target].Add(element);

                for (int i = 0; i < PegDisks.Count; i++)
                {
                    int counter = 0;
                    int startRow = pegRow + pegHeight - 1;
                    int endRow = pegRow + pegHeight - LocalDataBase.DiskCounts;

                    for (int row = startRow; row >= endRow; row--)
                    {
                        if (counter >= PegDisks[i].Count)
                        {
                            Console.SetCursorPosition(PegUpPivots[i].Col - LocalDataBase.DiskCounts, row);
                            Console.Write("{0}{1}{0}", new string(' ', LocalDataBase.DiskCounts), Chars.VERTICAL_STICK);
                            continue;
                        }

                        int currDisc = PegDisks[i][counter];
                        Console.SetCursorPosition(PegUpPivots[i].Col - currDisc, row);
                        Console.Write(new string(Chars.DOWN_BLOCK, currDisc * 2 + 1));

                        counter++;

                    }

                }
                // set cursor in bottom left corner.
                Console.SetCursorPosition(0, Console.WindowHeight - 1);

                //Console.ReadKey(true);
                Thread.Sleep(Constants.SLEEP_TIME);
            }

            // set cursor in bottom left corner
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }
    }
}
