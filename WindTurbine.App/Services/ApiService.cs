using System.Net.Http.Json;
using WindTurbine.DTOs.Alerts;
using WindTurbine.DTOs.Dashboard;
using WindTurbine.DTOs.Reports;
using WindTurbine.DTOs.Turbine;
using WindTurbine.DTOs.Turbines;
using WindTurbine.DTOs.Weather; 

namespace WindTurbine.App.Services
{
    public class ApiService : IApiService
    {
        public async Task<AvailabilityDto> GetAvailabilityAsync()
        {
           
            await Task.Delay(300);

            var data = new AvailabilityDto
            {
                BeklenenUretimTotal = 750.5,
                OlusanUretimTotal = 625.4,
                GunBeklentisi = 800.0,

                Labels = new List<string> { "00:00", "04:00", "08:00", "12:00", "16:00", "20:00", "23:59" },
                BeklenenSeries = new List<double> { 50, 45, 70, 90, 95, 85, 60 },
                OlusanSeries = new List<double> { 40, 25, 60, 85, 90, 75, 50 }
            };

            return data;
        }
        public async Task<TurbineHealthDto?> GetTurbineHealthAsync(int turbineId)
        {
            return await _httpClient
                .GetFromJsonAsync<TurbineHealthDto>($"api/TurbineHealth/{turbineId}");
        }

        public async Task<bool> CreateReportAsync(ReportDto report)
        {
            
            var response = await _httpClient.PostAsJsonAsync("api/Reports", report);
            return response.IsSuccessStatusCode;
        }
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

    
        public async Task<DashboardDto> GetDashboardAsync() => await _httpClient.GetFromJsonAsync<DashboardDto>("api/Dashboard");
        public async Task<List<AlertDto>> GetAlertsAsync() => await _httpClient.GetFromJsonAsync<List<AlertDto>>("api/Alerts");
        public async Task<List<TurbineDto>> GetTurbinesAsync() => await _httpClient.GetFromJsonAsync<List<TurbineDto>>("api/Turbines");
        public async Task<bool> AddTurbineAsync(TurbineCreateDto dto) => (await _httpClient.PostAsJsonAsync("api/Turbines", dto)).IsSuccessStatusCode;
        public async Task<bool> UpdateTurbineAsync(TurbineDto dto) => (await _httpClient.PutAsJsonAsync("api/Turbines", dto)).IsSuccessStatusCode;
        public async Task<bool> DeleteTurbineAsync(int id) => (await _httpClient.DeleteAsync($"api/Turbines/{id}")).IsSuccessStatusCode;
        public async Task<bool> RegisterAsync(WindTurbine.DTOs.Auth.UserRegisterDto dto) => (await _httpClient.PostAsJsonAsync("api/Auth/register", dto)).IsSuccessStatusCode;
        public async Task<bool> LoginAsync(WindTurbine.DTOs.Auth.UserLoginDto dto) => (await _httpClient.PostAsJsonAsync("api/Auth/login", dto)).IsSuccessStatusCode;
        
        public async Task<List<WeatherForecastDto>> GetWeatherForecastAsync()
        {
            try
            {
                // 1. URL DEĞİŞTİ: "daily" yerine "hourly" istiyoruz
                // Manisa Koordinatları: 38.61, 27.42
                string url = "https://api.open-meteo.com/v1/forecast?latitude=38.61&longitude=27.42&hourly=temperature_2m,wind_speed_10m,weather_code&timezone=auto";

                using var externalClient = new HttpClient();
                var response = await externalClient.GetFromJsonAsync<OpenMeteoResponse>(url);

                if (response?.Hourly == null) return new List<WeatherForecastDto>();

                var forecasts = new List<WeatherForecastDto>();
                var data = response.Hourly;

                // 2. ŞU ANKİ SAATİ BULMA ALGORİTMASI
                var simdi = DateTime.Now;
                int baslangicIndex = -1;

                // Listede şu anki saatle eşleşen (veya en yakın gelecek) saati bul
                for (int i = 0; i < data.Time.Count; i++)
                {
                    var zaman = DateTime.Parse(data.Time[i]);
                    // Aynı gün ve aynı saat (veya sonrası)
                    if (zaman >= simdi.AddMinutes(-59)) // (-59 dk: Saat 16:50 ise 16:00 verisini kaçırmamak için)
                    {
                        baslangicIndex = i;
                        break;
                    }
                }

                // 3. ŞU AN + 5 SAAT (Toplam 6 Veri) AL
                if (baslangicIndex != -1)
                {
                    // Döngü 6 kez dönecek (Şu an + 5 saat)
                    for (int i = baslangicIndex; i < baslangicIndex + 6; i++)
                    {
                        // Dizi sınırını aşmamak için kontrol
                        if (i >= data.Time.Count) break;

                        double windKmH = data.WindSpeed10m[i];
                        double windMs = Math.Round(windKmH / 3.6, 1); // km/h -> m/s çevrimi

                        int code = data.WeatherCode[i];
                        string condition = WmoCodeToText(code);

                        // --- AI KARAR MANTIĞI (SAATLİK) ---
                        string comment = "";
                        string recommendation = "";

                        if (windMs >= 25)
                        {
                            comment = $"TEHLİKE: {windMs} m/s fırtına!";
                            recommendation = "ACİL DURDURUN";
                        }
                        else if (windMs > 12)
                        {
                            comment = $"Rüzgar sert ({windMs} m/s).";
                            recommendation = "Kısıtlı Mod / İzle";
                        }
                        else if (windMs >= 5)
                        {
                            comment = $"Rüzgar ({windMs} m/s) ideal seviyede.";
                            recommendation = "Tam Kapasite Çalıştır";
                        }
                        else
                        {
                            comment = $"Rüzgar ({windMs} m/s) yetersiz.";
                            recommendation = "Bakım İçin Uygun";
                        }

                        if (condition.Contains("Fırtına") || condition.Contains("Kar"))
                        {
                            recommendation = "Sahaya Çıkış Yasak";
                        }

                        forecasts.Add(new WeatherForecastDto
                        {
                            Date = DateTime.Parse(data.Time[i]),
                            Temperature = data.Temperature2m[i],
                            WindSpeed = windMs,
                            Condition = condition,
                            AiComment = comment,
                            Recommendation = recommendation
                        });
                    }
                }

                return forecasts;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return new List<WeatherForecastDto>();
            }
        }

        // Yardımcı: WMO Hava Durumu Kodlarını Metne Çevirir
        private string WmoCodeToText(int code)
        {
            return code switch
            {
                0 => "Güneşli",
                1 or 2 or 3 => "Parçalı Bulutlu",
                45 or 48 => "Sisli",
                51 or 53 or 55 => "Çiseleme",
                61 or 63 or 65 => "Yağmurlu",
                71 or 73 or 75 => "Karlı",
                95 or 96 or 99 => "Fırtınalı",
                _ => "Bulutlu"
            };
        }

        // ... (Rapor Metodu varsa o da kalsın) ...
        public async Task<List<ReportDto>> GetReportsAsync()
        {
            try
            {
                // Artık SAHTE VERİ YOK. Gerçek API'ye gidiyor.
                return await _httpClient.GetFromJsonAsync<List<ReportDto>>("api/Reports");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Raporlar alınamadı: {ex.Message}");
                return new List<ReportDto>();
            }
        }
    }
}