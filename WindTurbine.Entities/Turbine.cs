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
    }
}
