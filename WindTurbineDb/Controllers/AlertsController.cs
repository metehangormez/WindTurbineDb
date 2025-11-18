using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Alerts; 
using WindTurbine.Entities;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IAlertService _alertService;
        public AlertsController(IAlertService alertService) { _alertService = alertService; }

        [HttpGet]
        public IActionResult GetAll()
        {
           
            var alerts = _alertService.GetAllAlerts();
            return Ok(alerts);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AlertCreateDto alertDto) 
        {
            var createdAlert = _alertService.CreateAlert(alertDto);
            
            return CreatedAtAction(nameof(GetAll), new { id = createdAlert.AlertId }, createdAlert);
        }
    }
}