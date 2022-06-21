using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;

namespace PowerPlantCzarnobyl
{
    internal class Program
    {
        static void Main()
        {
            new Program().Run();
        }

        private readonly LoginHandler _loginHandler;
        private static readonly PowerPlantActionsHandler _powerPlantActionsHandler = new PowerPlantActionsHandler();
        private readonly ReceivedDataService _recievedDataService;
        private readonly ErrorService _errorService;

        public Program()
        {
            var errorRepostiory = new ErrorsRepository();
            var recievedDataRepository = new ReceivedDataRepository();
            var dateProvider = new DateProvider();
            var consoleManager = new ConsoleManager();
            var membersRepository = new MembersRepository();
            var membersService = new MemberService(membersRepository);
            var cliHelper = new CliHelper();

            _loginHandler = new LoginHandler(membersService, consoleManager, cliHelper);
            _errorService = new ErrorService(errorRepostiory, dateProvider);
            _recievedDataService = new ReceivedDataService(recievedDataRepository);
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
