using System;

namespace PowerPlantCzarnobyl.Domain.Models
{
    [Serializable]
    public class AssetParameter
    {
        public double MinValue { get; }
        public double MaxValue { get; }
        public double TypicalValue { get; }
        public double CurrentValue { get; internal set; }
        public string Unit { get; }

        internal AssetParameter(double minValue, double maxValue, double typicalValue, string unit)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            TypicalValue = typicalValue;
            CurrentValue = 0.0d;
            Unit = unit;
        }
    }
}
