using System;
using System.Collections.Generic;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Models
{
    public class Peg
    {
        private readonly Position topPivot;
        private List<int> disks;

        public Peg(Position topPivot, List<int> disks, PegType type)
        {
            this.topPivot = topPivot;
            this.disks = disks;
            this.Type = type;
        }

        public List<int> Disks
        {
            get
            {
                return this.disks;
            }
        }

        public Position TopPivot
        {
            get
            {
                return new Position(this.topPivot);
            }
        }

        public PegType Type { get; set; }

        public void AddDisk(int disk)
        {
            disks.Add(disk);
        }

        public int PopDisk()
        {
            if (disks.Count == 0)
                throw new ArgumentException("There are no disks on this peg");

            int lastIndex = disks.Count - 1;

            int element = disks[lastIndex];

            disks.RemoveAt(lastIndex);

            return element;
        }
    }
}
