using Newtonsoft.Json;
using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Domain.Models;
using PowerPlantCzarnobyl.Infrastructure;
using System;
using System.IO;

namespace PowerPlantCzarnobyl
{
    internal class PowerPlantActionsHandler
    {
        private readonly CliHelper _cliHelper;
        private readonly LoginHandler _loginHandler;
        private readonly LibraryService _libraryService;
        private readonly ErrorService _errorService;
        private readonly MemberService _memberService;

        public PowerPlantActionsHandler()
        {
            _cliHelper = new CliHelper();

            var membersRepository = new MembersRepository();
            var libraryRepository = new LibraryRepository();
            var errorsRepository = new ErrorsRepository();
            var dateProvider = new DateProvider();

            _loginHandler = new LoginHandler(membersRepository);
            _libraryService = new LibraryService(libraryRepository);
            _errorService = new ErrorService(errorsRepository, dateProvider);
            _memberService = new MemberService(membersRepository);
        }
        public void ProgramLoop(string loggedMember)
        {
            bool exit = false;
            Member admin = _memberService.CheckMemberRole(loggedMember);

            while (!exit)
            {
                string operation = _cliHelper.GetStringFromUser("Enter number of operation: \n 1.Current work status \n 2.Add user \n 3.Delete User \n 4.Produced energy \n 5.Export file with errors \n 6.Exit \n");
                
                switch (operation)
                {
                    case "1":
                        CurrentWorkStatus();
                        break;
                    case "2":
                        _loginHandler.AddMember(admin);
                        break;
                    case "3":
                        _loginHandler.DeleteMember(admin);
                        break;
                    case "4":
                        ShowProducedPower();
                        break;
                    case "5":
                        ExportErrorsListToJson();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong number, try again");
                        break;
                }
            }
        }

        private async void ExportErrorsListToJson()
        {
            DateTime startData = _cliHelper.GetDateFromUser("give me start date in format yyyy/MM/dd:GHH:mm");
            DateTime endData = _cliHelper.GetDateFromUser("give me end date in format yyyy/MM/dd:GHH:mm");

            Console.Write("Enter file name to save data: ");
            string fileName = Console.ReadLine();
            fileName += ".json";

            while (File.Exists(fileName))
            {
                Console.WriteLine($"File {fileName} already exists! Choose different name: ");
                fileName = Console.ReadLine();
                fileName += ".json";
            }

            string json = JsonConvert.SerializeObject(await _errorService.GetAllErrorsAsync(startData, endData), Formatting.Indented);
            File.WriteAllText(fileName, json);

            if (!File.Exists(fileName))
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"List of errors wasn't created, something went wrong....");
                Console.ForegroundColor = defaultColor;
            }
            else
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"List of errors was created succesfully :)");
                Console.ForegroundColor = defaultColor;
            }
        }

        private void ShowProducedPower()
        {
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += _libraryService.ProducedPower;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                _libraryService.OnRecieveData -= _libraryService.ProducedPower;
                Console.Clear();
            }
        }

        private void CurrentWorkStatus()
        {
            _libraryService.ActualDataSender();
            _libraryService.OnRecieveData += _libraryService.PowerPlantDataArrived;
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                _libraryService.OnRecieveData -= _libraryService.PowerPlantDataArrived;
                Console.Clear();
            }
        }
    }
}