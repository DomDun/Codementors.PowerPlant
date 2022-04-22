using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain
{
    public class MemberService
    {
        private IMembersRepository _usersRepository;

        public MemberService(IMembersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public bool Add(Member user)
        {
            return _usersRepository.AddUser(user);
        }

        public bool CheckUserCredentials(string username, string password)
        {
            Member user = _usersRepository.GetUser(username);

            return user != null && user.Password == password;
        }
    }
}
