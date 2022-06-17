using PowerPlantCzarnobyl.WebApi.Client.Clients;
using PowerPlantCzarnobyl.WebApi.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class InspectionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly InspectionWebApiClient _inspectionWebApiClient;
        private readonly RecievedDataWebApiClient _recievedDataWebApiClient;

        public InspectionHandler()
        {
            _cliHelper = new CliHelper();
            _inspectionWebApiClient = new InspectionWebApiClient();
            _recievedDataWebApiClient = new RecievedDataWebApiClient();
        }

        public bool AddInspection(MemberWebApi loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role == "Engineer")
            {
                Console.WriteLine("\nYou are not authorized to add new inspection. Go to Your CEO for a promotion :)\n");
                return false;
            }

            Inspection newInspection = new Inspection();

            newInspection.CreateDate = DateTime.Now;

            var machineNameFromUser = CheckIfMemberCanOpenNewInspectionForThisMachine();
            newInspection.MachineName = machineNameFromUser;

            newInspection.State = State.Open;


            bool success = _inspectionWebApiClient.AddInspection(newInspection).Result;

            string message = success
                ? "\ninspection added successfully\n"
                : "\nError when adding inspection\n";

            Console.WriteLine(message);
            return success;
        }

        private string CheckIfMemberCanOpenNewInspectionForThisMachine()
        {
            List<string> machines = CreateMachinesList();

            string machineName;
            var machineNameIsCorrect = false;

            do
            {
                machineName = _cliHelper.GetStringFromUser("give me machine name You want to check");

                if (machines.Contains(machineName))
                {
                    var inspections = _inspectionWebApiClient.GetAllInspections().Result;
                    Dictionary<string, DateTime?> inspectionsInSystem = new Dictionary<string, DateTime?>();

                    foreach (var inspection in inspections)
                    {
                        inspectionsInSystem.Add(inspection.MachineName, inspection.EndDate);
                    }

                    if (inspectionsInSystem.ContainsKey(machineName))
                    {
                        if(inspectionsInSystem.ContainsValue(null))
                        {
                            Console.WriteLine("There is open inspection for this machine in system, You can't open another one");
                        }
                        else
                        {
                            machineNameIsCorrect = true;
                        }
                    }
                    else
                    {
                        machineNameIsCorrect = true;
                    }    
                }
                else
                {
                    Console.WriteLine("We don't have that machine, please type machine name again");
                }
            }while(!machineNameIsCorrect);
            

            return machineName;
        }

        private List<string> CreateMachinesList()
        {
            PowerPlantDataSet plant = _recievedDataWebApiClient.GetData().Result;
            List<string> machines = new List<string>();

            foreach (var item in plant.Transformators)
            {
                Console.WriteLine($"{item.Name}");
                machines.Add(item.Name);
            }
            foreach (var item in plant.Turbines)
            {
                Console.WriteLine($"{item.Name}");
                machines.Add(item.Name);
            }
            foreach (var item in plant.Cauldrons)
            {
                Console.WriteLine($"{item.Name}");
                machines.Add(item.Name);
            }

            return machines;
        }

        public void ShowAllInspections()
        {
            var inspections = _inspectionWebApiClient.GetAllInspections().Result;

            if (inspections != null)
            {
                foreach (var inspection in inspections)
                {
                    Console.WriteLine($"\nCreate date: {inspection.CreateDate}");
                    Console.WriteLine($"Update date: {inspection.UpdateDate}");
                    Console.WriteLine($"End date: {inspection.EndDate}");
                    Console.WriteLine($"Machine name: {inspection.MachineName}");
                    Console.WriteLine($"Comments: {inspection.Comments}");
                    Console.WriteLine($"State: {inspection.State}\n");
                }
            }
        }
    }
}
