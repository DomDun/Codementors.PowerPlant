using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class CauldronData
    {
        public string Name { get; set; }

        /// <summary>
        /// Water preasure in MegaPascals
        /// </summary>
        public AssetParameterData WaterPressure { get; set; } //ok 12

        /// <summary>
        /// Water temperature in Celcius degrees
        /// </summary>
        public AssetParameterData WaterTemperature { get; set; } //ok 350

        /// <summary>
        /// Camber temperature in Celcius degrees
        /// </summary>
        public AssetParameterData CamberTemperature { get; set; } //ok 400
    }
}
