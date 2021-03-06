﻿using System;
using System.Collections.Generic;
using System.Linq;
using TowersOfHanoi.Common;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Core
{
    public class SolverIterative : ISolver
    {
        private int previusElement = -1;
        private List<Stack<int>> pegs = new List<Stack<int>>();
        private Step step = new Step();

        private int numbOfElements;
        private readonly IDataBase dataBase;

        public SolverIterative(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public void Execute()
        {
            this.numbOfElements = dataBase.DiskCounts;
            pegs.Add(new Stack<int>(Enumerable.Range(1, this.numbOfElements).Reverse()));
            pegs.Add(new Stack<int>());
            pegs.Add(new Stack<int>());
            Solve(numbOfElements % 2 == 0 ? 1 : -1 );
        }

        private void Solve(int direction)
        {
            if (pegs[2].Count == numbOfElements)
            {
                return;
            }

            int fromIndex = FindBiggest();
            int newIndex = CanMoveOnPosition(direction, fromIndex);

            if (newIndex < 0)
            {
                fromIndex = FindSmallest();
                newIndex = CanMoveOnPosition(direction, fromIndex);
            }

            this.step.Source = (PegType)fromIndex;
            this.step.Target = (PegType)newIndex;
            this.dataBase.Steps.Add(this.step);

            previusElement = newIndex;

            pegs[newIndex].Push(pegs[fromIndex].Pop());

            Solve(direction);

        }

        private int CanMoveOnPosition(int direction, int fromPosition)
        {
            int newPosition = -1;
            int length = pegs.Count;

            for (int i = 0; i < length; i++)
            {
                int nextIndex = Math.Abs(fromPosition + length + direction) % length;
                if (pegs[nextIndex].Count == 0)
                {
                    return nextIndex;
                }
                else if (pegs[fromPosition].Peek() < pegs[nextIndex].Peek())
                {
                    return nextIndex;
                }

                direction += direction;
            }

            return newPosition;
        }

        private int FindSmallest()
        {
            int left = int.MaxValue,
                mid = int.MaxValue,
                right = int.MaxValue;

            if (previusElement != 0 && pegs[0].Count > 0)
            {
                left = pegs[0].Peek();
            }
            if (previusElement != 1 && pegs[1].Count > 0)
            {
                mid = pegs[1].Peek();
            }
            if (previusElement != 2 && pegs[2].Count > 0)
            {
                right = pegs[2].Peek();
            }

            int reference = Math.Min(Math.Min(left, mid), right);

            if (reference == left)
            {
                return 0;
            }
            if (reference == mid)
            {
                return 1;
            }

            return 2;
        }

        private int FindBiggest()
        {
            int left = int.MinValue, mid = int.MinValue, right = int.MinValue;

            if (previusElement != 0 && pegs[0].Count > 0)
            {
                left = pegs[0].Peek();
            }
            if (previusElement != 1 && pegs[1].Count > 0)
            {
                mid = pegs[1].Peek();
            }
            if (previusElement != 2 && pegs[2].Count > 0)
            {
                right = pegs[2].Peek();
            }

            int reference = Math.Max(Math.Max(left, mid), right);

            if (reference == left)
            {
                return 0;
            }
            if (reference == mid)
            {
                return 1;
            }

            return 2;
        }
    }
}