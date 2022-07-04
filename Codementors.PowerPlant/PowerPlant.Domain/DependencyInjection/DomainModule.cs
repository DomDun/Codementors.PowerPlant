using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.DependencyInjection
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IConsoleManager>().To<ConsoleManager>();
            Kernel.Bind<IDateProvider>().To<DateProvider>();
            Kernel.Bind<IErrorService>().To<ErrorService>();
            Kernel.Bind<IInspectionService>().To<InspectionService>();
            Kernel.Bind<IMemberService>().To<MemberService>();
            Kernel.Bind<IReceivedDataService>().To<ReceivedDataService>();
        }
    }
}
