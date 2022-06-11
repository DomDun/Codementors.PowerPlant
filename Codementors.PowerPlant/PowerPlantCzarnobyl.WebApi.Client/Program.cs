namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class Program
    {
        private static readonly LoginHandler _loginHandler = new LoginHandler();
        private static readonly PowerPlantActionsHandler _powerPlantActionsHandler = new PowerPlantActionsHandler();
        static void Main(string[] args)
        {
            string loggedUser = _loginHandler.LoginLoop();

            if (!string.IsNullOrEmpty(loggedUser))
            {
                _powerPlantActionsHandler.ProgramLoop(loggedUser);
            }
        }
    }
}
