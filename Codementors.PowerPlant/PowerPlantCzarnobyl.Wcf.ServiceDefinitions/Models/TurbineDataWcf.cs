using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
