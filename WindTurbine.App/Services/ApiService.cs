using System.Net.Http.Json;
using WindTurbine.DTOs.Dashboard;
using WindTurbine.DTOs.Alerts;
using WindTurbine.DTOs.Turbines;
using WindTurbine.DTOs.Auth;
using WindTurbine.DTOs.Reports;
namespace WindTurbine.App.Services
{

    public class ApiService : IApiService
    {
        public async Task<List<ReportDto>> GetReportsAsync()
        {

            await Task.Delay(500); 

            return new List<ReportDto>
    {
        new ReportDto { ReportId = 1, Title = "2025 Kasım Ayı Performans Raporu", GeneratedDate = DateTime.Now.AddDays(-2), FileType = "PDF", FileSize = "4.2 MB", Status = "Hazır" },
        new ReportDto { ReportId = 2, Title = "2025 Kasım Ayı Arıza Dökümü", GeneratedDate = DateTime.Now.AddDays(-5), FileType = "Excel", FileSize = "1.8 MB", Status = "Hazır" },
        new ReportDto { ReportId = 3, Title = "2025 Ekim Ayı Genel Faaliyet Raporu", GeneratedDate = DateTime.Now.AddMonths(-1), FileType = "PDF", FileSize = "15.4 MB", Status = "Hazır" },
        new ReportDto { ReportId = 4, Title = "Q3 (Temmuz-Eylül) Finansal Özet", GeneratedDate = DateTime.Now.AddMonths(-2), FileType = "Excel", FileSize = "8.1 MB", Status = "Arşiv" },
        new ReportDto { ReportId = 5, Title = "Yıllık Bakım Planı 2025", GeneratedDate = new DateTime(2025, 1, 15), FileType = "PDF", FileSize = "12.0 MB", Status = "Arşiv" }
    };
        }
        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(UserLoginDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AddTurbineAsync(TurbineCreateDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Turbines", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTurbineAsync(TurbineDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Turbines", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTurbineAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Turbines/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<TurbineDto>> GetTurbinesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TurbineDto>>("api/Turbines");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Türbinler alınamadı: {ex.Message}");
                return new List<TurbineDto>();
            }
        }
        private readonly HttpClient _httpClient;
        public async Task<DashboardDto> GetDashboardAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DashboardDto>("api/Dashboard");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard hatası: {ex.Message}");
                return null;
            }
        }

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AlertDto>> GetAlertsAsync()
        {
            try
            {
                
                return await _httpClient.GetFromJsonAsync<List<AlertDto>>("api/Alerts");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"API'ye bağlanırken hata oluştu: {ex.Message}");
                return new List<AlertDto>(); 
            }
        }
    }
}