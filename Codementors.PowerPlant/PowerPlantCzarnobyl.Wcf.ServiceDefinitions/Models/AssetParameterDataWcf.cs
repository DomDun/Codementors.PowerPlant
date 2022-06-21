using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class AssetParameterDataWcf
    {
        [DataMember]
        public double MinValue { get; set; }
        [DataMember]
        public double MaxValue { get; set; }
        [DataMember]
        public double TypicalValue { get; set; }
        [DataMember]
        public double CurrentValue { get; set; }
        [DataMember]
        public string Unit { get; set; }
    }
}
