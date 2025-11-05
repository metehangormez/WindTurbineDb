using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract; // Repository arayüzünü çağıracağız
using WindTurbine.Entities;

namespace WindTurbine.Business.Concrete
{
    public class AlertManager : IAlertService
    {
        // Business katmanı, DataAccess katmanına bağlıdır
        private readonly IAlertRepository _alertRepository;

        public AlertManager(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public Alert CreateAlert(Alert alert)
        {
            // Gelecekte burada iş kuralları olabilir
            // (Örn: if(alert.Severity > 3) { SendEmail(); } )

            return _alertRepository.CreateAlert(alert);
        }

        public List<Alert> GetAllAlerts()
        {
            return _alertRepository.GetAllAlerts();
        }
    }
}