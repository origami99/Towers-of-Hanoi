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

        private static int stepCount = 0;

        private static int actionDisk;

        private static readonly int pegHeight = LocalDataBase.DiskCounts + 1;

        private static List<Peg> Pegs = new List<Peg>()
        {
            new Peg(new Position(pegRow, Constants.MAX_PEGS + 1), Enumerable.Range(1, LocalDataBase.DiskCounts).Reverse().ToList(), PegType.Left),
            new Peg(new Position(pegRow, Constants.MAX_PEGS * 3 + 3), new List<int>(), PegType.Middle),
            new Peg(new Position(pegRow, Constants.MAX_PEGS * 5 + 5), new List<int>(), PegType.Right)
        };

        public static void Print()
        {
            DrawPegs();
            DrawDisks();
            PrintPoints();

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
            // TODO: Refactorate this method

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
                    Console.Write(new string(Chars.FULFILLED_BLOCK, currDisc * 2 + 1));

                    counter++;
                }
            }

            Thread.Sleep(Constants.STEP_SLEEP_TIME);
        }

        private static void DrawStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            EraseDiskInSource(sourcePeg);
            AddDiskInTarget(targetPeg);
            PrintPoints();

            //Console.ReadKey(true);
            Thread.Sleep(Constants.STEP_SLEEP_TIME);
        }

        private static void EraseDiskInSource(Peg sourcePeg)
        {
            int cursorCol = sourcePeg.TopPivot.Col - actionDisk;
            int cursorRow = pegRow + pegHeight - sourcePeg.Disks.Count - 1;

            char[] fadeBlocks = { Chars.FULFILLED_BLOCK, Chars.FADE_BLOCK_1, Chars.FADE_BLOCK_2, Chars.FADE_BLOCK_3, Chars.EMPTY_BLOCK };

            // TODO: make the fade effect decreasing the speed
            foreach (char fadeBlock in fadeBlocks)
            {
                Thread.Sleep(Constants.FADE_SLEEP_TIME);

                Console.SetCursorPosition(cursorCol, cursorRow);

                Console.Write(new string(fadeBlock, actionDisk * 2 + 1));
            }

            Console.SetCursorPosition(cursorCol + actionDisk, cursorRow);
            Console.Write(Chars.VERTICAL_STICK);
        }

        private static void AddDiskInTarget(Peg targetPeg)
        {
            int currDisk = targetPeg.Disks.Last();

            int cursorCol = targetPeg.TopPivot.Col - currDisk;
            int cursorRow = pegRow + pegHeight - targetPeg.Disks.Count;

            char[] fadeBlocks = { Chars.EMPTY_BLOCK, Chars.FADE_BLOCK_3, Chars.FADE_BLOCK_2, Chars.FADE_BLOCK_1, Chars.FULFILLED_BLOCK };

            foreach (char fadeBlock in fadeBlocks)
            {
                Thread.Sleep(Constants.FADE_SLEEP_TIME);

                Console.SetCursorPosition(cursorCol, cursorRow);

                Console.Write(new string(fadeBlock, actionDisk * 2 + 1));
            }
        }

        private static void PerformStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            actionDisk = sourcePeg.PopDisk();
            targetPeg.AddDisk(actionDisk);

            stepCount++;
        }

        private static void PrintPoints()
        {
            string text = $"   Steps left: {LocalDataBase.Steps.Count - stepCount}";

            int cursorRow = Constants.CONSOLE_HEIGHT - 2;
            int cursorCol = Constants.CONSOLE_WIDTH - text.Length - 1;
            Console.SetCursorPosition(cursorCol, cursorRow);

            Console.Write(text);
        }
    }
}
