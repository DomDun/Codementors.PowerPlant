using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IErrorsRepository
    {
        void AddError(Error error);
        List<Error> GetAllErrors(DateTime startData, DateTime endData);
    }
}
