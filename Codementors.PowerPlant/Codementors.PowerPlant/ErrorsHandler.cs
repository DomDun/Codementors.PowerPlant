using Newtonsoft.Json;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.IO;

namespace PowerPlantCzarnobyl
{
    internal class ErrorsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly ErrorService _errorService;
        private readonly ConsoleManager _consoleManager;

        public ErrorsHandler()
        {
            var dateProvider = new DateProvider();
            var errorsRepository = new ErrorsRepository();

            _cliHelper = new CliHelper();
            _errorService = new ErrorService(errorsRepository, dateProvider);
            _consoleManager = new ConsoleManager();
        }
        public void ShowAllErrors()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date");
            var endDate = _cliHelper.GetDateFromUser("enter end date");

            var errors = _errorService.GetAllErrorsAsync(startDate, endDate).Result;

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
            DateTime startData = _cliHelper.GetDateFromUser("give me start date in format yyyy/MM/dd:GHH:mm");
            DateTime endData = _cliHelper.GetDateFromUser("give me end date in format yyyy/MM/dd:GHH:mm");

            _consoleManager.Write("Enter file name to save data: ");
            string fileName = _consoleManager.ReadLine();
            fileName += ".json";

            while (File.Exists(fileName))
            {
                _consoleManager.WriteLine($"File {fileName} already exists! Choose different name: ");
                fileName = _consoleManager.ReadLine();
                fileName += ".json";
            }

            string json = JsonConvert.SerializeObject(await _errorService.GetAllErrorsAsync(startData, endData), Formatting.Indented);
            File.WriteAllText(fileName, json);

            if (!File.Exists(fileName))
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                _consoleManager.WriteLine($"List of errors wasn't created, something went wrong....");
                Console.ForegroundColor = defaultColor;
            }
            else
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                _consoleManager.WriteLine($"List of errors was created succesfully :)");
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}
