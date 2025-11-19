using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;
using System.Linq;

namespace WindTurbine.DataAccess.EntityFramework
{
    public class EfUserRepository : IUserRepository
    {
        private readonly WindTurbineDbContext _context;
        public EfUserRepository(WindTurbineDbContext context) { _context = context; }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}