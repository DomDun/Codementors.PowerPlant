using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    public class ErrorManagementClient : ClientBase<IErrorManagementService>
    {
        public List<ErrorWcf> GetAllErrors(DateTime startDate, DateTime endDate)
        {
            return Channel.GetAllErrors(startDate, endDate);
        }

        public Dictionary<string,int> GetAllErrorsInDictionary(DateTime startDate, DateTime endDate)
        {
            return base.Channel.GetAllErrorsInDictionary(startDate, endDate);
        }

        public void AddError(ErrorWcf error)
        {
            Channel.AddError(error);
        }
    }
}