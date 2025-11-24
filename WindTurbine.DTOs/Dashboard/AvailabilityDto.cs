using System.Collections.Generic;

namespace WindTurbine.DTOs.Dashboard
{
    public class AvailabilityDto
    {
        // Kart Verileri
        public double BeklenenUretimTotal { get; set; }
        public double OlusanUretimTotal { get; set; }
        public double GunBeklentisi { get; set; }

        // Grafik Verileri (Saatlik veya Günlük)
        public List<string> Labels { get; set; } = new List<string>();
        public List<double> BeklenenSeries { get; set; } = new List<double>();
        public List<double> OlusanSeries { get; set; } = new List<double>();
    }
}