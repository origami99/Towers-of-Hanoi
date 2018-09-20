using TowersOfHanoi.Common;

namespace TowersOfHanoi.Contracts
{
    public interface IStep
    {
        PegType Source { get; set; }

        PegType Target { get; set; }
    }
}
