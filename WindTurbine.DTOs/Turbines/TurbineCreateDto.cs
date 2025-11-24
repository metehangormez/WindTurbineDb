namespace WindTurbine.DTOs.Turbines
{
    public class TurbineCreateDto
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int WindFarmId { get; set; }

        // --- YENİ EKLENEN ---
        // Varsayılan olarak "1" (Aktif) olsun
        public string InitialStatus { get; set; } = "1";
    }
}