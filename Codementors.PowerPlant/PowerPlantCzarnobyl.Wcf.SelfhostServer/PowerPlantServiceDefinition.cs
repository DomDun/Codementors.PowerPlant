using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.SelfhostServer
{
    public class PowerPlantServiceDefinition : IMemberManagementService
    {
        private readonly MemberService _memberService;
        private readonly Mapper _mapper;


        public PowerPlantServiceDefinition()
        {
            var memberRepository = new MembersRepository();

            _mapper = new Mapper();
            _memberService = new MemberService(memberRepository);
        }
        public void AddMember(Member member)
        {
            _memberService.Add(_mapper.MapToDomainMember(member));
        }

        public bool CheckMemberCredentialsAsync(string username, string password)
        {
            return _memberService.CheckUserCredentials(username, password);
        }

        public Member CheckMemberRoleAsync(string login)
        {
            return _mapper.MapToContractMember(_memberService.CheckMemberRole(login));
        }
    }
}
