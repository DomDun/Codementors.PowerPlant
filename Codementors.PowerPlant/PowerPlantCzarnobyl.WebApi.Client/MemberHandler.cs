using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class MemberHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly MemberWebApiClient _memberWebApiClient;

        public MemberHandler()
        {
            _cliHelper = new CliHelper();
            _memberWebApiClient = new MemberWebApiClient();
        }

        public bool DeleteMember(MemberWebApi loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role != "Admin")
            {
                Console.WriteLine("\nYou are not authorized to delete member. Go to Your CEO for a promotion :)\n");
                return false;
            }

            bool correctCredentials;
            do
            {
                string password = _cliHelper.GetStringFromUser("Type Your password to confirm You are Admin");
                correctCredentials = _memberWebApiClient.Login(loggedUser.Login, password).Result;
            } while (!correctCredentials);

            string loginToDelete = _cliHelper.GetStringFromUser("Type login of member You want to delete");
            if (loggedUser.Login == loginToDelete)
            {
                Console.WriteLine("\nYou can't delete Your own account\n");
                return false;
            }
            else
            {
                bool success = _memberWebApiClient.Delete(loginToDelete).Result;

                string message = success
                    ? "\nMember deleted successfully\n"
                    : "\nError when deleting Member\n";

                Console.WriteLine(message);
                return success;
            }
        }

        public bool AddMember(MemberWebApi loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role != "Admin")
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
                return false;
            }

            MemberWebApi member = _cliHelper.GetMemberFromAdmin();

            bool success = _memberWebApiClient.AddMember(member).Result;

            string message = success
                ? "\nMember added successfully\n"
                : "\nError when adding Member\n";

            Console.WriteLine(message);
            return success;
        }
    }
}
