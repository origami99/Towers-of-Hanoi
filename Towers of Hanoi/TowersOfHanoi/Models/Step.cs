using TowersOfHanoi.Common;
using TowersOfHanoi.Contracts;

namespace TowersOfHanoi.Models
{
    public struct Step : IStep
    {
        public PegType Source { get; set; }

        public PegType Target { get; set; }
    }
}
