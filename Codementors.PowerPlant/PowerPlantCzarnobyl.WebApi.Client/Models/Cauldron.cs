namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class Cauldron
    {
        public string Name { get; set; }
        public AssetParameter WaterPressure { get; set; }
        public AssetParameter WaterTemperature { get; set; }
        public AssetParameter CamberTemperature { get; set; }
    }
}
