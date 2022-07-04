using Ninject.Modules;
using PowerPlantCzarnobyl.Domain.Interfaces;

namespace PowerPlantCzarnobyl.Infrastructure.DependencyInjection
{
    public class InfrastructureModel: NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IErrorsRepository>().To<ErrorsRepository>();
            Kernel.Bind<IInspectionRepository>().To<InspectionRepository>();
            Kernel.Bind<IMembersRepository>().To<MembersRepository>();
            Kernel.Bind<IRecievedDataRepository>().To<ReceivedDataRepository>();
        }
    }
}
