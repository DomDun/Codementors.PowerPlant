using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain
{
    public class MemberService
    {
        private readonly IMembersRepository _membersRepository;

        public MemberService(IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public bool Add(Member member)
        {
            return _membersRepository.Add(member);
        }
        
        public bool CheckUserCredentials(string login, string password)
        {
            Member member = _membersRepository.GetMember(login);

            return member != null && member.Password == password;
        }

        public Member CheckMemberRole(string login)
        {
            Member member = _membersRepository.GetMember(login);
            return member;
        }

        public bool Delete(string login)
        {
            bool result = _membersRepository.DeleteMember(login);
            return result;
        }
    }
}
