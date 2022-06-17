using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IRecievedDataManagementService
    {
        [OperationContract]
        void ActualDataSender();
        [OperationContract]
        PowerPlantDataSetWcf GetNewDataSet();
        [OperationContract]
        void RecievedDataSender(object sender, PowerPlantDataSetWcf plant);
    }
}
