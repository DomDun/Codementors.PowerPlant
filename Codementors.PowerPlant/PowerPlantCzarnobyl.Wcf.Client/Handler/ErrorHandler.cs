using PowerPlantCzarnobyl.Wcf.Client.Client;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Linq;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    internal class ErrorHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly ErrorManagementClient _errorManagementClient;
        private readonly InspectionManagementClient _inspectionManagementClient;
        public string _loggedUser;

        public ErrorHandler()
        {
            _cliHelper = new CliHelper();
            _errorManagementClient = new ErrorManagementClient();
            _inspectionManagementClient = new InspectionManagementClient();
        }
        internal void ShowAllErrors()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date (yyyy/MM/dd:GHH:mm)");
            var endDate = _cliHelper.GetDateFromUser("enter end date (yyyy/MM/dd:GHH:mm)");

            var errors = _errorManagementClient.GetAllErrors(startDate, endDate);

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

            Console.ReadKey();
            Console.Clear();
        }

        internal void CheckIfMachinesWorkCorrectly(object sender, PowerPlantDataSetWcf plant)
        {
            foreach (var cauldron in plant.Cauldrons)
            {
                if (!CheckIfMachineIsInspected(cauldron.Name))
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

        public bool CheckValue(string machineName, string parameter, AssetParameterDataWcf value, PowerPlantDataSetWcf plant, string user)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                ErrorWcf error = new ErrorWcf()
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

        public void Add(ErrorWcf error)
        {
            _errorManagementClient.AddError(error);
        }

        public bool CheckIfMachineIsInspected(string machineName)
        {
            var inspections = _inspectionManagementClient.GetAllInspections();
            var cos = inspections
                .Where(x => x.MachineName == machineName)
                .Where(x => x.State != State.Closed)
                .ToList();

            if (cos.Count > 0)
            {
                return true;
            }
            else return false;
        }

        internal void ShowErrorsStats()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date (yyyy/MM/dd:GHH:mm)");
            var endDate = _cliHelper.GetDateFromUser("enter end date (yyyy/MM/dd:GHH:mm)");

            var errors = _errorManagementClient.GetAllErrorsInDictionary(startDate, endDate);

            foreach (var error in errors)
            {
                Console.WriteLine($"Machine: {error.Key} count of errors: {error.Value}");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}