using PowerPlantCzarnobyl.Wcf.Client.Handler;
using System;

namespace PowerPlantCzarnobyl.Wcf.Client
{
    public class PowerPlantActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly MemberManagementClient _memberManagementClient;
        private readonly MemberHandler _memberHandler;
        private readonly ErrorHandler _errorHandler;
        private readonly InspectionHandler _inspectionHandler;
        private readonly RecievedDataHandler _recievedDataHandler;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();
            _memberManagementClient = new MemberManagementClient();
            _memberHandler = new MemberHandler();
            _errorHandler = new ErrorHandler();
            _inspectionHandler = new InspectionHandler();
            _recievedDataHandler = new RecievedDataHandler();
        }
        public void ProgramLoop(string loggedMember)
        {
            bool exit = false;
            var loggedUser = _memberManagementClient.CheckMemberRole(loggedMember);

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Show all errors (yyyy/MM/dd:GHH:mm) \n 5.anomaly statistics \n 6.Add Inspection \n 7.Show all inspections \n 8.Assign Engineer to inspection \n 9.Show assigned inspections \n 10.Exit \n");

                switch (operation)
                {
                    case "1":
                        _recievedDataHandler.CurrentWorkStatus();
                        break;
                    case "2":
                        _memberHandler.AddMember(loggedUser);
                        break;
                    case "3":
                        _memberHandler.DeleteMember(loggedUser);
                        break;
                    case "4":
                        _errorHandler.ShowAllErrors();
                        break;
                    case "5":
                        _errorHandler.ShowErrorsStats();
                        break;
                    case "6":
                        _inspectionHandler.AddInspection(loggedUser);
                        break;
                    case "7":
                        _inspectionHandler.ShowAllInspections();
                        break;
                    case "8":
                        _inspectionHandler.AssignEngineerToInspection(loggedUser);
                        break;
                    case "9":
                        _inspectionHandler.ShowAssignedInspections(loggedUser);
                        break;
                    case "10":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }
        }

        
    }
}
