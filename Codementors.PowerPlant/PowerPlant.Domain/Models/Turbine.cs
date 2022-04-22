using PowerPlantCzarnobyl.Domain.DataGenerator;
using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class Turbine
    {
        public string Name { get; }

        /// <summary>
        /// Steam temperature in overheater in Celcius Degrees
        /// </summary>
        public AssetParameter OverheaterSteamTemperature { get; } //ok 550 stC

        /// <summary>
        /// Steam preasure in turbine chamber in MegaPascals
        /// </summary>
        public AssetParameter SteamPressure { get; } //90Mpa

        /// <summary>
        /// Turbine rotation speed in Rotation Per Minute
        /// </summary>
        public AssetParameter RotationSpeed { get; } //ok 3000RPM

        /// <summary>
        /// Current power in MegaWatts
        /// </summary>
        public AssetParameter CurrentPower { get; } //100MW

        /// <summary>
        /// Output voltage in Volts
        /// </summary>
        public AssetParameter OutputVoltage { get; } //Ok 14tys

        internal Turbine(string name)
        {
            const double MARGIN = 0.15;
            const double OVERHEATER_STEAM_TEMPERATURE_DEFAULT = 550;
            const double STEAM_PRESSURE_DEFAULT = 90;
            const double ROTATION_SPEED_DEFAULT = 3000;
            const double CURRENT_POWER_DEFAULT = 100;
            const double OUTPUT_VOLTAGE_DEFAULT = 14000;

            Name = name;

            OverheaterSteamTemperature = new AssetParameter(OVERHEATER_STEAM_TEMPERATURE_DEFAULT * (1.0d - MARGIN), OVERHEATER_STEAM_TEMPERATURE_DEFAULT * (1.0d + MARGIN), OVERHEATER_STEAM_TEMPERATURE_DEFAULT, "C");
            PowerPlantDataGenerator.Subscribe(OverheaterSteamTemperature);

            SteamPressure = new AssetParameter(STEAM_PRESSURE_DEFAULT * (1.0d - MARGIN), STEAM_PRESSURE_DEFAULT * (1.0d + MARGIN), STEAM_PRESSURE_DEFAULT, "MPa");
            PowerPlantDataGenerator.Subscribe(SteamPressure);

            RotationSpeed = new AssetParameter(ROTATION_SPEED_DEFAULT * (1.0d - MARGIN), ROTATION_SPEED_DEFAULT * (1.0d + MARGIN), ROTATION_SPEED_DEFAULT, "RPM");
            PowerPlantDataGenerator.Subscribe(RotationSpeed);

            CurrentPower = new AssetParameter(CURRENT_POWER_DEFAULT * (1.0d - MARGIN), CURRENT_POWER_DEFAULT * (1.0d + MARGIN), CURRENT_POWER_DEFAULT, "MW");
            PowerPlantDataGenerator.Subscribe(CurrentPower);

            OutputVoltage = new AssetParameter(OUTPUT_VOLTAGE_DEFAULT * (1.0d - MARGIN), OUTPUT_VOLTAGE_DEFAULT * (1.0d + MARGIN), OUTPUT_VOLTAGE_DEFAULT, "V");
            PowerPlantDataGenerator.Subscribe(OutputVoltage);
        }
    }
}
