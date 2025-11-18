using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.WindFarms; 
using WindTurbine.Entities;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindFarmsController : ControllerBase
    {
        private readonly IWindFarmService _windFarmService;
        public WindFarmsController(IWindFarmService windFarmService) { _windFarmService = windFarmService; }

        [HttpGet]
        public IActionResult GetAll()
        {
            var windFarms = _windFarmService.GetAllWindFarms();
            return Ok(windFarms);
        }

        [HttpPost]
        public IActionResult Create([FromBody] WindFarmCreateDto windFarmDto)
        {
            var createdWindFarm = _windFarmService.CreateWindFarm(windFarmDto);
            return Ok(createdWindFarm);
        }
    }
}