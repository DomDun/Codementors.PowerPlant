using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    internal class MemberHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly MemberManagementClient _memberManagementClient;

        public MemberHandler()
        {
            _cliHelper = new CliHelper();
            _memberManagementClient = new MemberManagementClient();
        }

        public bool DeleteMember(MemberWcf loggedUser)
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
                correctCredentials = _memberManagementClient.LoginAsync(loggedUser.Login, password);
            } while (!correctCredentials);

            string loginToDelete = _cliHelper.GetStringFromUser("Type login of member You want to delete");
            if (loggedUser.Login == loginToDelete)
            {
                Console.WriteLine("\nYou can't delete Your own account\n");
                return false;
            }
            else
            {
                bool success = _memberManagementClient.DeleteMember(loginToDelete);

                string message = success
                    ? "\nMember deleted successfully\n"
                    : "\nError when deleting Member\n";

                Console.WriteLine(message);
                return success;
            }
        }

        public bool AddMember(MemberWcf loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role != "Admin")
            {
                Console.WriteLine("\nYou are not authorized to add new member. Go to Your CEO for a promotion :)\n");
                return false;
            }

            MemberWcf member = _cliHelper.GetMemberFromAdmin();

            bool success = _memberManagementClient.AddMember(member);

            string message = success
                ? "\nMember added successfully\n"
                : "\nError when adding Member\n";

            Console.WriteLine(message);
            return success;
        }
    }
}
