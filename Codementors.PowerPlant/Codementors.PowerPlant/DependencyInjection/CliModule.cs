using Ninject.Modules;

namespace PowerPlantCzarnobyl.DependencyInjection
{
    internal class CliModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICliHelper>().To<CliHelper>();
            Kernel.Bind<ILoginHandler>().To<LoginHandler>();
            Kernel.Bind<IPowerPlantActionsHandler>().To<PowerPlantActionsHandler>();
            Kernel.Bind<IErrorHandler>().To<ErrorHandler>();
        }
    }
}
