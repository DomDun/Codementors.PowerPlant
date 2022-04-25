using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;

namespace PowerPlantCzarnobyl
{
    internal class LoginHandler
    {
        private readonly MemberService _memberService;
        private readonly CliHelper _cliHelper = new CliHelper();

        public LoginHandler()
        {
            var memberRepostiory = new MembersRepository();

            _memberService = new MemberService(memberRepostiory);
        }

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

        public void DeleteMember(string loggedMember)
        {
            Console.Clear();
            Member admin = _memberService.CheckMemberRole(loggedMember);

            if (admin.Role == "Admin")
            {
                bool correctCredentials;
                do
                {
                    string password = _cliHelper.GetStringFromUser("Type Your password to confirm You are Admin");
                    correctCredentials = _memberService.CheckUserCredentials(loggedMember, password);
                } while (!correctCredentials); 
                
                string loginToDelete = _cliHelper.GetStringFromUser("Type login of member You want to delete");
                if (loggedMember == loginToDelete)
                {
                    Console.WriteLine("\nYou can't delete Your own account\n");
                }
                else
                {
                    bool success = _memberService.Delete(loginToDelete);

                    string message = success
                        ? "\nMember deleted successfully\n"
                        : "\nError when deleting Member\n";

                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine("You are not authorized to add new member. Go to Your CEO for a promotion :)");
            }
        }

        public void AddMember(string loggedMember)
        {
            Console.Clear();
            Member admin = _memberService.CheckMemberRole(loggedMember);

            if (admin.Role == "Admin")
            {
                Member member = _cliHelper.GetMemberFromAdmin();

                bool success = _memberService.Add(member);

                string message = success
                    ? "\nMember added successfully\n"
                    : "\nError when adding Member\n";

                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
            }
        }
    }
}