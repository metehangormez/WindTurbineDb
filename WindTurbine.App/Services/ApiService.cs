using System.Net.Http.Json; 
                            
using WindTurbine.DTOs.Alerts;

namespace WindTurbine.App.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

       
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