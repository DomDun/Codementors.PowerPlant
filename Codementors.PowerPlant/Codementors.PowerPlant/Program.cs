using Ninject;
using PowerPlantCzarnobyl.DependencyInjection;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;

namespace PowerPlantCzarnobyl
{
    internal class Program
    {
        

        private readonly ILoginHandler _loginHandler;
        private readonly IPowerPlantActionsHandler _powerPlantActionsHandler;
        private readonly IReceivedDataService _recievedDataService;
        private readonly IErrorService _errorService;

        static void Main()
        {
            var kernel = new DependencyResolver().GetKernel();
            var program = kernel.Get<Program>();
            program.Run();
        }

        public Program(
            ILoginHandler loginHandler,
            IPowerPlantActionsHandler powerPlantActionsHandler,
            IReceivedDataService receivedDataService,
            IErrorService errorService)
        {
            _loginHandler = loginHandler;
            _errorService = errorService;
            _recievedDataService = receivedDataService;
            _powerPlantActionsHandler = powerPlantActionsHandler;
        }
        string loggedMember = null;
        public void Run()
        {
            StartWork();
            
            bool exit = false;
            

            while (!exit)
            {
                loggedMember = _loginHandler.LoginMember();
                exit = !string.IsNullOrEmpty(loggedMember);
            }

            if (!string.IsNullOrEmpty(loggedMember))
            {
                _errorService._loggedUser = loggedMember;
                _powerPlantActionsHandler.ProgramLoop(loggedMember);
            }
        }

        public void StartWork()
        {
            _recievedDataService.ActualDataSender();
            _recievedDataService.OnRecieveData += _errorService.CheckIfMachinesWorkCorrectly;
        }
    }
}
