using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    public class MemberManagementClient : ClientBase<IMemberManagementService>
    {
        public Member CheckMemberRole(string loggedMember)
        {
            return base.Channel.CheckMemberRoleAsync(loggedMember);
        }

        public bool LoginAsync(string login, string password)
        {
            return base.Channel.CheckMemberCredentialsAsync(login,password);
        }
    }
}