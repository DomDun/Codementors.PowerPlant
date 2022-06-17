using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using System;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    internal class RecievedDataHandler : ClientBase<IRecievedDataManagementService>
    {

        internal void CurrentWorkStatus()
        {
            throw new NotImplementedException();
        }
    }
}