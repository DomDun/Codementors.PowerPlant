using Microsoft.Owin.Hosting;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using System;

namespace PowerPlantCzarnobyl.WebApi.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = @"http://localhost:1992/";

            var recievedDataRepository = new ReceivedDataRepository();
            ReceivedDataService.Instance = new ReceivedDataService(recievedDataRepository);
            ReceivedDataService.Instance.ActualDataSender();

            using (WebApp.Start<StartUp>(baseAddress))
            {
                Console.WriteLine("API Started");
                Console.ReadKey();
            }
        }
    }
}
