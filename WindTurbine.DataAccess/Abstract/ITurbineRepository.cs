using WindTurbine.Entities;
namespace WindTurbine.DataAccess.Abstract
{
    public interface ITurbineRepository
    {
        Turbine CreateTurbine(Turbine turbine);
        List<Turbine> GetAllTurbines();
    }
}