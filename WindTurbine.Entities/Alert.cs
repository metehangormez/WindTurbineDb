using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WindTurbine.Entities
{
    public class Alert
    {
        public int AlertId { get; set; } 

        public DateTime Timestamp { get; set; } 
        public string Message { get; set; } 
        public int Severity { get; set; } 
        public double Confidence { get; set; } 
        public string Status { get; set; } 

        
        public int TurbineId { get; set; }
        public Turbine Turbine { get; set; }
    }
}