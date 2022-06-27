using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class TurbineDataWcf
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public AssetParameterDataWcf OverheaterSteamTemperature { get; set; }
        [DataMember]
        public AssetParameterDataWcf SteamPressure { get; set; }
        [DataMember]
        public AssetParameterDataWcf RotationSpeed { get; set; }
        [DataMember]
        public AssetParameterDataWcf CurrentPower { get; set; }
        [DataMember]
        public AssetParameterDataWcf OutputVoltage { get; set; }
    }
}
