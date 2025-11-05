using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace WindTurbine.Entities
{
    public class WindFarm
    {
        public int WindFarmId { get; set; } 
        public string Name { get; set; }
        public string Location { get; set; }

        
        
        public ICollection<Turbine> Turbines { get; set; } = new List<Turbine>();
    }
}