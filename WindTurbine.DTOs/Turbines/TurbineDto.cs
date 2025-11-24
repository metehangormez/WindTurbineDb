namespace WindTurbine.DTOs.Turbines
{
    public class TurbineDto
    {
        public int TurbineId { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int WindFarmId { get; set; }

        // --- HARİTA İÇİN GEREKLİ OLANLAR (BUNLAR EKSİKTİ) ---
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        // ---------------------------------------------------

        // --- CANLI VERİLER ---
        public double CurrentPower { get; set; }
        public double CurrentWindSpeed { get; set; }
        public string LastStatus { get; set; }

        // --- DETAYLI SENSÖR VERİLERİ ---
        public double PitchAngle { get; set; }
        public double BladeVibration { get; set; }
        public double HubTemperature { get; set; }
        public double GearboxOilTemp { get; set; }
        public double GearboxVibration { get; set; }
        public double GeneratorTemp { get; set; }
        public double GeneratorRPM { get; set; }
        public double MainBearingTemp { get; set; }
        public double TransformerTemp { get; set; }
    }
}