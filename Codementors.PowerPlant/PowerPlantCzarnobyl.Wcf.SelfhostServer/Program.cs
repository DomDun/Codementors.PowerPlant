using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PowerPlantCzarnobyl.Wcf.SelfhostServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create a URI to serve as the base address
            Uri httpUrl = new Uri("http://localhost:1992/PowerPlantCzarnobyl");

            //Create ServiceHost
            ServiceHost host = new ServiceHost(typeof(PowerPlantServiceDefinition), httpUrl);

            //Add a service endpoint
            var binding = new WSHttpBinding();
            host.AddServiceEndpoint(typeof(IMemberManagementService), binding, "Members");
            host.AddServiceEndpoint(typeof(IReceivedDataManagementService), binding, "ReceivedData");
            host.AddServiceEndpoint(typeof(IErrorManagementService), binding, "Errors");
            host.AddServiceEndpoint(typeof(IInspectionManagementService), binding, "Inspections");

            var receivedDataRepository = new ReceivedDataRepository();
            ReceivedDataService.Instance = new ReceivedDataService(receivedDataRepository);
            ReceivedDataService.Instance.ActualDataSender();

            //Enable metadata exchange
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            //Start the Service
            host.Open();
            Console.WriteLine("Service is host at " + DateTime.Now.ToString());
            Console.WriteLine("Host is running... Press  key to stop");
            Console.ReadLine();
        }
    }
}
