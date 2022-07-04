using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IMembersService
    {
        bool Add(Member member);
        bool CheckUserCredentials(string login, string password);
        Member CheckMemberRole(string login);
        bool Delete(string login);
        List<Member> GetAllMembers();
    }

    public class MemberService : IMembersService
    {
        private readonly IMembersRepository _membersRepository;

        public MemberService(IMembersRepository membersRepository)
        {
            _membersRepository = membersRepository;
        }

        public List<Member> GetAllMembers()
        {
            return _membersRepository.GetAllMembers();
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

        public object GetMember(string login)
        {
            return _membersRepository.GetMember(login);
        }
    }
}
