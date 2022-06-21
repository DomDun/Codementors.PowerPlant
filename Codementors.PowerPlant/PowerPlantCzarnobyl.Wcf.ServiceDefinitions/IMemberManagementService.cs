using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IMemberManagementService
    {
        [OperationContract]
        bool AddMember(MemberWcf member);

        [OperationContract]
        MemberWcf CheckMemberRole(string login);

        [OperationContract]
        bool CheckMemberCredentials(string username, string password);

        [OperationContract]
        bool DeleteMember(string member);
    }
}
