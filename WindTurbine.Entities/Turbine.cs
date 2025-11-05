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

        // --- İlişkiler ---

        // Foreign Key (Yabancı Anahtar)
        public int WindFarmId { get; set; }

        // Navigation Property: 
        // Bu türbinin ait olduğu tek bir Rüzgar Santrali vardır.
        public WindFarm WindFarm { get; set; }

        // Navigation Property: 
        // Bir Türbinin birden fazla Alarmı olabilir.
        public ICollection<Alert> Alerts { get; set; }
    }
}
