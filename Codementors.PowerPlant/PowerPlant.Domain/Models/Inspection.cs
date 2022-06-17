using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Inspection
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MachineName { get; set; }
        public string Comments { get; set; }
        public State State;
    }

    public enum State
    {
        Open,
        InProgress,
        Closed,
        IncorrectState
    }
}
