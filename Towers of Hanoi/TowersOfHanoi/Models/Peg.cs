using System;
using System.Collections.Generic;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Models
{
    public class Peg
    {
        private readonly Position topPivot;
        private List<Disk> disks;

        public Peg(Position topPivot, List<Disk> disks, PegType type)
        {
            this.topPivot = topPivot;
            this.disks = disks;
            this.Type = type;
        }

        public List<Disk> Disks
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

        public void AddDisk(Disk disk)
        {
            disks.Add(disk);
        }

        public Disk PopDisk()
        {
            if (disks.Count == 0)
                throw new ArgumentException("There are no disks on this peg");

            int lastIndex = disks.Count - 1;

            Disk element = disks[lastIndex];

            disks.RemoveAt(lastIndex);

            return element;
        }
    }
}
