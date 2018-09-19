using System;
using System.Collections.Generic;
using TowersOfHanoi.Common;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Core
{
    public class Solver
    {
        private int previusElement = 0;
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

        public void Solve()
        {
            PreparePegLeft();

            if (n % 2 != 0)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
            }
        }

        private void MoveLeft()
        {

        }

        private void MoveRight()
        {
            if (pegs[2].Count == n)
            {
                return;
            }

            if (LocalDataBase.Steps == null)
            {
                previusElement = 0;
            }

            int fromIndex = FindBiggest();
            int newIndex = CanMoveOnPosition(moveRight, fromIndex);

            if (newIndex < 0)
            {
                fromIndex = FindSmallest();
                newIndex = CanMoveOnPosition(moveRight, fromIndex);
            }

            this.step.Source = (Peg)fromIndex;
            this.step.Target = (Peg)newIndex;
            LocalDataBase.Steps.Add(this.step);

            previusElement = newIndex;

            pegs[newIndex].Push(pegs[fromIndex].Pop());

            MoveRight();

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

        private int CanMoveOnPosition(int direction, int fromPosition)
        {
            int newPosition = -1;
            int length = pegs.Count;

            for (int i = fromPosition; i < length + fromPosition; i += direction)
            {
                if (pegs[(i + 1) % length].Count == 0)
                {
                    return (i + 1) % length;
                }
                else if (pegs[fromPosition].Peek() < pegs[(i + 1) % length].Peek())
                {
                    return (i + 1) % length;
                }
            }

            return newPosition;
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
