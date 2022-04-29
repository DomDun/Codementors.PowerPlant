namespace PowerPlantCzarnobyl
{
    internal class Program
    {
        private static readonly LoginHandler _loginHandler = new LoginHandler();
        private static readonly PowerPlantActionsHandler _powerPlantActionsHandler = new PowerPlantActionsHandler();

        static void Main()
        {
            bool exit = false;
            string loggedMember = null;

            while (!exit)
            {
                loggedMember = _loginHandler.LoginMember();
                exit = !string.IsNullOrEmpty(loggedMember);
            }

            if (!string.IsNullOrEmpty(loggedMember))
            {
                _powerPlantActionsHandler.ProgramLoop(loggedMember);
            }
        }
    }
}
