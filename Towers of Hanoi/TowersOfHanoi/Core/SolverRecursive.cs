using TowersOfHanoi.Models;
using TowersOfHanoi.DataBase;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Core
{
    public class SolverRecursive : ISolver
    {
        private readonly IDataBase dataBase;
        private Step step = new Step();
        private int numbOfElements;

        public SolverRecursive(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public void Execute()
        {
            this.numbOfElements = dataBase.DiskCounts;
            Solve(numbOfElements, PegType.Left, PegType.Right, PegType.Middle);
        }

        private void Solve(int bottomDisk, PegType left, PegType right, PegType middle)
        {
            if (bottomDisk == 1)
            {
                this.step.Source = left;
                this.step.Target = right;
                this.dataBase.Steps.Add(this.step);
            }
            else
            {
                Solve(bottomDisk - 1, left, middle, right);

                this.step.Source = left;
                this.step.Target = right;
                this.dataBase.Steps.Add(this.step);

                Solve(bottomDisk - 1, middle, right, left);
            }
        }
    }
}