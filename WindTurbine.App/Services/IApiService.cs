using WindTurbine.DTOs.Alerts;
using WindTurbine.DTOs.Dashboard;
using WindTurbine.DTOs.Turbines;
using WindTurbine.DTOs.Auth;
using WindTurbine.DTOs.Reports;
namespace WindTurbine.App.Services
{
    public interface IApiService
    {
        Task<List<ReportDto>> GetReportsAsync();
        Task<List<TurbineDto>> GetTurbinesAsync();
        Task<List<AlertDto>> GetAlertsAsync();
        Task<bool> AddTurbineAsync(TurbineCreateDto dto);
        Task<bool> UpdateTurbineAsync(TurbineDto dto);
        Task<bool> DeleteTurbineAsync(int id);
        Task<bool> RegisterAsync(UserRegisterDto dto);
        Task<bool> LoginAsync(UserLoginDto dto);
        Task<DashboardDto> GetDashboardAsync(); 
    }
}