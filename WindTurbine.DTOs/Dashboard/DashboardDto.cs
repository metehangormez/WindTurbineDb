using System.Collections.Generic;

namespace WindTurbine.DTOs.Dashboard
{
    public class DashboardDto
    {
        // Kart Verileri
        public int RiskliUyariSayisi { get; set; }
        public int ToplamOneriSayisi { get; set; }
        public int AktifTurbinSayisi { get; set; }
        public double ToplamUretimMW { get; set; }
        public double VerimlilikYuzdesi { get; set; }

        // Grafik Verileri
        public List<double> GerceklesUretimSerisi { get; set; } = new List<double>();
        public List<double> BeklenenUretimSerisi { get; set; } = new List<double>();
        public List<string> SaatEtiketleri { get; set; } = new List<string>();
    }
}