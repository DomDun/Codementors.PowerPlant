using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl
{
    internal class CliHelper
    {
        public string GetStringFromUser(string message)
        {
            string inputFromUser;

            do
            {
                Console.Write($"{message}: ");
                inputFromUser = Console.ReadLine();

                if (string.IsNullOrEmpty(inputFromUser))
                {
                    Console.WriteLine("You have to type something");
                }

            } while (string.IsNullOrEmpty(inputFromUser));

            return inputFromUser;
        }

        internal Member GetMemberFromAdmin()
        {
            Member member = new Member
            {
                Login = GetStringFromUser("Add login of new member"),
                Password = GetStringFromUser("Add pasword"),
                Role = string.Empty,
            };

            do
            {
                member.Role = GetStringFromUser("Add role for new member");
                if (member.Role != "Admin" && member.Role != "User")
                {
                    Console.WriteLine("You have to type Admin or User! try again");
                }
            }while (member.Role != "Admin" && member.Role != "User");

            return member;
        }
    }
}