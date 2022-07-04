using Microsoft.Owin.Logging;
using Ninject;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Routing;

namespace PowerPlantCzarnobyl.MVC.PP.App_Start
{
    public class NinjectDiConfig
    {
        public static void UseNinject()
        {
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        private class NinjectControllerFactory : DefaultControllerFactory
        {
            private IKernel ninjectKernel = new StandardKernel(GetModules());

            private static INinjectModule[] GetModules()
            {
                return new INinjectModule[]
                {
                    new DomainModule(),
                    new InfrastructureModule(),
                    new MvcModule()
                };
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                return controllerType == null
                    ? null
                    : (IController)ninjectKernel.Get(controllerType);
            }
        }

        private class MvcModule : NinjectModule
        {
            public object RollingInterval { get; private set; }

            public override void Load()
            {
                Bind<ILogger>().ToConstant(SetUpLogger());
            }

            private ILogger SetUpLogger()
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .CreateLogger();

                return logger;
            }
        }
    }
}