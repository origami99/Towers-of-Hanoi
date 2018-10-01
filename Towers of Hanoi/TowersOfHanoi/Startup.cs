using Autofac;
using System.Reflection;
using TowersOfHanoi.Core;

namespace TowersOfHanoi
{
    public class Startup
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var conteiner = builder.Build();
            var engine = conteiner.Resolve<IEngine>();
            engine.Start();
        }
    }
}
