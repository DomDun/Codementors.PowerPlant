//using PowerPlantDataProvider;
//using PowerPlantDataProvider.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PowerPlantCzarnobyl.Infrastructure
//{
//    public class LibraryRepository
//    {
//        public void Subscribe()
//        {
//            PowerPlant.Instance.OnNewDataSetArrival += RecieveDataFromLibrary;
//        }

//        public void RecieveDataFromLibrary(object sender, PowerPlantDataSet dataSet)
//        {

//            Cauldrons = dataSet.Cauldrons
//             .Select(cauldron => new Cauldron
//             {
//                 Name = cauldron.Name,
//                 WaterPressure = new AssetParameter
//                 {
//                     MinValue = cauldron.WaterPressure.MinValue,
//                     MaxValue = cauldron.WaterPressure.MaxValue,
//                     TypicalValue = cauldron.WaterPressure.TypicalValue,
//                     CurrentValue = cauldron.WaterPressure.CurrentValue,
//                     Unit = cauldron.WaterPressure.Unit
//                 },
//                 WaterTemperature = new AssetParameter
//                 {
//                     MinValue = cauldron.WaterTemperature.MinValue,
//                     MaxValue = cauldron.WaterTemperature.MaxValue,
//                     TypicalValue = cauldron.WaterTemperature.TypicalValue,
//                     CurrentValue = cauldron.WaterTemperature.CurrentValue,
//                     Unit = cauldron.WaterTemperature.Unit
//                 },
//                 CamberTemperature = new AssetParameter
//                 {
//                     MinValue = cauldron.CamberTemperature.MinValue,
//                     MaxValue = cauldron.CamberTemperature.MaxValue,
//                     TypicalValue = cauldron.CamberTemperature.TypicalValue,
//                     CurrentValue = cauldron.CamberTemperature.CurrentValue,
//                     Unit = cauldron.CamberTemperature.Unit
//                 }
//             })
//             .ToArray(),
                
           

//        }

//    }
//}
