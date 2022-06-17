using System.Collections.Generic;

namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class ErrorStatistics
    {
        public Dictionary<string, List<Error>> MachinesStats;
    }
}
