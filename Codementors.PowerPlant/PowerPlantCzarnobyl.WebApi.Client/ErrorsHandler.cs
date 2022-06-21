using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Linq;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class ErrorsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly ErrorWebApiClient _errorWebApiClient;
        private readonly InspectionWebApiClient _inspectionWebApiClient;
        public string _loggedUser;
        public ErrorsHandler()
        {
            _cliHelper = new CliHelper();
            _errorWebApiClient = new ErrorWebApiClient();
            _inspectionWebApiClient = new InspectionWebApiClient();
        }

        public void ShowAllErrors()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date (yyyy/MM/dd:GHH:mm)");
            var endDate = _cliHelper.GetDateFromUser("enter end date (yyyy/MM/dd:GHH:mm)");

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
                    Console.WriteLine($"Maximum value: {error.MaxValue}\n");
                }
            }

            Console.ReadKey();
            Console.Clear();
        }

        internal async void ShowErrorsStats()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date (yyyy/MM/dd:GHH:mm)");
            var endDate = _cliHelper.GetDateFromUser("enter end date (yyyy/MM/dd:GHH:mm)");

            var errors = await _errorWebApiClient.GetAllErrorsInDictionary(startDate, endDate);

            foreach (var error in errors)
            {
                Console.WriteLine($"Machine: {error.Key} count of errors: {error.Value}");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void Add(Error error)
        {
            _errorWebApiClient.AddError(error);
        }

        public void CheckIfMachinesWorkCorrectly(object sender, PowerPlantDataSet plant)
        {
            foreach (var cauldron in plant.Cauldrons)
            {
                if(!CheckIfMachineIsInspected(cauldron.Name))
                {
                    CheckValue(cauldron.Name, "WaterPressure", cauldron.WaterPressure, plant, _loggedUser);
                    CheckValue(cauldron.Name, "WaterTemperature", cauldron.WaterTemperature, plant, _loggedUser);
                    CheckValue(cauldron.Name, "CamberTemperature", cauldron.CamberTemperature, plant, _loggedUser);
                }
            }

            foreach (var turbine in plant.Turbines)
            {
                if (!CheckIfMachineIsInspected(turbine.Name))
                {
                    CheckValue(turbine.Name, "SteamPressure", turbine.SteamPressure, plant, _loggedUser);
                    CheckValue(turbine.Name, "OverheaterSteamTemperature", turbine.OverheaterSteamTemperature, plant, _loggedUser);
                    CheckValue(turbine.Name, "OutputVoltage", turbine.OutputVoltage, plant, _loggedUser);
                    CheckValue(turbine.Name, "RotationSpeed", turbine.RotationSpeed, plant, _loggedUser);
                    CheckValue(turbine.Name, "CurrentPower", turbine.CurrentPower, plant, _loggedUser);
                }
            }

            foreach (var transformator in plant.Transformators)
            {
                if (!CheckIfMachineIsInspected(transformator.Name))
                {
                    CheckValue(transformator.Name, "InputVoltage", transformator.InputVoltage, plant, _loggedUser);
                    CheckValue(transformator.Name, "OutputVoltage", transformator.OutputVoltage, plant, _loggedUser);
                }
            }
        }

        public bool CheckValue(string machineName, string parameter, AssetParameter value, PowerPlantDataSet plant, string user)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                Error error = new Error()
                {
                    PlantName = plant.PlantName,
                    MachineName = machineName,
                    Parameter = parameter,
                    ErrorTime = DateTime.Now,
                    LoggedUser = user,
                    MinValue = value.MinValue,
                    MaxValue = value.MaxValue
                };

                if (user == null)
                {
                    error.LoggedUser = "N/A";
                }

                Add(error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfMachineIsInspected(string machineName)
        {
            var inspections = _inspectionWebApiClient.GetAllInspections().Result;
            var cos = inspections
                .Where(x=>x.MachineName == machineName)
                .Where(x=>x.State != State.Closed)
                .ToList();

            if(cos.Count > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
