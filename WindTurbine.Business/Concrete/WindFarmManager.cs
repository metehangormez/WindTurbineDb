using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.WindFarms;
using WindTurbine.Entities;
using System.Linq; 

namespace WindTurbine.Business.Concrete
{
    public class WindFarmManager : IWindFarmService
    {
        private readonly IWindFarmRepository _windFarmRepository;
        public WindFarmManager(IWindFarmRepository windFarmRepository) { _windFarmRepository = windFarmRepository; }

        public WindFarm CreateWindFarm(WindFarmCreateDto windFarmDto) 
        {
            
            var windFarm = new WindFarm
            {
                Name = windFarmDto.Name,
                Location = windFarmDto.Location
            };
            return _windFarmRepository.CreateWindFarm(windFarm);
        }

        public List<WindFarmDto> GetAllWindFarms() 
        {
            var windFarms = _windFarmRepository.GetAllWindFarms();

            
            return windFarms.Select(wf => new WindFarmDto
            {
                WindFarmId = wf.WindFarmId,
                Name = wf.Name,
                Location = wf.Location
            }).ToList();
        }
    }
}