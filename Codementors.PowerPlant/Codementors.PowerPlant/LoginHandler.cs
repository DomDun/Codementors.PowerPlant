using PowerPlantCzarnobyl.Domain;
using System;

namespace PowerPlantCzarnobyl
{
    internal class LoginHandler
    {
        private readonly MemberService _memberService;
        private readonly CliHelper _cliHelper = new CliHelper();

        public string LoginMember()
        {
            string login = _cliHelper.GetStringFromUser("Type Your login");
            string password = _cliHelper.GetStringFromUser("Type Your password");

            bool correctCredentials = _memberService.CheckUserCredentials(login, password);

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