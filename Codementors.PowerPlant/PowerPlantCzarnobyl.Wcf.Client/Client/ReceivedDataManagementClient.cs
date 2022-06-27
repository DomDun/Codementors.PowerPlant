using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    public class ReceivedDataManagementClient : ClientBase<IReceivedDataManagementService>
    {
        public PowerPlantDataSetWcf GetNewDataSet()
        {
            return base.Channel.GetNewDataSet();
        }
    }
}