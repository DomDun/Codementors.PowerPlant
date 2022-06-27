using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IReceivedDataManagementService
    {
        [OperationContract]
        PowerPlantDataSetWcf GetNewDataSet();
    }
}
