using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl.Domain
{

    public class RecievedDataService
    {
        public static RecievedDataService Instance { get; set; }

        public event EventHandler<PowerPlantDataSetData> OnRecieveData = null;

        public IRecievedDataRepository _recievedDataRepository;

        public PowerPlantDataSetData NewData { get; set; }

        public RecievedDataService(IRecievedDataRepository recievedDataRepository)
        {
            _recievedDataRepository = recievedDataRepository;

             _recievedDataRepository.OnRecievedDataToDomain += RecievedDataSender;
        }
        public void ActualDataSender()
        {
            _recievedDataRepository.Subscribe();
            _recievedDataRepository.OnRecievedDataToDomain += RecievedDataSender;
        }

        public PowerPlantDataSetData GetNewDataSet()
        {
            return NewData;
        }

        public void RecievedDataSender(object sender, PowerPlantDataSetData plant)
        {
            NewData = plant;

            if (OnRecieveData != null)
            {
                OnRecieveData(sender, plant);
            }
        }
    }
}
