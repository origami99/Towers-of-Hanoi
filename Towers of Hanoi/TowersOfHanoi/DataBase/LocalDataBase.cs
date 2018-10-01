using System.Collections.Generic;
using TowersOfHanoi.Core;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.DataBase
{
    public class LocalDataBase : IDataBase
    {
        private List<Step> steps = new List<Step>();
        private int diskCounts;

        public int DiskCounts
        {
            get => diskCounts;
            set => diskCounts = value;
        }

        public List<Step> Steps
        {
            get => steps;
            set => steps = value;
        }
    }
}
