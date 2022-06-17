using PowerPlantCzarnobyl.Domain.Models;
using WcfMember = PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models.Member;


namespace PowerPlantCzarnobyl.Wcf.SelfhostServer
{
    public class Mapper
    {
        public Member MapToDomainMember(WcfMember wcfMember)
        {
            return new Member
            {
                Login = wcfMember.Login,
                Password = wcfMember.Password,
                Role = wcfMember.Role
            };
        }

        public WcfMember MapToContractMember(Member member)
        {
            return new WcfMember
            {
                Login = member.Login,
                Password = member.Password,
                Role = member.Role
            };
        }
    }
}
