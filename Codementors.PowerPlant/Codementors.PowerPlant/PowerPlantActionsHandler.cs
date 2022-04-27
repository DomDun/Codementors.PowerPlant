using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;

namespace PowerPlantCzarnobyl
{
    internal class PowerPlantActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly LoginHandler _loginHandler;
        private readonly ErrorService _errorService;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();
            _loginHandler = new LoginHandler();

            var errorRepostiory = new ErrorsRepository();

            _errorService = new ErrorService(errorRepostiory);
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
                        //todo: zliczanie energii
                        ProducedPower();
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

        private static void CurrentWorkStatus()
        {
            PowerPlant.Instance.OnNewDataSetArrival += PowerPlantDataArrived;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                PowerPlant.Instance.OnNewDataSetArrival -= PowerPlantDataArrived;
                Console.Clear();
            }
        }

        private static void PowerPlantDataArrived(object sender, PowerPlantDataSet plant)
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

        private static void PrintValue(string name, AssetParameter value)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                Console.Write("\t" + name + "\t");
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ value.CurrentValue} {value.Unit} it will blow in any moment, we're totally fucked!!!");
                Console.ForegroundColor = defaultColor;
            }
            else
            {
                Console.WriteLine("\t" + name + "\t" + value.CurrentValue + " " + value.Unit);
            }
        }

        private void CatchError(PowerPlantDataSet plant, string name, AssetParameter value )
        {
            Error error = new Error()
            {
                PlantName = plant.PlantName,
                MachineName = name,
                MachineValue = value.CurrentValue,
                Unit = value.Unit,
                errorTime = DateTime.Now,
            };

            _errorService.Add(error);
        }
        private static void ProducedPower()
        {
            Console.Clear();
            var polishUnit = "w ch*j";
            Console.WriteLine($"\nthis plant made {polishUnit} MwH so far\n");
        }
    }
}