using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.Turbines; 
using WindTurbine.Entities;
using System.Linq; 

namespace WindTurbine.Business.Concrete
{
    public class TurbineManager : ITurbineService
    {
        private readonly ITurbineRepository _turbineRepository;
        public TurbineManager(ITurbineRepository turbineRepository) { _turbineRepository = turbineRepository; }

        public Turbine CreateTurbine(TurbineCreateDto turbineDto)
        {
            
            var turbine = new Turbine
            {
                Name = turbineDto.Name,
                Model = turbineDto.Model,
                Latitude = turbineDto.Latitude,
                Longitude = turbineDto.Longitude,
                WindFarmId = turbineDto.WindFarmId
            };
            return _turbineRepository.CreateTurbine(turbine);
        }

        public List<TurbineDto> GetAllTurbines()
        {
            var turbines = _turbineRepository.GetAllTurbines();

            
            return turbines.Select(t => new TurbineDto
            {
                TurbineId = t.TurbineId,
                Name = t.Name,
                Model = t.Model,
                WindFarmId = t.WindFarmId
            }).ToList();
        }
    }
}