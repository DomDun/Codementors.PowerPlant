using PowerPlantCzarnobyl.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IMembersRepository
    {
        bool Add(Member member);
        Member GetMember(string login);
        bool DeleteMember(string login);
        List<Member> GetAllMembers();
    }
}
