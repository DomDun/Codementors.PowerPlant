using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Error
    {
        public string PlantName { get; set; }
        public string MachineName { get; set; }
        public double MachineValue { get; set; }
        public string Unit { get; set; }
        public DateTime errorTime { get; set; }
    }
}
