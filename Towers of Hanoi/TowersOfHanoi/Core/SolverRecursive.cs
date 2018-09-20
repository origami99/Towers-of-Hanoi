using System;
using System.Collections.Generic;
using TowersOfHanoi.Models;
using System.Linq;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Core
{
    class SolverRecursive
    {

        private Step step = new Step();
        private int numbOfElements;

        private static Stack<int> left;
        private static Stack<int> middle = new Stack<int>();
        private static Stack<int> right = new Stack<int>();

        public SolverRecursive(int n)
        {
            this.numbOfElements = n;
            left = new Stack<int>(Enumerable.Range(1, numbOfElements).Reverse());
        }

        public void Execute()
        {
            Solve(numbOfElements, left, right, middle);
        }

        private void Solve(int bottomDisk, Stack<int> left, Stack<int> right, Stack<int> middle)
        {
            if (bottomDisk == 1)
            {
                right.Push(left.Pop());
                this.step.Source = Peg.Left;
                this.step.Target = Peg.Right;
                LocalDataBase.Steps.Add(this.step);
            }
            else
            {
                Solve(bottomDisk - 1, left, middle, right);

                right.Push(left.Pop());

                this.step.Source = Peg.Left;
                this.step.Target = Peg.Middle;
                LocalDataBase.Steps.Add(this.step);

                Solve(bottomDisk - 1, middle, right, left);
            }
        }
    }
}
