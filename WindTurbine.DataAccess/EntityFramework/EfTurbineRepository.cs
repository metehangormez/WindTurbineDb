using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;
namespace WindTurbine.DataAccess.EntityFramework
{
    public class EfTurbineRepository : ITurbineRepository
    {
        private readonly WindTurbineDbContext _context;
        public EfTurbineRepository(WindTurbineDbContext context) { _context = context; }

        public Turbine CreateTurbine(Turbine turbine)
        {
            _context.Turbines.Add(turbine);
            _context.SaveChanges();
            return turbine;
        }
        public List<Turbine> GetAllTurbines()
        {
            return _context.Turbines.ToList();
        }
    }
}