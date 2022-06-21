using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    internal class RecievedDataHandler
    {
        private readonly RecievedDataWebApiClient _recievedDataWebApiClient;
        private readonly ErrorsHandler _errorsHandler;

        public RecievedDataHandler()
        {
            _recievedDataWebApiClient = new RecievedDataWebApiClient();
            _errorsHandler = new ErrorsHandler();
        }

        public void StartWork()
        {
            var timer = new Timer(1000);
            timer.Elapsed += SearchForErrorInDataFromPlant;
            timer.Interval = 1000;
            timer.Start();
        }

        public void SearchForErrorInDataFromPlant(object sender, ElapsedEventArgs cos)
        {
            PowerPlantDataSet plant = _recievedDataWebApiClient.GetData().Result;

            _errorsHandler.CheckIfMachinesWorkCorrectly(sender, plant);
        }

        public void CurrentWorkStatus()
        {
            var timer = new Timer(1000);
            timer.Elapsed += ShowDataFromPlant;
            timer.Interval = 1000;
            timer.Start();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape);

            timer.Stop();
            Console.Clear();
            return;
        }

        public void ShowDataFromPlant(object sender, ElapsedEventArgs cos)
        {
            PowerPlantDataSet plant = _recievedDataWebApiClient.GetData().Result;

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
