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
    public class ViewConsole : IVisualization
    {
        private readonly int pegRow = 3;
        private int stepCount = 0;
        private Disk actionDisk;
        private int pegHeight;
        private readonly IDataBase dataBase;
        private List<Peg> Pegs;

        public ViewConsole(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public void Print()
        {
            this.pegHeight = this.dataBase.DiskCounts + 1;

            SetupPegs();

            DrawPegs();
            DrawDisks();
            PrintPoints();

            foreach (Step step in this.dataBase.Steps)
            {
                PerformStep(step);
                DrawStep(step);
            }
        }

        private void SetupPegs()
        {
            Position leftPegPos = new Position(pegRow, Constants.MAX_PEGS + 1);

            List<int> diskSizes = Enumerable.Range(1, this.dataBase.DiskCounts).Reverse().ToList();
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

            List<Disk> leftPegDisks = new Disk[this.dataBase.DiskCounts]
                .Select((x, y) => new Disk(diskSizes[y], diskColors[this.dataBase.DiskCounts - y - 1]))
                .ToList();

            Peg leftPeg = new Peg(leftPegPos, leftPegDisks, PegType.Left);
            Peg middlePeg = new Peg(new Position(pegRow, Constants.MAX_PEGS * 3 + 3), new List<Disk>(), PegType.Middle);
            Peg rightPeg = new Peg(new Position(pegRow, Constants.MAX_PEGS * 5 + 5), new List<Disk>(), PegType.Right);

            Pegs = new List<Peg>()
            {
                leftPeg, middlePeg, rightPeg
            };
        }

        private void DrawPegs()
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

        private void DrawDisks()
        {
            // TODO: Refactorate this method

            int counter = 0;
            int startRow = pegRow + pegHeight - 1;
            int endRow = pegRow + pegHeight - this.dataBase.DiskCounts;

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

        private void DrawStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            EraseDiskInSource(sourcePeg);
            AddDiskInTarget(targetPeg);
            PrintPoints();

            //Console.ReadKey(true);
            Thread.Sleep(Constants.STEP_SLEEP_TIME);
        }

        private void EraseDiskInSource(Peg sourcePeg)
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

        private void AddDiskInTarget(Peg targetPeg)
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

        private void PerformStep(Step step)
        {
            Peg sourcePeg = Pegs.Single(p => p.Type == step.Source); // ref
            Peg targetPeg = Pegs.Single(p => p.Type == step.Target); // ref

            actionDisk = sourcePeg.PopDisk();
            targetPeg.AddDisk(actionDisk);

            stepCount++;
        }

        private void PrintPoints()
        {
            string text = $"   Steps left: {this.dataBase.Steps.Count - stepCount}";

            int cursorRow = Constants.CONSOLE_HEIGHT - 2;
            int cursorCol = Constants.CONSOLE_WIDTH - text.Length - 1;

            Console.SetCursorPosition(cursorCol, cursorRow);
            Console.ResetColor();
            Console.Write(text);
        }
    }
}
