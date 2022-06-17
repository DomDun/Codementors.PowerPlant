using System.Runtime.Serialization;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models
{
    [DataContract]
    public class Member
    {
        [DataMember]
        public string Login;
        [DataMember]
        public string Password;
        [DataMember]
        public string Role;
    }
}
