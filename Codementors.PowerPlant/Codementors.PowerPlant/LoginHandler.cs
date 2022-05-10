using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;

namespace PowerPlantCzarnobyl
{
    public interface ILoginHandler
    {
        string LoginMember();
        bool DeleteMember(Member admin);
        bool AddMember(Member admin);
        void Clear();
    }

    public class LoginHandler : ILoginHandler
    {
        //private readonly MemberService _memberService;
        private readonly CliHelper _cliHelper = new CliHelper();
        private readonly IMembersService _iMembersService;

        public LoginHandler(IMembersService iMembersService)
        {
            //var membersRepostiory = new MembersRepository();

            _iMembersService = iMembersService;
            //_memberService = new MemberService(membersRepostiory);
        }

        public string LoginMember()
        {
            string login = _cliHelper.GetStringFromUser("Type Your login");
            string password = _cliHelper.GetStringFromUser("Type Your password");

            bool correctCredentials = _iMembersService.CheckUserCredentials(login, password);

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

        public bool DeleteMember(Member admin)
        {
            Clear();

            if (admin.Role == "Admin")
            {
                bool correctCredentials;
                do
                {
                    string password = _cliHelper.GetStringFromUser("Type Your password to confirm You are Admin");
                    correctCredentials = _iMembersService.CheckUserCredentials(admin.Login, password);
                } while (!correctCredentials);

                string loginToDelete = _cliHelper.GetStringFromUser("Type login of member You want to delete");
                if (admin.Login == loginToDelete)
                {
                    Console.WriteLine("\nYou can't delete Your own account\n");
                }
                else
                {
                    bool success = _iMembersService.Delete(loginToDelete);

                    string message = success
                        ? "\nMember deleted successfully\n"
                        : "\nError when deleting Member\n";

                    Console.WriteLine(message);
                }
                return true;
            }
            else
            {
                Console.WriteLine("You are not authorized to add new member. Go to Your CEO for a promotion :)");
                return false;
            }
        }

        public bool AddMember(Member admin)
        {
            Clear();

            if (admin.Role == "Admin")
            {
                Member member = _cliHelper.GetMemberFromAdmin();

                bool success = _iMembersService.Add(member);

                string message = success
                    ? "\nMember added successfully\n"
                    : "\nError when adding Member\n";

                Console.WriteLine(message);
                return true;
            }
            else
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
                return false;
            }
        }

        public void Clear() => Console.Clear();
    }
}