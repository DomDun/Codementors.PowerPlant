using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class TurbineData
    {
        public string Name { get; }

        /// <summary>
        /// Steam temperature in overheater in Celcius Degrees
        /// </summary>
        public AssetParameterData OverheaterSteamTemperature { get; } //ok 550 stC

        /// <summary>
        /// Steam preasure in turbine chamber in MegaPascals
        /// </summary>
        public AssetParameterData SteamPressure { get; } //90Mpa

        /// <summary>
        /// Turbine rotation speed in Rotation Per Minute
        /// </summary>
        public AssetParameterData RotationSpeed { get; } //ok 3000RPM

        /// <summary>
        /// Current power in MegaWatts
        /// </summary>
        public AssetParameterData CurrentPower { get; } //100MW

        /// <summary>
        /// Output voltage in Volts
        /// </summary>
        public AssetParameterData OutputVoltage { get; } //Ok 14tys
    }
}
