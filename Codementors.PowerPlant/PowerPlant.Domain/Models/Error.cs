using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Error
    {
        public string PlantName { get; set; }
        public string MachineName { get; set; }
        public string Parameter { get; set; }
        public DateTime ErrorTime { get; set; }
        public string LoggedUser { get; set; }
        
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
