using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Clients;
using System;
using System.IO;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class ErrorsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly ErrorWebApiClient _errorWebApiClient;

        public ErrorsHandler()
        {
            _cliHelper = new CliHelper();
            _errorWebApiClient = new ErrorWebApiClient();
        }

        public void ShowAllErrors()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date");
            var endDate = _cliHelper.GetDateFromUser("enter end date");

            var errors = _errorWebApiClient.GetAllErrors(startDate, endDate).Result;

            if (errors != null)
            {
                foreach (var error in errors)
                {
                    Console.WriteLine($"Plant name: {error.PlantName}");
                    Console.WriteLine($"Machine name: {error.MachineName}");
                    Console.WriteLine($"Parameter: {error.Parameter}");
                    Console.WriteLine($"Error time: {error.ErrorTime}");
                    Console.WriteLine($"Logged member: {error.LoggedUser}");
                    Console.WriteLine($"Minimal value: {error.MinValue}");
                    Console.WriteLine($"Maximum value: {error.MaxValue}");
                }
            }
        }

        public async void ExportErrorsListToJson()
        {
            DateTime startDate = _cliHelper.GetDateFromUser("give me start date in format yyyy/MM/dd:GHH:mm");
            DateTime endDate = _cliHelper.GetDateFromUser("give me end date in format yyyy/MM/dd:GHH:mm");

            Console.Write("Enter file name to save data: ");
            string fileName = Console.ReadLine();
            fileName += ".json";

            while (File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} already exists! Choose different name: ");
                fileName = Console.ReadLine();
                fileName += ".json";
            }

            string json = JsonConvert.SerializeObject(_errorWebApiClient.GetAllErrors(startDate, endDate).Result, Formatting.Indented);
            File.WriteAllText(fileName, json);

            if (!File.Exists(fileName))
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"List of errors wasn't created, something went wrong....");
                Console.ForegroundColor = defaultColor;
            }
            else
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"List of errors was created succesfully :)");
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}
