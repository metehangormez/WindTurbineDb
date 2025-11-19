using Microsoft.AspNetCore.Mvc;
using WindTurbine.DataAccess.Context;
using WindTurbine.Entities;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly WindTurbineDbContext _context;

        public ReportsController(WindTurbineDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reports = _context.GeneratedReports
                .OrderByDescending(r => r.GeneratedDate)
                .Select(r => new
                {
                    r.Id,
                    r.Title,
                    r.GeneratedDate,
                    r.FileType,
                    r.FileSize
                })
                .ToList();
            return Ok(reports);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GeneratedReport report)
        {
            report.GeneratedDate = DateTime.UtcNow;
            _context.GeneratedReports.Add(report);
            _context.SaveChanges();
            return Ok(report);
        }
    }
}