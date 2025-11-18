using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;

namespace WindTurbine.DataAccess.EntityFramework 
{
    public class EfWindFarmRepository : IWindFarmRepository
    {
        private readonly WindTurbineDbContext _context;

        public EfWindFarmRepository(WindTurbineDbContext context)
        {
            _context = context;
        }

        public WindFarm CreateWindFarm(WindFarm windFarm)
        {
            _context.WindFarms.Add(windFarm);
            _context.SaveChanges();
            return windFarm;
        }

        public List<WindFarm> GetAllWindFarms()
        {
            return _context.WindFarms.ToList();
        }
    }
}