using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface IAlertService
    {
        Alert CreateAlert(Alert alert);
        List<Alert> GetAllAlerts();
    }
}