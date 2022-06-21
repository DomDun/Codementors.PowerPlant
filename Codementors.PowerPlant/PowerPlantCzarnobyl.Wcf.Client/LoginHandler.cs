using System;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    internal class LoginHandler
    {
        private readonly MemberManagementClient _memberManagementClient;
        private readonly CliHelper _cliHelper = new CliHelper();

        public LoginHandler()
        {
            _memberManagementClient = new MemberManagementClient();
        }

        public string LoginLoop()
        {
            bool exit = false;
            string loggedMember = null;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("[Login] Choose action [Login, Exit]");
                switch (operation)
                {
                    case "Login":
                        loggedMember = LoginMember();
                        exit = !string.IsNullOrEmpty(loggedMember);
                        break;
                    case "Exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("I don'tknow what You want, try again...");
                        break;
                }
            }

            return loggedMember;
        }

        public string LoginMember()
        {
            string login = _cliHelper.GetStringFromUser("Type Your login");
            string password = _cliHelper.GetStringFromUser("Type Your password");

            bool correctCredentials = _memberManagementClient.LoginAsync(login, password);

            if (correctCredentials)
            {
                Console.WriteLine($"Hello {login}!");
            }
            else
            {
                Console.WriteLine("Login unsuccesful. Try again...");
                return null;
            }

            return login;
        }
    }
}
