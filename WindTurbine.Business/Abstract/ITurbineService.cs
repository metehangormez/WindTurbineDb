using WindTurbine.DTOs.Turbines; 
using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface ITurbineService
    {
 
        Turbine CreateTurbine(TurbineCreateDto turbineDto);
        void UpdateTurbine(TurbineDto turbineDto);
        void DeleteTurbine(int id);

        List<TurbineDto> GetAllTurbines();
    }
}