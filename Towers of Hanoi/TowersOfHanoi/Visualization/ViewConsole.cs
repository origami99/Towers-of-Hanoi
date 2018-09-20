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

        private static Disk actionDisk;

        private static readonly int pegHeight = LocalDataBase.DiskCounts + 1;

        private static List<Peg> Pegs;

        public static void Print()
        {
            SetupPegs();

            DrawPegs();
            DrawDisks();
            PrintPoints();

            foreach (Step step in LocalDataBase.Steps)
            {
                PerformStep(step);
                DrawStep(step);
            }
        }

        private static void SetupPegs()
        {
            Position leftPegPos = new Position(pegRow, Constants.MAX_PEGS + 1);

            List<int> diskSizes = Enumerable.Range(1, LocalDataBase.DiskCounts).Reverse().ToList();
            List<ConsoleColor> diskColors = new List<ConsoleColor>()
            {
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Red,
                ConsoleColor.Cyan,
                ConsoleColor.Yellow,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkRed,
                ConsoleColor.DarkCyan,
                ConsoleColor.DarkYellow
            };

            List<Disk> leftPegDisks = new Disk[LocalDataBase.DiskCounts]
                .Select((x, y) => new Disk(diskSizes[y], diskColors[LocalDataBase.DiskCounts - y - 1]))
                .ToList();

            Peg leftPeg = new Peg(leftPegPos, leftPegDisks, PegType.Left);
            Peg middlePeg = new Peg(new Position(pegRow, Constants.MAX_PEGS * 3 + 3), new List<Disk>(), PegType.Middle);
            Peg rightPeg = new Peg(new Position(pegRow, Constants.MAX_PEGS * 5 + 5), new List<Disk>(), PegType.Right);

            Pegs = new List<Peg>()
            {
                leftPeg, middlePeg, rightPeg
            };
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

            int counter = 0;
            int startRow = pegRow + pegHeight - 1;
            int endRow = pegRow + pegHeight - LocalDataBase.DiskCounts;

            for (int row = startRow; row >= endRow; row--)
            {
                Disk currDisc = Pegs.First().Disks[counter];
                Console.ForegroundColor = currDisc.Color;
                Console.SetCursorPosition(Pegs.First().TopPivot.Col - currDisc.Size, row);
                Console.Write(new string(Chars.FULFILLED_BLOCK, currDisc.Size * 2 + 1));

                counter++;
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
            int cursorCol = sourcePeg.TopPivot.Col - actionDisk.Size;
            int cursorRow = pegRow + pegHeight - sourcePeg.Disks.Count - 1;

            char[] fadeBlocks = { Chars.FULFILLED_BLOCK, Chars.FADE_BLOCK_1, Chars.FADE_BLOCK_2, Chars.FADE_BLOCK_3, Chars.EMPTY_BLOCK };

            Console.ForegroundColor = actionDisk.Color;

            // TODO: make the fade effect decreasing the speed
            foreach (char fadeBlock in fadeBlocks)
            {
                Thread.Sleep(Constants.FADE_SLEEP_TIME);

                Console.SetCursorPosition(cursorCol, cursorRow);

                Console.Write(new string(fadeBlock, actionDisk.Size * 2 + 1));
            }

            Console.ResetColor();
            Console.SetCursorPosition(cursorCol + actionDisk.Size, cursorRow);
            Console.Write(Chars.VERTICAL_STICK);
        }

        private static void AddDiskInTarget(Peg targetPeg)
        {
            Disk currDisk = targetPeg.Disks.Last();

            int cursorCol = targetPeg.TopPivot.Col - currDisk.Size;
            int cursorRow = pegRow + pegHeight - targetPeg.Disks.Count;

            char[] fadeBlocks = { Chars.EMPTY_BLOCK, Chars.FADE_BLOCK_3, Chars.FADE_BLOCK_2, Chars.FADE_BLOCK_1, Chars.FULFILLED_BLOCK };

            Console.ForegroundColor = currDisk.Color;

            foreach (char fadeBlock in fadeBlocks)
            {
                Thread.Sleep(Constants.FADE_SLEEP_TIME);

                Console.SetCursorPosition(cursorCol, cursorRow);

                Console.Write(new string(fadeBlock, actionDisk.Size * 2 + 1));
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
            Console.ResetColor();
            Console.Write(text);
        }
    }
}
