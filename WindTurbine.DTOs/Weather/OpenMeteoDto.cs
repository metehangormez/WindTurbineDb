using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WindTurbine.DTOs.Weather
{
    public class OpenMeteoResponse
    {
        // Artık "Daily" yerine "Hourly" verisi çekiyoruz
        [JsonPropertyName("hourly")]
        public HourlyData Hourly { get; set; }
    }

    public class HourlyData
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<double> Temperature2m { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public List<double> WindSpeed10m { get; set; }

        [JsonPropertyName("weather_code")]
        public List<int> WeatherCode { get; set; }
    }
}