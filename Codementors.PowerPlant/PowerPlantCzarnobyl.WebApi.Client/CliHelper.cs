using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class CliHelper
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

        public MemberWebApi GetMemberFromAdmin()
        {
            MemberWebApi member = new MemberWebApi
            {
                Login = GetStringFromUser("Add login of new member"),
                Password = GetStringFromUser("Add pasword"),
                Role = string.Empty,
            };

            do
            {
                member.Role = GetStringFromUser("Add role for new member");
                if (member.Role != "Admin" && member.Role != "User" && member.Role != "Engineer")
                {
                    Console.WriteLine("You have to type Admin or User or Engineer! try again");
                }
            } while (member.Role != "Admin" && member.Role != "User" && member.Role != "Engineer");

            return member;
        }

        public DateTime GetDateFromUser(string message)
        {
            Console.WriteLine(message);
            string line = Console.ReadLine();
            DateTime data;
            while (!DateTime.TryParseExact(line, "yyyy/MM/dd:GHH:mm", null, System.Globalization.DateTimeStyles.None, out data))
            {
                Console.WriteLine("Invalid date, please retry");
                line = Console.ReadLine();
            }

            return data;
        }
    }
}
