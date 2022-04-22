using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IMembersRepository
    {
        bool AddUser(Member user);
        Member GetUser(string username);
    }
}
