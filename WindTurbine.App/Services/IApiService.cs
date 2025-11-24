using WindTurbine.DTOs.Alerts;
using WindTurbine.DTOs.Dashboard;
using WindTurbine.DTOs.Turbines;
using WindTurbine.DTOs.Auth;
using WindTurbine.DTOs.Reports;
using WindTurbine.DTOs.Weather;
using WindTurbine.DTOs.Turbine;

namespace WindTurbine.App.Services
{
    public interface IApiService
    {
        Task<AvailabilityDto> GetAvailabilityAsync();
        Task<bool> CreateReportAsync(ReportDto report);
        Task<List<WeatherForecastDto>> GetWeatherForecastAsync();
        Task<List<ReportDto>> GetReportsAsync();
        Task<List<TurbineDto>> GetTurbinesAsync();
        Task<List<AlertDto>> GetAlertsAsync();
        Task<bool> AddTurbineAsync(TurbineCreateDto dto);
        Task<bool> UpdateTurbineAsync(TurbineDto dto);
        Task<bool> DeleteTurbineAsync(int id);
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<bool> LoginAsync(UserLoginDto dto);
        Task<DashboardDto> GetDashboardAsync();
        Task<TurbineHealthDto?> GetTurbineHealthAsync(int turbineId);

    }
}