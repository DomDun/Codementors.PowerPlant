namespace PowerPlantCzarnobyl.WebApi.Client.Models
{
    public class PowerPlantDataSet
    {
        public string PlantName { get; set; }
        public Cauldron[] Cauldrons { get; set; }
        public Turbine[] Turbines { get; set; }
        public Transformator[] Transformators { get; set; }
    }
}
