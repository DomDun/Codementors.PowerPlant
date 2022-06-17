using PowerPlantCzarnobyl.WebApi.Client.Clients;
using System;

namespace PowerPlantCzarnobyl.WebApi.Client
{
    public class PowerPlantActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly MemberWebApiClient _memberWebApiClient;
        private readonly MemberHandler _memberHandler;
        private readonly RecievedDataHandler _recievedHandler;
        private readonly ErrorsHandler _errorsHandler;
        private readonly InspectionHandler _inspectionHandler;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();
            _memberWebApiClient = new MemberWebApiClient();
            _memberHandler = new MemberHandler();
            _errorsHandler = new ErrorsHandler();
            _inspectionHandler = new InspectionHandler();
            _recievedHandler = new RecievedDataHandler();
        }
        public void ProgramLoop(string loggedMember)
        {
            bool exit = false;
            var loggedUser = _memberWebApiClient.CheckMemberRole(loggedMember).Result;

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Show all errors (yyyy/MM/dd:GHH:mm) \n 5.anomaly statistics \n 6.Add Inspection \n 7.Show all inspections \n 8.Exit \n");

                switch (operation)
                {
                    case "1":
                        _recievedHandler.CurrentWorkStatus();
                        break;
                    case "2":
                        _memberHandler.AddMember(loggedUser);
                        break;
                    case "3":
                        _memberHandler.DeleteMember(loggedUser);
                        break;
                    case "4":
                        _errorsHandler.ShowAllErrors();
                        break;
                    case "5":
                        _errorsHandler.ShowErrorsStats();
                        break;
                    case "6":
                        _inspectionHandler.AddInspection(loggedUser);
                        break;
                    case "7":
                        _inspectionHandler.ShowAllInspections();
                        break;
                    case "8":
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