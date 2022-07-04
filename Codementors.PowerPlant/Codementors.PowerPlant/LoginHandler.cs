using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl
{
    public interface ILoginHandler
    {
        string LoginMember();
        bool DeleteMember(Member admin);
        bool AddMember(Member loggedUser);
    }

    public class LoginHandler : ILoginHandler
    {
        private readonly ICliHelper _cliHelper;
        private readonly IMemberService _iMembersService;
        private readonly IConsoleManager _iConsoleManager;

        public LoginHandler(IMemberService iMembersService, IConsoleManager iConsoleManager, ICliHelper iCliHelper)
        {
            _iMembersService = iMembersService;
            _iConsoleManager = iConsoleManager;
            _cliHelper = iCliHelper;
        }

        public string LoginMember()
        {
            string login = _cliHelper.GetStringFromUser("Type Your login");
            string password = _cliHelper.GetStringFromUser("Type Your password");

            bool correctCredentials = _iMembersService.CheckUserCredentials(login, password);

            if (correctCredentials)
            {
                _iConsoleManager.WriteLine($"Hello {login}!");
            }
            else
            {
                _iConsoleManager.WriteLine("Login unsuccesful. Try again...");
                return null;
            }

            return login;
        }

        public bool DeleteMember(Member loggedUser)
        {
            _iConsoleManager.Clear();

            if (loggedUser.Role != "Admin")
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
                return false;
            }

            bool correctCredentials;
            do
            {
                string password = _cliHelper.GetStringFromUser("Type Your password to confirm You are Admin");
                correctCredentials = _iMembersService.CheckUserCredentials(loggedUser.Login, password);
            } while (!correctCredentials);

            string loginToDelete = _cliHelper.GetStringFromUser("Type login of member You want to delete");
            if (loggedUser.Login == loginToDelete)
            {
                _iConsoleManager.WriteLine("\nYou can't delete Your own account\n");
                return false;
            }
            else
            {
                bool success = _iMembersService.Delete(loginToDelete);

                string message = success
                    ? "\nMember deleted successfully\n"
                    : "\nError when deleting Member\n";

                _iConsoleManager.WriteLine(message);
                return success;
            }
        }

        public bool AddMember(Member loggedUser)
        {
            _iConsoleManager.Clear();

            if(loggedUser.Role != "Admin")
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
                return false;
            }

            Member member = _cliHelper.GetMemberFromAdmin();

            bool success = _iMembersService.Add(member);

            string message = success
                ? "\nMember added successfully\n"
                : "\nError when adding Member\n";

            _iConsoleManager.WriteLine(message);
            return success;
        }
    }
}