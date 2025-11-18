using WindTurbine.DTOs.WindFarms; 
using WindTurbine.Entities;

namespace WindTurbine.Business.Abstract
{
    public interface IWindFarmService
    {
        WindFarm CreateWindFarm(WindFarmCreateDto windFarmDto); 
        List<WindFarmDto> GetAllWindFarms(); 
    }
}