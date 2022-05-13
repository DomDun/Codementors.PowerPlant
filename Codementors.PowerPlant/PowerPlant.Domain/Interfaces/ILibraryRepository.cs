using PowerPlantCzarnobyl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlantCzarnobyl.Domain.Interfaces
{
    public interface ILibraryRepository
    {
        event EventHandler<PowerPlantDataSetData> OnRecievedDataToDomain;
        void Subscribe();
        void RecieveDataFromLibrary(object sender, object plant);
    }
}
