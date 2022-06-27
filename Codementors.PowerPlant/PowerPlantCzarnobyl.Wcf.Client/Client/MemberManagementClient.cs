using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    public class MemberManagementClient : ClientBase<IMemberManagementService>
    {
        public MemberWcf CheckMemberRole(string loggedMember)
        {
            return base.Channel.CheckMemberRole(loggedMember);
        }

        public bool LoginAsync(string login, string password)
        {
            return base.Channel.CheckMemberCredentials(login,password);
        }

        public bool AddMember(MemberWcf loggedUser)
        {
            return base.Channel.AddMember(loggedUser);
        }

        public bool DeleteMember(string memberToDelete)
        {
            return base.Channel.DeleteMember(memberToDelete);
        }
    }
}