using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl
{
    public interface ICliHelper
    {
        string GetStringFromUser(string message);
        Member GetMemberFromAdmin();
        DateTime GetDateFromUser(string message);
        Inspection GetInspectionFromUser();
    }

    internal class CliHelper : ICliHelper
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

        public Member GetMemberFromAdmin()
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

        public Inspection GetInspectionFromUser()
        {
            Inspection inspection = new Inspection
            {
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                EndDate = DateTime.Now,
                MachineName = string.Empty,
                Comments = string.Empty,
            };
            return inspection;
        }
    }
}