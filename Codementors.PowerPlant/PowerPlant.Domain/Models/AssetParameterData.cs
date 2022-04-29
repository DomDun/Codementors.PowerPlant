﻿using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class AssetParameterData
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double TypicalValue { get; set; }
        public double CurrentValue { get; set; }
        public string Unit { get; set; }

        public AssetParameterData(double minValue, double maxValue, double typicalValue, string unit)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            TypicalValue = typicalValue;
            CurrentValue = 0.0d;
            Unit = unit;
        }
    }
}