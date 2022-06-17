using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
