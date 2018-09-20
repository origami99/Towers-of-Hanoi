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

        public SolverRecursive(int n)
        {
            this.numbOfElements = n;
        }

        public void Execute()
        {
            Solve(numbOfElements, PegType.Left, PegType.Right, PegType.Middle);
        }

        private void Solve(int bottomDisk, PegType left, PegType right, PegType middle)
        {
            if (bottomDisk == 1)
            {
                this.step.Source = left;
                this.step.Target = right;
                LocalDataBase.Steps.Add(this.step);
            }
            else
            {
                Solve(bottomDisk - 1, left, middle, right);

                this.step.Source = left;
                this.step.Target = right;
                LocalDataBase.Steps.Add(this.step);

                Solve(bottomDisk - 1, middle, right, left);
            }
        }
    }
}
