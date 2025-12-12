using System.Collections.Generic;

namespace WindTurbine.DTOs.Solar  // <-- Namespace burası çok önemli!
{
    public class SolarFarmDto
    {
        public double AnlikUretimkW { get; set; }
        public double GunlukUretimMWh { get; set; }
        public double Isinim { get; set; }
        public double PanelSicakligi { get; set; }
        public double PerformanceRatio { get; set; }

        // Listeyi boş başlatıyoruz ki "Null Reference" hatası vermesin
        public List<InverterDto> Inverters { get; set; } = new List<InverterDto>();

        public double[] UretimGrafigi { get; set; } = new double[0];
    }

    public class InverterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CurrentPower { get; set; }
        public double Efficiency { get; set; }
        public string Status { get; set; }
    }
}