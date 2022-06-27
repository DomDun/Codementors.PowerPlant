using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class TransformatorDataWcf
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public AssetParameterDataWcf InputVoltage { get; set; }
        [DataMember]
        public AssetParameterDataWcf OutputVoltage { get; set; }
    }
}
