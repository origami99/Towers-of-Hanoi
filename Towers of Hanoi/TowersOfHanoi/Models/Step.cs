using System;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Models
{
    public struct Step
    {
        public PegType Source { get; set; }

        public PegType Target { get; set; }
    }
}
