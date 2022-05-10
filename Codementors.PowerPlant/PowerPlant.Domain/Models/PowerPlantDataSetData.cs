namespace PowerPlantCzarnobyl.Domain.Models
{
    public class PowerPlantDataSetData
    {
        public string PlantName { get; set; }
        public CauldronData[] Cauldrons { get; set; }
        public TurbineData[] Turbines { get; set; }
        public TransformatorData[] Transformators { get; set; }
    }
}
