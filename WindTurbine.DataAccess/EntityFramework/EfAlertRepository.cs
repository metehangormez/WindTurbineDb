using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;

namespace WindTurbine.DataAccess.Repositories // veya .Concrete
{
    public class EfAlertRepository : IAlertRepository
    {
        // DbContext'i "Dependency Injection" (DI) ile alacağız
        private readonly WindTurbineDbContext _context;

        public EfAlertRepository(WindTurbineDbContext context)
        {
            _context = context;
        }

        public Alert CreateAlert(Alert alert)
        {
            _context.Alerts.Add(alert);
            _context.SaveChanges(); // Değişiklikleri veritabanına kaydet
            return alert;
        }

        public List<Alert> GetAllAlerts()
        {
            return _context.Alerts.ToList();
        }
    }
}