namespace WindTurbine.DTOs.Turbines
{
    public class TurbineDto
    {
        public int TurbineId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int WindFarmId { get; set; }
    }
}