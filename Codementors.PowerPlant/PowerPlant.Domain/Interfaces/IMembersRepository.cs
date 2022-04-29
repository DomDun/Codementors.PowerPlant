using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IMembersRepository
    {
        bool AddMember(Member member);
        Member GetMember(string login);
        bool DeleteMember(string login);
    }
}
