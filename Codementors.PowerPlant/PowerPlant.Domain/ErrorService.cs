using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IErrorsService
    {
        void Add(Error error);
        void CheckIfMachinesWorkCorrectly(object sender, PowerPlantDataSetData plant);
        bool CheckValue(string machineName, string parameter, AssetParameterData value, PowerPlantDataSetData plant, string user);
        List<Error> GetAllErrorsAsync(DateTime startData, DateTime endData);
        Dictionary<string, int> GetAllErrorsInDictionaryAsync(DateTime startData, DateTime endData);
    }
    public class ErrorService : IErrorsService
    {
        private readonly IErrorsRepository _errorsRepository;
        private readonly IDateProvider _dateProvider;
        public string _loggedUser;

        public ErrorService(IErrorsRepository errorsRepository, IDateProvider dateProvider)
        {
            _errorsRepository = errorsRepository;
            _dateProvider = dateProvider;
        }

        public void Add(Error error)
        {
            _errorsRepository.AddError(error);
        }

        public void CheckIfMachinesWorkCorrectly(object sender, PowerPlantDataSetData plant)
        {

            foreach (var cauldron in plant.Cauldrons)
            {
                CheckValue(cauldron.Name, "WaterPressure", cauldron.WaterPressure, plant, _loggedUser);
                CheckValue(cauldron.Name, "WaterTemperature", cauldron.WaterTemperature, plant, _loggedUser);
                CheckValue(cauldron.Name, "CamberTemperature", cauldron.CamberTemperature, plant, _loggedUser);
            }

            foreach (var turbine in plant.Turbines)
            {
                CheckValue(turbine.Name, "SteamPressure", turbine.SteamPressure, plant, _loggedUser);
                CheckValue(turbine.Name, "OverheaterSteamTemperature", turbine.OverheaterSteamTemperature, plant, _loggedUser);
                CheckValue(turbine.Name, "OutputVoltage", turbine.OutputVoltage, plant, _loggedUser);
                CheckValue(turbine.Name, "RotationSpeed", turbine.RotationSpeed, plant, _loggedUser);
                CheckValue(turbine.Name, "CurrentPower", turbine.CurrentPower, plant, _loggedUser);
            }

            foreach (var transformator in plant.Transformators)
            {
                CheckValue(transformator.Name, "InputVoltage", transformator.InputVoltage, plant, _loggedUser);
                CheckValue(transformator.Name, "OutputVoltage", transformator.OutputVoltage, plant, _loggedUser);
            }
        }

        public bool CheckValue(string machineName, string parameter, AssetParameterData value, PowerPlantDataSetData plant, string user)
        {
            if (value.CurrentValue > value.MaxValue || value.CurrentValue < value.MinValue)
            {
                Error error = new Error()
                {
                    PlantName = plant.PlantName,
                    MachineName = machineName,
                    Parameter = parameter,
                    ErrorTime = _dateProvider.Now,
                    LoggedUser = user,
                    MinValue = value.MinValue,
                    MaxValue = value.MaxValue
                };

                if (user == null)
                {
                    error.LoggedUser = "N/A";
                }

                Add(error);
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Error> GetAllErrorsAsync(DateTime startData, DateTime endData)
        {
            return  _errorsRepository.GetAllErrorsAsync(startData, endData);
        }

        public Dictionary<string, int> GetAllErrorsInDictionaryAsync(DateTime startData, DateTime endData)
        {
            return _errorsRepository.GetAllErrorsAsync(startData, endData)
                .GroupBy(x=>x.MachineName)
                .ToDictionary(x=>x.Key,x=>x.Count());
        }
    }
}
