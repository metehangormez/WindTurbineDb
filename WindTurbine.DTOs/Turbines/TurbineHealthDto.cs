namespace WindTurbine.DTOs.Turbine
{
    public class TurbineHealthDto
    {
        public int TurbineId { get; set; }
        public string TurbineName { get; set; }

        // Hub & Blades
        public double Blades { get; set; }
        public double Hub { get; set; }
        public double PitchSystem { get; set; }

        // Nacelle & UPS
        public double Generator { get; set; }
        public double Gearbox { get; set; }
        public double Ups { get; set; }

        // Yaw & Tower
        public double WindDetail { get; set; }
        public double YawSystem { get; set; }
        public double Tower { get; set; }

        // Trafo & iletim
        public double Transformer { get; set; }
        public double ConverterInverter { get; set; }
        public double Circuit { get; set; }
    }
}
