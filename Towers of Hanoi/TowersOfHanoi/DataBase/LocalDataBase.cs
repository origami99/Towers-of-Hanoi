using System;
using System.Collections.Generic;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.DataBase
{
    public class LocalDataBase
    {
        private List<Step> steps;
        private int diskCounts;

        public int DiskCounts { get => diskCounts; set => diskCounts = value; }
        public List<Step> Steps { get => new List<Step>(this.steps); set => steps = value; }
    }
}
