using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Turbines; 
using WindTurbine.Entities;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurbinesController : ControllerBase
    {
        private readonly ITurbineService _turbineService;
        public TurbinesController(ITurbineService turbineService) { _turbineService = turbineService; }

        [HttpGet]
        public IActionResult GetAll()
        {
           
            var turbines = _turbineService.GetAllTurbines();
            return Ok(turbines);
        }

        [HttpPost]
      
        public IActionResult Create([FromBody] TurbineCreateDto turbineDto)
        {
            var createdTurbine = _turbineService.CreateTurbine(turbineDto);
            
            return Ok(createdTurbine);
        }
    }
}