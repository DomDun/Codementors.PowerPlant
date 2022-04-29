using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class TransformatorData
    {
        public string Name { get; }
        /// <summary>
        /// Input voltage in Volts
        /// </summary>
        public AssetParameterData InputVoltage { get; private set; } //14tys

        /// <summary>
        /// Output voltage in Volts
        /// </summary>
        public AssetParameterData OutputVoltage { get; private set; } //110tys
    }
}
