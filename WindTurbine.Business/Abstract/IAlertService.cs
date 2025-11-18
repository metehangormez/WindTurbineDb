using WindTurbine.DTOs.Alerts; 
using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface IAlertService
    {
        Alert CreateAlert(AlertCreateDto alertDto);
        List<AlertDto> GetAllAlerts(); 
    }
}