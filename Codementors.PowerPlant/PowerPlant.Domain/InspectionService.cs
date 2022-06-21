using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IInspectionService
    {
        Task<bool> AddInspectionAsync(Inspection inspection);
        List<Inspection> GetAllInspections();
        Inspection GetInspection(int id);
        bool UpdateInspection(int id, Inspection inspection);
    }

    public class InspectionService : IInspectionService
    {
        private readonly IInspectionRepository _inspectionRepository;
        public string _loggedUser;

        public InspectionService(IInspectionRepository inspectionRepository)
        {
            _inspectionRepository = inspectionRepository;
        }

        public async Task<bool> AddInspectionAsync(Inspection inspection)
        {
            return await _inspectionRepository.AddInspectionAsync(inspection);
        }

        public List<Inspection> GetAllInspections()
        {
            return _inspectionRepository.GetAllInspections();
        }

        public Inspection GetInspection(int id)
        {
            return _inspectionRepository.GetInspection(id);
        }

        public bool UpdateInspection(int id, Inspection inspection)
        {
            return _inspectionRepository.UpdateInspection(id, inspection);
        }
    }
}
