namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class Transformator
    {
        public string Name { get; set; }
        public AssetParameter InputVoltage { get; set; }
        public AssetParameter OutputVoltage { get; set; }
    }
}
