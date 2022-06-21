using System;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private readonly LoginHandler _loginHandler;
        private readonly PowerPlantActionsHandler _powerPlantActionsHandler;
        private readonly RecievedDataHandler _recievedDataHandler;

        public Program()
        {
            _loginHandler = new LoginHandler();
            _powerPlantActionsHandler = new PowerPlantActionsHandler();
            _recievedDataHandler = new RecievedDataHandler();
        }

        private void Run()
        {
            _recievedDataHandler.StartWork();

            string loggedUser = _loginHandler.LoginLoop();

            if (!string.IsNullOrEmpty(loggedUser))
            {
                _powerPlantActionsHandler.ProgramLoop(loggedUser);
            }
        }
    }
}
