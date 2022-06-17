using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
