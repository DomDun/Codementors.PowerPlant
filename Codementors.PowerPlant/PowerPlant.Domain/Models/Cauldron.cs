using PowerPlantCzarnobyl.Domain.DataGenerator;
using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class Cauldron
    {
        public string Name { get; }

        /// <summary>
        /// Water preasure in MegaPascals
        /// </summary>
        public AssetParameter WaterPressure { get; private set; } //ok 12

        /// <summary>
        /// Water temperature in Celcius degrees
        /// </summary>
        public AssetParameter WaterTemperature { get; private set; } //ok 350

        /// <summary>
        /// Camber temperature in Celcius degrees
        /// </summary>
        public AssetParameter CamberTemperature { get; private set; } //ok 400

        internal Cauldron(string name)
        {
            const double MARGIN = 0.15;
            const double WATER_PRESSURE_DEFAULT = 12;
            const double WATER_TEMPERATURE_DEFAULT = 350;
            const double CAMBER_TEMPERATURE_DEFAULT = 400;

            Name = name;

            WaterPressure = new AssetParameter(WATER_PRESSURE_DEFAULT * (1.0d - MARGIN), WATER_PRESSURE_DEFAULT * (1.0d + MARGIN), WATER_PRESSURE_DEFAULT, "MPa");
            PowerPlantDataGenerator.Subscribe(WaterPressure);

            WaterTemperature = new AssetParameter(WATER_TEMPERATURE_DEFAULT * (1.0d - MARGIN), WATER_TEMPERATURE_DEFAULT * (1.0d + MARGIN), WATER_TEMPERATURE_DEFAULT, "C");
            PowerPlantDataGenerator.Subscribe(WaterTemperature);

            CamberTemperature = new AssetParameter(CAMBER_TEMPERATURE_DEFAULT * (1.0d - MARGIN), CAMBER_TEMPERATURE_DEFAULT * (1.0d + MARGIN), CAMBER_TEMPERATURE_DEFAULT, "C");
            PowerPlantDataGenerator.Subscribe(CamberTemperature);
        }
    }
}
