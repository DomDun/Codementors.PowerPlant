namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class Turbine
    {
        public string Name { get; set; }
        public AssetParameter OverheaterSteamTemperature { get; set; }
        public AssetParameter SteamPressure { get; set; }
        public AssetParameter RotationSpeed { get; set; }
        public AssetParameter CurrentPower { get; set; }
        public AssetParameter OutputVoltage { get; set; }
    }
}
