﻿using System;

namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class Inspection
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
    }
}