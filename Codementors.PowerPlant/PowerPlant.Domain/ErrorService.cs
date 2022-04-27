using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain
{
    public class ErrorService
    {
        private readonly IErrorsRepository _errorsRepository;

        public ErrorService(IErrorsRepository membersRepository)
        {
            _errorsRepository = membersRepository;
        }

        public void Add(Error error)
        {
            _errorsRepository.AddError(error);
        }
    }
}
