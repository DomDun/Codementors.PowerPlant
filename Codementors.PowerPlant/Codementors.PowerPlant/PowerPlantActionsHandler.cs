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
        private readonly RecievedDataService _recievedDataService;
        private readonly MemberService _memberService;
        private readonly ConsoleManager _consoleManager;
        private readonly InspectionHandler _inspectionHandler;
        private readonly ErrorsHandler _errorsHandler;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();

            var recievedDataRepository = new RecievedDataRepository();
            var membersRepository = new MembersRepository();

            _inspectionHandler = new InspectionHandler();
            _errorsHandler = new ErrorsHandler();
            _consoleManager = new ConsoleManager();
            _recievedDataService = new RecievedDataService(recievedDataRepository);
            _memberService = new MemberService(membersRepository);
            _loginHandler = new LoginHandler(_memberService, _consoleManager, _cliHelper);
        }
        public void ProgramLoop(string loggedMember)
        {
            bool exit = false;
            var loggedUser = _memberService.CheckMemberRole(loggedMember);

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Produced energy \n 5.Export file with errors \n 6.Show all errors \n 7.Add Inspection \n 8.Show inspections by selected dates \n 9.Exit \n");
                
                switch (operation)
                {
                    case "1":
                        CurrentWorkStatus();
                        break;
                    case "2":
                        _loginHandler.AddMember(loggedUser);
                        break;
                    case "3":
                        _loginHandler.DeleteMember(loggedUser);
                        break;
                    case "4":
                        ShowProducedPower();
                        break;
                    case "5":
                        _errorsHandler.ExportErrorsListToJson();
                        break;
                    case "6":
                        _errorsHandler.ShowAllErrors();
                        break;
                    case "7":
                        _inspectionHandler.AddInspection();
                        break;
                    case "8":
                        _inspectionHandler.ShowInspectionsBySelectedDate();
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        _consoleManager.WriteLine("Wrong number, try again");
                        break;
                }
            }
        }

        private void ShowProducedPower()
        {
            _recievedDataService.ActualDataSender();
            _recievedDataService.OnRecieveData += ProducedPower;
            if (_consoleManager.ReadKey().Key == ConsoleKey.Escape)
            {
                _recievedDataService.OnRecieveData -= ProducedPower;
                _consoleManager.Clear();
            }
        }

        private void CurrentWorkStatus()
        {
            _recievedDataService.ActualDataSender();
            _recievedDataService.OnRecieveData += PowerPlantDataArrived;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                _recievedDataService.OnRecieveData -= PowerPlantDataArrived;
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

            Console.WriteLine($"\n power generated  {totalPower * (collectedPower.Count / time)}  MWH");
        }

        private double CalculateProducedPower(string name, AssetParameterData value)
        {
            var producedPower = value.CurrentValue;
            return producedPower;
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