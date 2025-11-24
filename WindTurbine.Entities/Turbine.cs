using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace WindTurbine.Entities
{
    public class Turbine
    {
        public int TurbineId { get; set; } // Primary Key
        public string Name { get; set; } // Örn: "T-101" veya Zenodo'daki "asset_id"
        public string Model { get; set; }
        public double Latitude { get; set; } // Haritada göstermek için
        public double Longitude { get; set; }
        public double CurrentPower { get; set; } = 0; // Anlık Güç (kW/MW)
        public double CurrentWindSpeed { get; set; } = 0; // Rüzgar Hızı (m/s)
        public string LastStatus { get; set; } = "Bilinmiyor";

        public int WindFarmId { get; set; }

       
        public WindFarm WindFarm { get; set; }

       
        public ICollection<Alert> Alerts { get; set; }
        public double PitchAngle { get; set; }       // Kanat Açısı (Derece)
        public double BladeVibration { get; set; }   // Kanat Titreşimi (%)
        public double HubTemperature { get; set; }   // Hub Sıcaklığı (°C)
        public double GearboxOilTemp { get; set; }   // Dişli Kutusu Yağ Isısı (°C)
        public double GearboxVibration { get; set; } // Dişli Kutusu Titreşim (mm/s)
        public double GeneratorTemp { get; set; }    // Jeneratör Isısı (°C)
        public double GeneratorRPM { get; set; }     // Jeneratör Devri (RPM)
        public double MainBearingTemp { get; set; }  // Ana Rulman Isısı (°C)
        public double TransformerTemp { get; set; }  // Trafo Isısı (°C)
    }
}
