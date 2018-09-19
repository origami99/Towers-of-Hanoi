using System;
using TowersOfHanoi.Common;

namespace TowersOfHanoi.Models
{
    public struct Step
    {
        public Peg Source { get; set; }

        public Peg Target { get; set; }
    }
}
