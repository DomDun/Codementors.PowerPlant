using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IInspectionRepository
    {
        Task<bool> AddInspection(Inspection inspection);
        Task<List<Inspection>> GetAllInspectionsAsync();
    }
}
