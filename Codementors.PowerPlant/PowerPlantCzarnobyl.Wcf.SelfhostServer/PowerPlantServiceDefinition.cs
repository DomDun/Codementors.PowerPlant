using PowerPlantCzarnobyl.Domain;
using PowerPlantCzarnobyl.Infrastructure;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions;
using PowerPlantCzarnobyl.Wcf.ServiceDefinitions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Wcf.SelfhostServer
{
    public class PowerPlantServiceDefinition : IMemberManagementService, IReceivedDataManagementService, IErrorManagementService, IInspectionManagementService
    {
        private readonly MemberService _memberService;
        private readonly ErrorService _errorService;
        private readonly InspectionService _inspectionService;
        private readonly Mapper _mapper;

        public PowerPlantServiceDefinition()
        {
            var memberRepository = new MembersRepository();
            var errorRepository = new ErrorsRepository();
            var inspectionRepository = new InspectionRepository();

            _mapper = new Mapper();
            _memberService = new MemberService(memberRepository);
            _errorService = new ErrorService(errorRepository, null);
            _inspectionService = new InspectionService(inspectionRepository);
        }

        //Members
        public bool AddMember(MemberWcf member)
        {
            return _memberService.Add(_mapper.MapToDomainMember(member));
        }

        public bool CheckMemberCredentials(string username, string password)
        {
            return _memberService.CheckUserCredentials(username, password);
        }

        public MemberWcf CheckMemberRole(string login)
        {
            return _mapper.MapToContractMember(_memberService.CheckMemberRole(login));
        }

        public bool DeleteMember(string member)
        {
            return _memberService.Delete(member);
        }

        //Data from plant
        public PowerPlantDataSetWcf GetNewDataSet()
        {
            return _mapper.MapToContractDataFromPlant(ReceivedDataService.Instance.NewData);
        }

        //Errors
        public List<ErrorWcf> GetAllErrors(DateTime startData, DateTime endData)
        {
            List<ErrorWcf> wcfErrors = new List<ErrorWcf>();

            foreach (var error in _errorService.GetAllErrors(startData, endData))
            {
                wcfErrors.Add(_mapper.MapToContractError(error));
            }

            return wcfErrors;
        }

        public Dictionary<string, int> GetAllErrorsInDictionary(DateTime startData, DateTime endData)
        {
            return _errorService.GetAllErrorsInDictionary(startData, endData);
        }

        public void AddError(ErrorWcf error)
        {
            _errorService.AddError(_mapper.MapToDomainError(error));
        }

        //Inspections
        public Task<bool> AddInspectionAsync(InspectionWcf inspection)
        {
            return _inspectionService.AddInspectionAsync(_mapper.MapToDomainInspection(inspection));
        }

        public List<InspectionWcf> GetAllInspections()
        {
            List<InspectionWcf> wcfInspections = new List<InspectionWcf>();

            foreach (var inspection in _inspectionService.GetAllInspections())
            {
                wcfInspections.Add(_mapper.MapToContractInspection(inspection));
            }

            return wcfInspections;
        }

        public InspectionWcf GetInspection(int id)
        {
            return _mapper.MapToContractInspection(_inspectionService.GetInspection(id));
        }

        public bool UpdateInspection(int id, InspectionWcf inspection)
        {
            return _inspectionService.UpdateInspection(id, _mapper.MapToDomainInspection(inspection));
        }
    }
}
