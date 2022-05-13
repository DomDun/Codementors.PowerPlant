using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IMembersRepository
    {
        bool Add(Member member);
        Member GetMember(string login);
        bool DeleteMember(string login);
    }
}
