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
        private readonly LibraryService _libraryService;
        private readonly ErrorService _errorService;

        public Program()
        {
            var errorRepostiory = new ErrorsRepository();
            var libraryRepository = new LibraryRepository();
            var dateProvider = new DateProvider();
            var membersRepository = new MembersRepository();
            var membersService = new MemberService(membersRepository);

            _loginHandler = new LoginHandler(membersService);
            _errorService = new ErrorService(errorRepostiory, dateProvider);
            _libraryService = new LibraryService(libraryRepository);
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
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += _errorService.CheckIfMachinesWorkCorrectly;
        }
    }
}
