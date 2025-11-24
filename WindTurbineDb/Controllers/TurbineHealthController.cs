using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Turbine;

namespace WindTurbineDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurbineHealthController : ControllerBase
    {
        private readonly ITurbineHealthService _turbineHealthService;

        public TurbineHealthController(ITurbineHealthService turbineHealthService)
        {
            _turbineHealthService = turbineHealthService;
        }

        [HttpGet("{turbineId}")]
        public ActionResult<TurbineHealthDto> Get(int turbineId, [FromQuery] int lastDays = 7)
        {
            var dto = _turbineHealthService.GetTurbineHealth(turbineId, lastDays);
            if (dto == null)
                return NotFound();

            return Ok(dto);
        }
    }
}
