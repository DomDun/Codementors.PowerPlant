using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IInspectionRepository
    {
        Task<bool> AddInspectionAsync(Inspection inspection);
        List<Inspection> GetAllInspections();
        Inspection GetInspection(int id);
        bool UpdateInspection(int id, Inspection inspection);
    }
}
