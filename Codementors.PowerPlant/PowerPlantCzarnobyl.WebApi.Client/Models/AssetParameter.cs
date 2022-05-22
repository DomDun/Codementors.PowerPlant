namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class AssetParameter
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double TypicalValue { get; set; }
        public double CurrentValue { get; set; }
        public string Unit { get; set; }
    }
}
