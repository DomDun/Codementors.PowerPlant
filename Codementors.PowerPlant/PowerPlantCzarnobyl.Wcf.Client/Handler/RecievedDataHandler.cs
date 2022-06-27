using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Timers;

namespace PowerPlantCzarnobyl.Wcf.Client.Handler
{
    public class RecievedDataHandler
    {
        private readonly ReceivedDataManagementClient _receivedDataManagementClient;
        private readonly ErrorHandler _errorHandler;

        public RecievedDataHandler()
        {
            _receivedDataManagementClient = new ReceivedDataManagementClient();
            _errorHandler = new ErrorHandler();
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
            PowerPlantDataSetWcf plant = _receivedDataManagementClient.GetNewDataSet();

            _errorHandler.CheckIfMachinesWorkCorrectly(sender, plant);
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
            PowerPlantDataSetWcf plant = _receivedDataManagementClient.GetNewDataSet();

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

        private static void PrintValue(string name, AssetParameterDataWcf value)
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

        //public void CurrentWorkStatus()
        //{
        //    var timer = new Timer(1000);
        //    timer.Elapsed += RecieveDataFromService;
        //    timer.Interval = 1000;
        //    timer.Start();

        //    ConsoleKeyInfo key;
        //    do
        //    {
        //        key = Console.ReadKey();
        //    } while (key.Key != ConsoleKey.Escape);

        //    timer.Stop();
        //    Console.Clear();
        //    return;
        //}

        //public void RecieveDataFromService(object sender, ElapsedEventArgs cos)
        //{
        //    PowerPlantDataSetWcf plant = _recievedDataManagementClient.GetNewDataSet();

        //    Console.Clear();

        //    Console.WriteLine("Press the Escape (Esc) key to quit: \n");

        //    Console.WriteLine(plant.PlantName + " " + DateTime.Now.ToString("O"));
        //    foreach (var cauldron in plant.Cauldrons)
        //    {
        //        Console.WriteLine(cauldron.Name);
        //        PrintValue("WaterPressure", cauldron.WaterPressure);
        //        PrintValue("WaterTemperature", cauldron.WaterTemperature);
        //        PrintValue("CamberTemperature", cauldron.CamberTemperature);
        //    }

        //    foreach (var turbine in plant.Turbines)
        //    {
        //        Console.WriteLine(turbine.Name);
        //        PrintValue("SteamPressure", turbine.SteamPressure);
        //        PrintValue("OverheaterSteamTemperature", turbine.OverheaterSteamTemperature);
        //        PrintValue("OutputVoltage", turbine.OutputVoltage);
        //        PrintValue("RotationSpeed", turbine.RotationSpeed);
        //        PrintValue("CurrentPower", turbine.CurrentPower);
        //    }

        //    foreach (var transformator in plant.Transformators)
        //    {
        //        Console.WriteLine(transformator.Name);
        //        PrintValue("InputVoltage", transformator.InputVoltage);
        //        PrintValue("OutputVoltage", transformator.OutputVoltage);
        //    }
        //}

        //private void PrintValue(string name, AssetParameterDataWcf value)
        //{
        //    if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
        //    {
        //        Console.Write("\t" + name + "\t");
        //        var defaultColor = Console.ForegroundColor;
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine($"{value.CurrentValue} {value.Unit} it will blow in any moment, we're totally fucked!!!");
        //        Console.ForegroundColor = defaultColor;
        //    }
        //    else
        //    {
        //        Console.WriteLine("\t" + name + "\t" + value.CurrentValue + " " + value.Unit);
        //    }
        //}
    }
}
