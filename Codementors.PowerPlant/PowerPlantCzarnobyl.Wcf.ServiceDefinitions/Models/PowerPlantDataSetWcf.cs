using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class PowerPlantDataSetWcf
    {
        [DataMember]
        public string PlantName { get; set; }
        [DataMember]
        public CauldronDataWcf[] Cauldrons { get; set; }
        [DataMember]
        public TurbineDataWcf[] Turbines { get; set; }
        [DataMember]
        public TransformatorDataWcf[] Transformators { get; set; }
    }
}
