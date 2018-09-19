using System;
using System.Collections.Generic;
using TowersOfHanoi.DataBase;

namespace TowersOfHanoi.Core
{
    public class Solver
    {
        private int previusElement = 0;
        private List<Stack<int>> pegs = new List<Stack<int>>();

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

            if (LocalDataBase.Steps.Count == 0)
            {
                previusElement = 1;
            }

            int bigesstElement = FindBiggest();
            int newIndex = CanMoveOnPosition(moveRight, bigesstElement);

            if (newIndex >= 0)
            {
                pegs[newIndex].Push(pegs[bigesstElement].Pop()); // check if it previusElement???
            }

        }

        private int CanMoveOnPosition(int direction, int fromPosition)
        {
            int newPosition = -1;
            for (int i = fromPosition; i < n + fromPosition; i += direction)
            {
                if (pegs[(i + 1) % n].Count == 0)
                {
                    return (i + 1) % n;
                }
                else if (pegs[fromPosition].Peek() < pegs[(i + 1) % n].Peek())
                {
                    return (i + 1) % n;
                }
            }

            return newPosition;
        }

        private int FindBiggest()
        {
            int left = 0, mid = 0, right = 0;

            if (pegs[0].Count > 0)
            {
                left = pegs[0].Peek();
            }
            if (pegs[1].Count > 0)
            {
                mid = pegs[1].Peek();
            }
            if (pegs[2].Count > 0)
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
