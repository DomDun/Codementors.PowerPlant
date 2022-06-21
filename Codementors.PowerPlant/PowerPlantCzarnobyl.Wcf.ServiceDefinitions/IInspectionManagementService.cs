using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IInspectionManagementService
    {
        [OperationContract]
        Task<bool> AddInspectionAsync(InspectionWcf inspection);

        [OperationContract]
        List<InspectionWcf> GetAllInspections();

        [OperationContract]
        InspectionWcf GetInspection(int id);

        [OperationContract]
        bool UpdateInspection(int id, InspectionWcf inspection);
    }
}
