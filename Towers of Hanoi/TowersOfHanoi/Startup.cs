using TowersOfHanoi.Core;

namespace TowersOfHanoi
{
    public class Startup
    {
        public static void Main()
        {
           // Engine.Instance.Start();

            var test = new Solver(4);

            test.Solve();
            
        }
    }
}
