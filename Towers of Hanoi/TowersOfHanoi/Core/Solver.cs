using System;
using System.Collections.Generic;
using TowersOfHanoi.Common;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Core
{
    public class Solver
    {
        private int previusElement = -1;
        private List<Stack<int>> pegs = new List<Stack<int>>();
        private Step step = new Step();

        private const int moveLeft = -1;
        private const int moveRight = 1;

        private int n;

        public Solver(int n)
        {
            this.n = n;
            pegs.Add(new Stack<int>());
            pegs.Add(new Stack<int>());
            pegs.Add(new Stack<int>());
        }

        public void Execute()
        {
            PreparePegLeft();

            Solve(n % 2 == 0 ? 1 : -1 );
        }

        private void Solve(int direction)
        {
            if (pegs[2].Count == n)
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

            this.step.Source = (Peg)fromIndex;
            this.step.Target = (Peg)newIndex;
            LocalDataBase.Steps.Add(this.step);

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

        private void PreparePegLeft()
        {
            for (int i = n; i > 0; i--)
            {
                pegs[0].Push(i);
            }
        }
    }
}
