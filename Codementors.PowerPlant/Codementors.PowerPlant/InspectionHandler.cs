using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;

namespace PowerPlantCzarnobyl
{
    public class InspectionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly InspectionService _inspectionService;

        public InspectionHandler()
        {
            _cliHelper = new CliHelper();

            var inspectionRepository = new InspectionRepository();
            _inspectionService = new InspectionService(inspectionRepository);
        }

        public bool AddInspection()
        {
            Console.Clear();

            Inspection inspection = _cliHelper.GetInspectionFromUser();

            bool success = _inspectionService.AddInspection(inspection);

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

            var inspections = _inspectionService.GetInspectionsByDates(startDate, endDate).Result;

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
