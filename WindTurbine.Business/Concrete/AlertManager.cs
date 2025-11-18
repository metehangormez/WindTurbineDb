using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.Alerts; 
using WindTurbine.Entities;
using System.Linq; 

namespace WindTurbine.Business.Concrete
{
    public class AlertManager : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        public AlertManager(IAlertRepository alertRepository) { _alertRepository = alertRepository; }

        public Alert CreateAlert(AlertCreateDto alertDto)
        {
            var alert = new Alert
            {
                Timestamp = alertDto.Timestamp,
                Message = alertDto.Message,
                Severity = alertDto.Severity,
                Confidence = alertDto.Confidence,
                Status = alertDto.Status,
                TurbineId = alertDto.TurbineId
            };
            return _alertRepository.CreateAlert(alert);
        }

        public List<AlertDto> GetAllAlerts() 
        {
            var alerts = _alertRepository.GetAllAlerts();

    
            return alerts.Select(alert => new AlertDto
            {
                AlertId = alert.AlertId,
                Timestamp = alert.Timestamp,
                Message = alert.Message,
                Severity = alert.Severity,
                Confidence = alert.Confidence,
                Status = alert.Status,
                TurbineId = alert.TurbineId
            }).ToList();
        }
    }
}