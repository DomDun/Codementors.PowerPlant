using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Inspection
    {
        public int Id;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MachineName { get; set; }
        public string Comments { get; set; }
        public State State;
        public string Engineer;
    }

    public enum State
    {
        Open,
        InProgress,
        Closed,
        IncorrectState
    }
}
