using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;
namespace WindTurbine.DataAccess.EntityFramework
{
    public class EfTurbineRepository : ITurbineRepository
    {
        public void UpdateTurbine(Turbine turbine)
        {
            _context.Turbines.Update(turbine);
            _context.SaveChanges();
        }

        public void DeleteTurbine(int id)
        {
            var turbine = _context.Turbines.Find(id);
            if (turbine != null)
            {
                _context.Turbines.Remove(turbine);
                _context.SaveChanges();
            }
        }

        public Turbine GetTurbineById(int id)
        {
            return _context.Turbines.Find(id);
        }
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