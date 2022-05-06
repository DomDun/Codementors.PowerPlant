using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;

namespace PowerPlantCzarnobyl
{
    internal class Program
    {
        static void Main()
        {
            new Program().Run();
        }

        private static readonly LoginHandler _loginHandler = new LoginHandler();
        private static readonly PowerPlantActionsHandler _powerPlantActionsHandler = new PowerPlantActionsHandler();
        private readonly LibraryService _libraryService;
        private readonly ErrorService _errorService;

        public Program()
        {
            var errorRepostiory = new ErrorsRepository();
            var libraryRepository = new LibraryRepository();

            _errorService = new ErrorService(errorRepostiory);
            _libraryService = new LibraryService(libraryRepository);
        }
        string loggedMember = null;
        public void Run()
        {
            StartWork();
            
            bool exit = false;
            

            while (!exit)
            {
                loggedMember = _loginHandler.LoginMember();
                exit = !string.IsNullOrEmpty(loggedMember);
            }

            if (!string.IsNullOrEmpty(loggedMember))
            {
                _powerPlantActionsHandler.ProgramLoop(loggedMember);
            }
        }

        public void StartWork()
        {
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += CheckIfMachinesWorkCorrectly;
        }

        private void CheckIfMachinesWorkCorrectly(object sender, PowerPlantDataSetData plant)
        {
            foreach (var cauldron in plant.Cauldrons)
            {
                CheckValue(cauldron.Name, "WaterPressure", cauldron.WaterPressure, plant, loggedMember);
                CheckValue(cauldron.Name, "WaterTemperature", cauldron.WaterTemperature, plant, loggedMember);
                CheckValue(cauldron.Name, "CamberTemperature", cauldron.CamberTemperature, plant, loggedMember);
            }

            foreach (var turbine in plant.Turbines)
            {
                CheckValue(turbine.Name, "SteamPressure", turbine.SteamPressure, plant, loggedMember);
                CheckValue(turbine.Name, "OverheaterSteamTemperature", turbine.OverheaterSteamTemperature, plant, loggedMember);
                CheckValue(turbine.Name, "OutputVoltage", turbine.OutputVoltage, plant, loggedMember);
                CheckValue(turbine.Name, "RotationSpeed", turbine.RotationSpeed, plant, loggedMember);
                CheckValue(turbine.Name, "CurrentPower", turbine.CurrentPower, plant, loggedMember);
            }

            foreach (var transformator in plant.Transformators)
            {
                CheckValue(transformator.Name, "InputVoltage", transformator.InputVoltage, plant, loggedMember);
                CheckValue(transformator.Name, "OutputVoltage", transformator.OutputVoltage, plant, loggedMember);
            }
        }

        private void CheckValue(string name, string parameter, AssetParameterData value, PowerPlantDataSetData plant, string loggedMember)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                Error error = new Error()
                {
                    PlantName = plant.PlantName,
                    MachineName = name,
                    Parameter = parameter,
                    ErrorTime = DateTime.Now,
                    LoggedUser = loggedMember,
                    MaxValue = value.MaxValue,
                    MinValue = value.MinValue
                };

                if (loggedMember == null)
                {
                    error.LoggedUser = "N/A";
                }

                _errorService.Add(error);
            }
        }
    }
}
