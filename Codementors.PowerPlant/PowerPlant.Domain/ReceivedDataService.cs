using PowerPlantCzarnobyl.Domain.Interfaces;
using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IReceivedDataService
    {
        event EventHandler<PowerPlantDataSetData> OnRecieveData;
        void ActualDataSender();
        PowerPlantDataSetData GetNewDataSet();
        void RecievedDataSender(object sender, PowerPlantDataSetData plant);
    }

    public class ReceivedDataService : IReceivedDataService
    {
        public static ReceivedDataService Instance { get; set; }

        public event EventHandler<PowerPlantDataSetData> OnRecieveData = null;

        public IRecievedDataRepository _recievedDataRepository;

        public PowerPlantDataSetData NewData { get; set; }

        public ReceivedDataService(IRecievedDataRepository recievedDataRepository)
        {
            _recievedDataRepository = recievedDataRepository;
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
