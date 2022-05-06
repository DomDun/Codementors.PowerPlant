using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.Collections.Generic;

namespace PowerPlantCzarnobyl
{
    internal class PowerPlantActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly LoginHandler _loginHandler;
        private readonly LibraryService _libraryService;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();
            _loginHandler = new LoginHandler();

            var libraryRepository = new LibraryRepository();

            _libraryService = new LibraryService(libraryRepository);
        }
        public void ProgramLoop(string loggedMember)
        {
            bool exit = false;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Produced energy \n 5.Exit \n");

                switch (operation)
                {
                    case "1":
                        CurrentWorkStatus();
                        break;
                    case "2":
                        _loginHandler.AddMember(loggedMember);
                        break;
                    case "3":
                        _loginHandler.DeleteMember(loggedMember);
                        break;
                    case "4":
                        ShowProducedPower();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }
        }

        private void ShowProducedPower()
        {
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += ProducedPower;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                _libraryService.OnRecieveData -= ProducedPower;
                Console.Clear();
            }
        }

        List<double> collectedPower = new List<double>();
        private void ProducedPower(object sender, PowerPlantDataSetData plant)
        {
            Console.Clear();
            
            double currentTurbinePower = 0;
            double totalPower = 0;
            foreach (var turbine in plant.Turbines)
            {
                Console.WriteLine(turbine.Name);
                PrintValue("InputVoltage", turbine.CurrentPower);
                currentTurbinePower = CalculateProducedPower("CurrentPower", turbine.CurrentPower);
                collectedPower.Add(currentTurbinePower);
            }

            collectedPower
                .ForEach(item =>
                {
                    totalPower += currentTurbinePower;
                });
            double time = 7200;
            Console.WriteLine($"\n power generated  {totalPower * (collectedPower.Count/time)}  MWH");
        }

        private double CalculateProducedPower(string name, AssetParameterData value)
        {
            var producedPower = value.CurrentValue;
            return producedPower;
        }

        private void CurrentWorkStatus()
        {
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += PowerPlantDataArrived;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                _libraryService.OnRecieveData -= PowerPlantDataArrived;
                Console.Clear();
            }
        }

        private void PowerPlantDataArrived(object sender, PowerPlantDataSetData plant)
        {
            Console.Clear();

            Console.WriteLine("Press the Escape (Esc) key to quit: \n");

            Console.WriteLine(plant.PlantName + " " + DateTime.Now.ToString("O"));
            foreach (var cauldron in plant.Cauldrons)
            {
                Console.WriteLine(cauldron.Name);
                PrintValue("WaterPressure", cauldron.WaterPressure);
                PrintValue("WaterTemperature", cauldron.WaterTemperature);
                PrintValue("CamberTemperature", cauldron.CamberTemperature);
            }

            foreach (var turbine in plant.Turbines)
            {
                Console.WriteLine(turbine.Name);
                PrintValue("SteamPressure", turbine.SteamPressure);
                PrintValue("OverheaterSteamTemperature", turbine.OverheaterSteamTemperature);
                PrintValue("OutputVoltage", turbine.OutputVoltage);
                PrintValue("RotationSpeed", turbine.RotationSpeed);
                PrintValue("CurrentPower", turbine.CurrentPower);
            }

            foreach (var transformator in plant.Transformators)
            {
                Console.WriteLine(transformator.Name);
                PrintValue("InputVoltage", transformator.InputVoltage);
                PrintValue("OutputVoltage", transformator.OutputVoltage);
            }
        }

        private static void PrintValue(string name, AssetParameterData value)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                Console.Write("\t" + name + "\t");
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{value.CurrentValue} {value.Unit} it will blow in any moment, we're totally fucked!!!");
                Console.ForegroundColor = defaultColor;
            }
            else
            {
                Console.WriteLine("\t" + name + "\t" + value.CurrentValue + " " + value.Unit);
            }
        }
    }
}