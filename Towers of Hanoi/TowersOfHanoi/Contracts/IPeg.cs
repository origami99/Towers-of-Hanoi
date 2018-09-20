using System.Collections.Generic;
using TowersOfHanoi.Common;
using TowersOfHanoi.Models;

namespace TowersOfHanoi.Contracts
{
    public interface IPeg
    {
        Position TopPivot { get; }

        List<Disk> Disks { get; }

        PegType Type { get; }

        void AddDisk(Disk disk);

        Disk PopDisk();
    }
}