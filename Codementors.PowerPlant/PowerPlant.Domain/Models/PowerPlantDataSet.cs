namespace PowerPlantCzarnobyl.Domain.Models
{
    public class PowerPlantDataSetData
    {
        public string PlantName { get; internal set; }
        public CauldronData[] Cauldrons { get; internal set; }
        public TurbineData[] Turbines { get; internal set; }
        public TransformatorData[] Transformators { get; internal set; }
    }
}
