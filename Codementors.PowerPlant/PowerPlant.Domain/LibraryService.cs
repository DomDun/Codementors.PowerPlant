using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;

namespace PowerPlantCzarnobyl.Domain
{

    public class LibraryService
    {
        public event EventHandler<PowerPlantDataSetData> OnRecieveData = null;

        public ILibraryRepository _libraryRepository;

        public LibraryService(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public void ActualDataSender()
        {
            _libraryRepository.Subscribe();
            _libraryRepository.OnRecievedDataToDomain += RecievedDataSender;
        }

        public void RecievedDataSender(object sender, PowerPlantDataSetData plant)
        {
            if (OnRecieveData != null)
            {
                OnRecieveData(sender, plant);
            }
        }

        List<double> collectedPower = new List<double>();
        public void ProducedPower(object sender, PowerPlantDataSetData plant)
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

        public void PowerPlantDataArrived(object sender, PowerPlantDataSetData plant)
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
