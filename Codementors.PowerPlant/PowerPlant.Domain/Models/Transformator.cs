using PowerPlantCzarnobyl.Domain.DataGenerator;
using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class Transformator
    {
        public string Name { get; }
        /// <summary>
        /// Input voltage in Volts
        /// </summary>
        public AssetParameter InputVoltage { get; private set; } //14tys

        /// <summary>
        /// Output voltage in Volts
        /// </summary>
        public AssetParameter OutputVoltage { get; private set; } //110tys

        internal Transformator(string name)
        {
            const double MARGIN = 0.15;
            const double INPUT_VOLTAGE_DEFAULT = 14000;
            const double OUTPUT_VOLTAGE_DEFAULT = 110000;

            Name = name;

            InputVoltage = new AssetParameter(INPUT_VOLTAGE_DEFAULT * (1.0d - MARGIN), INPUT_VOLTAGE_DEFAULT * (1.0d + MARGIN), INPUT_VOLTAGE_DEFAULT, "V");
            PowerPlantDataGenerator.Subscribe(InputVoltage);

            OutputVoltage = new AssetParameter(OUTPUT_VOLTAGE_DEFAULT * (1.0d - MARGIN), OUTPUT_VOLTAGE_DEFAULT * (1.0d + MARGIN), OUTPUT_VOLTAGE_DEFAULT, "V");
            PowerPlantDataGenerator.Subscribe(OutputVoltage);
        }
    }
}
