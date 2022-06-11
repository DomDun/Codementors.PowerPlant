using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain
{
    public class InspectionService
    {
        private readonly IInspectionRepository _inspectionRepository;
        public string _loggedUser;

        public InspectionService(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public bool AddInspection(Inspection inspection)
        {
            return _inspectionRepository.AddInspection(inspection);
        }

        public async Task<List<Inspection>> GetInspectionsByDates(DateTime startData, DateTime endData)
        {
            return await _inspectionRepository.GetInspectionsAsync(startData, endData);
        }
    }
}
