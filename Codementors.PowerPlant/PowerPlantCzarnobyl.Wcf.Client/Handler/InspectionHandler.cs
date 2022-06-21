using PowerPlantCzarnobyl.Wcf.Client.Client;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.Client.Handler
{
    public class InspectionHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly InspectionManagementClient _inspectionManagementClient;
        private readonly ReceivedDataManagementClient _receivedDataManagementClient;

        public InspectionHandler()
        {
            _cliHelper = new CliHelper();
            _inspectionManagementClient = new InspectionManagementClient();
            _receivedDataManagementClient = new ReceivedDataManagementClient();
        }

        public async Task<bool> AddInspection(MemberWcf loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role == "Engineer")
            {
                Console.WriteLine("\nYou are not authorized to add new inspection. Go to Your CEO for a promotion :)\n");
                return false;
            }

            InspectionWcf newInspection = new InspectionWcf();

            newInspection.CreateDate = DateTime.Now;

            var machineNameFromUser = CheckIfMemberCanOpenNewInspectionForThisMachine();
            newInspection.MachineName = machineNameFromUser;

            newInspection.State = State.Open;


            bool success = await _inspectionManagementClient.AddInspectionAsync(newInspection);

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
                    var inspections = _inspectionManagementClient.GetAllInspections();
                    Dictionary<string, DateTime?> inspectionsInSystem = new Dictionary<string, DateTime?>();

                    foreach (var inspection in inspections)
                    {
                        inspectionsInSystem.Add(inspection.MachineName, inspection.EndDate);
                    }

                    if (inspectionsInSystem.ContainsKey(machineName))
                    {
                        if (inspectionsInSystem.ContainsValue(null))
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
            } while (!machineNameIsCorrect);


            return machineName;
        }

        private List<string> CreateMachinesList()
        {
            PowerPlantDataSetWcf plant = _receivedDataManagementClient.GetNewDataSet();
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
            Console.Clear();
            var inspections = _inspectionManagementClient.GetAllInspections();

            if (inspections != null)
            {
                foreach (var inspection in inspections)
                {
                    Console.WriteLine($"\nInspection Id: {inspection.Id}");
                    Console.WriteLine($"Create date: {inspection.CreateDate}");
                    Console.WriteLine($"Update date: {inspection.UpdateDate}");
                    Console.WriteLine($"End date: {inspection.EndDate}");
                    Console.WriteLine($"Machine name: {inspection.MachineName}");
                    Console.WriteLine($"Comments: {inspection.Comments}");
                    Console.WriteLine($"State: {inspection.State}");
                    Console.WriteLine($"Processing Engineer: {inspection.Engineer}\n");
                }
            }
        }

        private List<InspectionWcf> GetAssignedInspections(MemberWcf loggedUser)
        {
            Console.Clear();
            var inspections = _inspectionManagementClient.GetAllInspections();
            List<InspectionWcf> result = new List<InspectionWcf>();
            if (loggedUser.Role == "Engineer")
            {
                result = inspections
                    .Where(i => i.Engineer == loggedUser.Login)
                    .ToList();
                return result;
            }
            else
            {
                result = inspections
                    .Where(i => i.Engineer != null)
                    .ToList();
                return result;
            }
        }

        public void ShowAssignedInspections(MemberWcf loggedUser)
        {
            Console.Clear();
            var inspections = GetAssignedInspections(loggedUser);

            foreach (var inspection in inspections)
            {
                Console.WriteLine($"\nInspection Id: {inspection.Id}");
                Console.WriteLine($"Create date: {inspection.CreateDate}");
                Console.WriteLine($"Update date: {inspection.UpdateDate}");
                Console.WriteLine($"End date: {inspection.EndDate}");
                Console.WriteLine($"Machine name: {inspection.MachineName}");
                Console.WriteLine($"Comments: {inspection.Comments}");
                Console.WriteLine($"State: {inspection.State}");
                Console.WriteLine($"Processing Engineer: {inspection.Engineer}\n");
            }
        }
        public bool AssignEngineerToInspection(MemberWcf loggedUser)
        {
            Console.Clear();

            if (loggedUser.Role != "Engineer")
            {
                Console.WriteLine("\nYou are not Engineer! Go to Your part of work!!!\n");
                return false;
            }
            ShowAllInspections();

            var id = _cliHelper.GetIntFromUser("Type Id of inspection You want to be assigned");
            InspectionWcf inspection = GetInspection(id);

            inspection.UpdateDate = GetUpdateDate(inspection.CreateDate);
            inspection.Comments = $"Comment by {loggedUser.Login}:    " + _cliHelper.GetStringFromUser("Type Your comment here");
            inspection.State = State.InProgress;
            inspection.Engineer = loggedUser.Login;

            bool success = _inspectionManagementClient.UpdateInspection(id, inspection);

            string message = success
                ? "\nInspection assigned successfully\n"
                : "\nError\n";

            Console.WriteLine(message);

            return success;
        }

        private InspectionWcf GetInspection(int id)
        {
            return _inspectionManagementClient.GetInspection(id);
        }

        private DateTime? GetUpdateDate(DateTime createDate)
        {
            DateTime? updateDate = null;
            do
            {
                updateDate = _cliHelper.GetDateFromUser("Type update date (yyyy/MM/dd:GHH:mm)");
                if (updateDate < createDate)
                {
                    Console.WriteLine("update date have to be later or same as create date");
                }
            } while (updateDate < createDate);

            return updateDate;
        }
    }
}
