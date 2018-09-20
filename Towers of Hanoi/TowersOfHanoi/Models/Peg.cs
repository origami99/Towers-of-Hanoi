using System;
using System.Collections.Generic;
using TowersOfHanoi.Common;
using TowersOfHanoi.Contracts;

namespace TowersOfHanoi.Models
{
    public class Peg : IPeg
    {
        private readonly Position topPivot;
        private List<Disk> disks;

        public Peg(Position topPivot, List<Disk> disks, PegType type)
        {
            this.topPivot = topPivot;
            this.disks = disks;
            this.Type = type;
        }

        public Position TopPivot
        {
            get
            {
                return new Position(this.topPivot);
            }
        }

        public List<Disk> Disks
        {
            get
            {
                return this.disks;
            }
        }

        public PegType Type { get; }

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
