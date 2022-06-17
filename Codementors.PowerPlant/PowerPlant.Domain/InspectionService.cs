using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IInspectionService
    {
        Task<bool> AddInspection(Inspection inspection);
        Task<List<Inspection>> GetAllInspections();
    }

    public class InspectionService : IInspectionService
    {
        private readonly IInspectionRepository _inspectionRepository;
        public string _loggedUser;

        public InspectionService(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public async Task<bool> AddInspection(Inspection inspection)
        {
            return await _inspectionRepository.AddInspection(inspection);
        }

        public async Task<List<Inspection>> GetAllInspections()
        {
            return await _inspectionRepository.GetAllInspectionsAsync();
        }
    }
}
