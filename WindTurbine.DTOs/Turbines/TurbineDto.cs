namespace WindTurbine.DTOs.Turbines
{
    public class TurbineDto
    {
        public double CurrentPower { get; set; }
        public double CurrentWindSpeed { get; set; }
        public string LastStatus { get; set; }
        public int TurbineId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int WindFarmId { get; set; }
    }
}