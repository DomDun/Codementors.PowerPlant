using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.ServiceDefinitions
{
    [ServiceContract]
    public interface IErrorManagementService
    {
        [OperationContract]
        List<ErrorWcf> GetAllErrors(DateTime startData, DateTime endData);

        [OperationContract]
        Dictionary<string, int> GetAllErrorsInDictionary(DateTime startData, DateTime endData);

        [OperationContract]
        void AddError(ErrorWcf error);
    }
}
