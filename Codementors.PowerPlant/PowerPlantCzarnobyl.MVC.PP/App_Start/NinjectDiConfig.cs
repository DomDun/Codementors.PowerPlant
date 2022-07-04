using Ninject;
using Ninject.Modules;
using PowerPlantCzarnobyl.Domain.DependencyInjection;
using PowerPlantCzarnobyl.Infrastructure.DependencyInjection;
using System;
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
                    new InfrastructureModel()
                };
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                return controllerType == null
                    ? null
                    : (IController)ninjectKernel.Get(controllerType);
            }
        }
    }
}