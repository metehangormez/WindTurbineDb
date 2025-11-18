using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindTurbine.Entities;

namespace WindTurbine.DataAccess.Abstract
{
    public interface IWindFarmRepository
    {
        WindFarm CreateWindFarm(WindFarm windFarm);
        List<WindFarm> GetAllWindFarms();
    }
}
