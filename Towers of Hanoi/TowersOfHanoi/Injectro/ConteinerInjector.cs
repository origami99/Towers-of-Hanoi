using System.Reflection;
using Autofac;
using TowersOfHanoi.Core;
using TowersOfHanoi.DataBase;

namespace TowersOfHanoi.Injectro
{
    internal class ConteinerInjector : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<LocalDataBase>().As<IDataBase>().SingleInstance();
            builder.RegisterType<InputFromConsole>().As<IInputData>().SingleInstance();
        }
    }
}
