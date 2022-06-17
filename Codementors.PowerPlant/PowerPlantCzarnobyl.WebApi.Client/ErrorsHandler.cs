using Newtonsoft.Json;
using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.IO;
using System.Linq;

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
                    Console.WriteLine($"\nPlant name: {error.PlantName}");
                    Console.WriteLine($"Machine name: {error.MachineName}");
                    Console.WriteLine($"Parameter: {error.Parameter}");
                    Console.WriteLine($"Error time: {error.ErrorTime}");
                    Console.WriteLine($"Logged member: {error.LoggedUser}");
                    Console.WriteLine($"Minimal value: {error.MinValue}");
                    Console.WriteLine($"Maximum value: {error.MaxValue}");
                }
            }
        }

        internal async void ShowErrorsStats()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date");
            var endDate = _cliHelper.GetDateFromUser("enter end date");

            var errors = await _errorWebApiClient.GetAllErrorsInDictionary(startDate, endDate);

            foreach (var error in errors)
            {
                Console.WriteLine($"Machine: {error.Key} count of errors: {error.Value}");
            }
        }
    }
}
