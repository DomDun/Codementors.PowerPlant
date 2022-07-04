using Ninject;
using PowerPlantCzarnobyl.Domain.DependencyInjection;
using PowerPlantCzarnobyl.Infrastructure.DependencyInjection;

namespace PowerPlantCzarnobyl.DependencyInjection
{
    internal class DependencyResolver
    {
        private static IKernel _kernel = null;

        public IKernel GetKernel()
        {
            if (_kernel != null)
            {
                return _kernel;
            }

            _kernel = new StandardKernel();

            _kernel.Load(
                new CliModule(),
                new DomainModule(),
                new InfrastructureModel());

            return _kernel;
        }
    }
}
