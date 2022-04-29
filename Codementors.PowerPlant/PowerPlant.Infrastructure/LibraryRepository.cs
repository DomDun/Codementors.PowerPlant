using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantDataProvider;
using PowerPlantDataProvider.Models;
using System.Linq;

namespace PowerPlantCzarnobyl.Infrastructure
{
    public class LibraryRepository
    {
        public void Subscribe()
        {
            PowerPlant.Instance.OnNewDataSetArrival += RecieveDataFromLibrary;
        }

        public void RecieveDataFromLibrary(object sender, PowerPlantDataSet dataSet)
        {

            Cauldrons = dataSet.Cauldrons
             .Select(cauldron => new CauldronData
             {
                 Name = cauldron.Name,
                 WaterPressure = new AssetParameterData
                 {
                     MinValue = cauldron.WaterPressure.MinValue,
                     MaxValue = cauldron.WaterPressure.MaxValue,
                     TypicalValue = cauldron.WaterPressure.TypicalValue,
                     CurrentValue = cauldron.WaterPressure.CurrentValue,
                     Unit = cauldron.WaterPressure.Unit
                 },
                 WaterTemperature = new AssetParameterData
                 {
                     MinValue = cauldron.WaterTemperature.MinValue,
                     MaxValue = cauldron.WaterTemperature.MaxValue,
                     TypicalValue = cauldron.WaterTemperature.TypicalValue,
                     CurrentValue = cauldron.WaterTemperature.CurrentValue,
                     Unit = cauldron.WaterTemperature.Unit
                 },
                 CamberTemperature = new AssetParameterData
                 {
                     MinValue = cauldron.CamberTemperature.MinValue,
                     MaxValue = cauldron.CamberTemperature.MaxValue,
                     TypicalValue = cauldron.CamberTemperature.TypicalValue,
                     CurrentValue = cauldron.CamberTemperature.CurrentValue,
                     Unit = cauldron.CamberTemperature.Unit
                 }
             })
             .ToArray();
                
           

        }

    }
}
