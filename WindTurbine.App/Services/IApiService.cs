using WindTurbine.DTOs.Alerts; 

namespace WindTurbine.App.Services
{
    public interface IApiService
    {
        Task<List<AlertDto>> GetAlertsAsync();
        
    }
}