using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.Client.Client
{
    public class InspectionManagementClient : ClientBase<IInspectionManagementService>
    {
        public Task<bool> AddInspectionAsync(InspectionWcf inspection)
        {
            return base.Channel.AddInspectionAsync(inspection);
        }

        public List<InspectionWcf> GetAllInspections()
        {
            return Channel.GetAllInspections();
        }

        public InspectionWcf GetInspection(int id)
        {
            return base.Channel.GetInspection(id);
        }

        public bool UpdateInspection(int id, InspectionWcf inspection)
        {
            return base.Channel.UpdateInspection(id, inspection);
        }
    }
}
