using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IMemberManagementService
    {
        [OperationContract]
        void AddMember(Member member);

        [OperationContract]
        Member CheckMemberRoleAsync(string login);

        [OperationContract]
        bool CheckMemberCredentialsAsync(string username, string password);
        
        //todo: dodać usuwanie użytkownika
    }
}
