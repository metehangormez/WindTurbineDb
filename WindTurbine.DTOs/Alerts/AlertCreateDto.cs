using System;

namespace WindTurbine.DTOs.Alerts
{
    
    public class AlertCreateDto
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public int Severity { get; set; }
        public double Confidence { get; set; }
        public string Status { get; set; }

        
        public int TurbineId { get; set; }
    }
}