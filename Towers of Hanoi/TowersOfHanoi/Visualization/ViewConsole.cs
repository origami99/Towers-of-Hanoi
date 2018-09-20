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

        private static List<Peg> Pegs = new List<Peg>()
        {
            new Peg(new Position(pegRow, Constants.MAX_PEGS + 1), Enumerable.Range(1, LocalDataBase.DiskCounts).Reverse().ToList(), PegType.Left),
            new Peg(new Position(pegRow, Constants.MAX_PEGS * 3 + 3), new List<int>(), PegType.Middle),
            new Peg(new Position(pegRow, Constants.MAX_PEGS * 5 + 5), new List<int>(), PegType.Right)
        };

        private static int StepCount = 0;

        public static void Print()
        {
            DrawPegs();
            DrawDisks();

            foreach (Step step in LocalDataBase.Steps)
            {
                PerformStep(step);
                DrawStep(step);
            }
        }

        private static void DrawPegs()
        {
            foreach (Position pivot in Pegs.Select(x => x.TopPivot))
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
            for (int i = 0; i < Pegs.Count; i++)
            {
                int counter = 0;
                int startRow = pegRow + pegHeight - 1;
                int endRow = pegRow + pegHeight - LocalDataBase.DiskCounts;

                for (int row = startRow; row >= endRow; row--)
                {
                    if (counter >= Pegs[i].Disks.Count)
                    {
                        Console.SetCursorPosition(Pegs[i].TopPivot.Col - LocalDataBase.DiskCounts, row);
                        Console.Write("{0}{1}{0}", new string(' ', LocalDataBase.DiskCounts), Chars.VERTICAL_STICK);
                        continue;
                    }

                    int currDisc = Pegs[i].Disks[counter];
                    Console.SetCursorPosition(Pegs[i].TopPivot.Col - currDisc, row);
                    Console.Write(new string(Chars.DOWN_BLOCK, currDisc * 2 + 1));

                    counter++;
                }
            }

            Thread.Sleep(Constants.SLEEP_TIME);
        }

        private static void DrawStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            EraseDiskInSource(sourcePeg);
            AddDiskInTarget(targetPeg);

            //Console.ReadKey(true);
            Thread.Sleep(Constants.SLEEP_TIME);
        }

        private static void AddDiskInTarget(Peg targetPeg)
        {
            int currDisk = targetPeg.Disks.Last();

            int cursorCol = targetPeg.TopPivot.Col - currDisk;
            int cursorRow = pegRow + pegHeight - targetPeg.Disks.Count;

            Console.SetCursorPosition(cursorCol, cursorRow);

            Console.Write(new string(Chars.DOWN_BLOCK, currDisk * 2 + 1));
        }

        private static void EraseDiskInSource(Peg sourcePeg)
        {
            int cursorCol = sourcePeg.TopPivot.Col - LocalDataBase.DiskCounts;
            int cursorRow = pegRow + pegHeight - sourcePeg.Disks.Count - 1;

            Console.SetCursorPosition(cursorCol, cursorRow);

            Console.Write("{0}{1}{0}", new string(' ', LocalDataBase.DiskCounts), Chars.VERTICAL_STICK);
        }

        private static void PerformStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            int actionDisk = sourcePeg.PopDisk();
            targetPeg.AddDisk(actionDisk);

            StepCount++;
        }
    }
}
