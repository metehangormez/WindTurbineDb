using System;

namespace WindTurbine.DTOs.Weather
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }
        public double WindSpeed { get; set; } // m/s
        public double Temperature { get; set; } // Derece
        public string Condition { get; set; } // "Güneşli", "Fırtınalı" vs.
        public string AiComment { get; set; } // Yapay Zeka Yorumu
        public string Recommendation { get; set; } // Örn: "Bakım İçin Uygun"
    }
}