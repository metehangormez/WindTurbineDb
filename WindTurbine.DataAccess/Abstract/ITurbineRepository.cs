using WindTurbine.Entities;
namespace WindTurbine.DataAccess.Abstract
{
    public interface ITurbineRepository
    {
        Turbine CreateTurbine(Turbine turbine);
        List<Turbine> GetAllTurbines();
        void UpdateTurbine(Turbine turbine);
        void DeleteTurbine(int id);
        Turbine GetTurbineById(int id);
    }
}