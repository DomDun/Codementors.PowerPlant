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
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Show all errors (yyyy-MM-ddTHH:mm:ss) \n 5.Add Inspection \n 6.Show inspections by selected dates \n 7.Exit \n");

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
                        _inspectionHandler.AddInspection();
                        break;
                    case "6":
                        _inspectionHandler.ShowInspectionsBySelectedDate();
                        break;
                    case "7":
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