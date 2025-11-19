using WindTurbine.Entities;

namespace WindTurbine.DataAccess.Abstract
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByEmail(string email);
    }
}