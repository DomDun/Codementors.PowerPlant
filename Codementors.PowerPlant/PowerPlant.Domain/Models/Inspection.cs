using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PowerPlantCzarnobyl.Domain.Models
{
    public class Inspection
    {
        public int Id;
        [DisplayName("Create Date")]
        [Required]
        public DateTime CreateDate { get; set; }

        [DisplayName("Update Date")]
        public DateTime? UpdateDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Machine Name")]
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
