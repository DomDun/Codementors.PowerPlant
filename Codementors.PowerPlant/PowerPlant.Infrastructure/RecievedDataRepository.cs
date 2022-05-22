using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantDataProvider;
using PowerPlantDataProvider.Models;
using System;
using System.Linq;

namespace PowerPlantCzarnobyl.Infrastructure
{
    public class RecievedDataRepository : IRecievedDataRepository
    {
        public event EventHandler<PowerPlantDataSetData> OnRecievedDataToDomain = null;
        public void Subscribe()
        {
            PowerPlant.Instance.OnNewDataSetArrival += RecieveDataFromLibrary;
        }

        public void RecieveDataFromLibrary(object sender, object plant)
        {
            var recievedData = (PowerPlantDataSet)plant;

            PowerPlantDataSetData plantData = new PowerPlantDataSetData 
            {
                PlantName = recievedData.PlantName,
                Cauldrons = recievedData.Cauldrons
                .Select(c => new CauldronData
                {
                    Name = c.Name,
                    WaterPressure = new AssetParameterData()
                    {
                        MinValue = c.WaterPressure.MinValue,
                        MaxValue = c.WaterPressure.MaxValue,
                        TypicalValue = c.WaterPressure.TypicalValue,
                        CurrentValue = c.WaterPressure.CurrentValue,
                        Unit = c.WaterPressure.Unit
                    },

                    WaterTemperature = new AssetParameterData()
                    {
                        MinValue = c.WaterTemperature.MinValue,
                        MaxValue = c.WaterTemperature.MaxValue,
                        TypicalValue = c.WaterTemperature.TypicalValue,
                        CurrentValue = c.WaterTemperature.CurrentValue,
                        Unit = c.WaterTemperature.Unit
                    },
                    CamberTemperature = new AssetParameterData()
                    {
                        MinValue = c.CamberTemperature.MinValue,
                        MaxValue = c.CamberTemperature.MaxValue,
                        TypicalValue = c.CamberTemperature.TypicalValue,
                        CurrentValue = c.CamberTemperature.CurrentValue,
                        Unit = c.CamberTemperature.Unit
                    }
                })
                .ToArray(),
                Turbines = recievedData.Turbines
                 .Select(t => new TurbineData
                 {
                     Name = t.Name,
                     OverheaterSteamTemperature = new AssetParameterData
                     {
                         MinValue = t.OverheaterSteamTemperature.MinValue,
                         MaxValue = t.OverheaterSteamTemperature.MaxValue,
                         TypicalValue = t.OverheaterSteamTemperature.TypicalValue,
                         CurrentValue = t.OverheaterSteamTemperature.CurrentValue,
                         Unit = t.OverheaterSteamTemperature.Unit
                     },
                     SteamPressure = new AssetParameterData
                     {
                         MinValue = t.SteamPressure.MinValue,
                         MaxValue = t.SteamPressure.MaxValue,
                         TypicalValue = t.SteamPressure.TypicalValue,
                         CurrentValue = t.SteamPressure.CurrentValue,
                         Unit = t.SteamPressure.Unit
                     },
                     RotationSpeed = new AssetParameterData
                     {
                         MinValue = t.RotationSpeed.MinValue,
                         MaxValue = t.RotationSpeed.MaxValue,
                         TypicalValue = t.RotationSpeed.TypicalValue,
                         CurrentValue = t.RotationSpeed.CurrentValue,
                         Unit = t.RotationSpeed.Unit
                     },
                     CurrentPower = new AssetParameterData
                     {
                         MinValue = t.CurrentPower.MinValue,
                         MaxValue = t.CurrentPower.MaxValue,
                         TypicalValue = t.CurrentPower.TypicalValue,
                         CurrentValue = t.CurrentPower.CurrentValue,
                         Unit = t.CurrentPower.Unit
                     },
                     OutputVoltage = new AssetParameterData
                     {
                         MinValue = t.OutputVoltage.MinValue,
                         MaxValue = t.OutputVoltage.MaxValue,
                         TypicalValue = t.OutputVoltage.TypicalValue,
                         CurrentValue = t.OutputVoltage.CurrentValue,
                         Unit = t.OutputVoltage.Unit
                     },
                 })
                .ToArray(),
                Transformators = recievedData.Transformators
                .Select(t => new TransformatorData
                {
                    Name = t.Name,
                    InputVoltage = new AssetParameterData
                    {
                        MinValue = t.InputVoltage.MinValue,
                        MaxValue = t.InputVoltage.MaxValue,
                        TypicalValue = t.InputVoltage.TypicalValue,
                        CurrentValue = t.InputVoltage.CurrentValue,
                        Unit = t.InputVoltage.Unit
                    },
                    OutputVoltage = new AssetParameterData
                    {
                        MinValue = t.OutputVoltage.MinValue,
                        MaxValue = t.OutputVoltage.MaxValue,
                        TypicalValue = t.OutputVoltage.TypicalValue,
                        CurrentValue = t.OutputVoltage.CurrentValue,
                        Unit = t.OutputVoltage.Unit
                    },
                })
                .ToArray()
            };

            if (OnRecievedDataToDomain != null)
            {
                OnRecievedDataToDomain(sender, plantData);
            }
        }
    }
}


