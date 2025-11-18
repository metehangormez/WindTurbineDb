using WindTurbine.DTOs.Turbines; 
using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface ITurbineService
    {
 
        Turbine CreateTurbine(TurbineCreateDto turbineDto);

       
        List<TurbineDto> GetAllTurbines();
    }
}