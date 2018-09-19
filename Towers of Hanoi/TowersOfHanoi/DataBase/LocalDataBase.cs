using System.Collections.Generic;
using TowersOfHanoi.Core;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.DataBase
{
    public static class LocalDataBase
    {
        private static List<Step> steps = new List<Step>();
        private static int diskCounts = Input.DiscsCount ;

        public static int DiskCounts
        {
            get => diskCounts;
            set => diskCounts = value;
        }

        public static List<Step> Steps
        {
            get => new List<Step>(steps);
            set => steps = value;
        }
    }
}
