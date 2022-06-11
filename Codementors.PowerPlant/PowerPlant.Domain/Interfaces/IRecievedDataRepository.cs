using PowerPlantCzarnobyl.Domain.Models;
using System;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface IRecievedDataRepository
    {
        event EventHandler<PowerPlantDataSetData> OnRecievedDataToDomain;
        void Subscribe();
        void RecieveDataFromLibrary(object sender, object plant);
    }
}
