using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class InspectionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly InspectionWebApiClient _inspectionWebApiClient;

        public InspectionHandler()
        {
            _cliHelper = new CliHelper();
            _inspectionWebApiClient = new InspectionWebApiClient();
        }

        public bool AddInspection()
        {
            Console.Clear();

            Inspection inspection = _cliHelper.GetInspectionFromUser();

            bool success = _inspectionWebApiClient.AddInspection(inspection).Result;

            string message = success
                ? "\ninspection added successfully\n"
                : "\nError when adding inspection\n";

            Console.WriteLine(message);
            return success;
        }

        public void ShowInspectionsBySelectedDate()
        {
            var startDate = _cliHelper.GetDateFromUser("enter start date");
            var endDate = _cliHelper.GetDateFromUser("enter end date");

            var inspections = _inspectionWebApiClient.GetAllInspections(startDate, endDate).Result;

            if (inspections != null)
            {
                foreach (var inspection in inspections)
                {
                    Console.WriteLine($"Create date: {inspection.CreateDate}");
                    Console.WriteLine($"Update date: {inspection.UpdateDate}");
                    Console.WriteLine($"End date: {inspection.EndDate}");
                    Console.WriteLine($"Machine name: {inspection.Name}");
                    Console.WriteLine($"Comments: {inspection.Comments}");
                }
            }
        }
    }
}
