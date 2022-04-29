using PowerPlantCzarnobyl.Domain.Models;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IErrorsRepository
    {
        void AddError(Error error);
    }
}
