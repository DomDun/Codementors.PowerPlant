using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class CauldronDataWcf
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public AssetParameterDataWcf WaterPressure { get; set; }
        [DataMember]
        public AssetParameterDataWcf WaterTemperature { get; set; }
        [DataMember]
        public AssetParameterDataWcf CamberTemperature { get; set; }
    }
}
