namespace PowerPlantCzarnobyl.Domain.Models
{
    public class PowerPlantDataSet
    {
        public string PlantName { get; internal set; }
        public Cauldron[] Cauldrons { get; internal set; }
        public Turbine[] Turbines { get; internal set; }
        public Transformator[] Transformators { get; internal set; }
    }
}
