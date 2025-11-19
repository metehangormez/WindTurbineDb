using Microsoft.AspNetCore.Mvc;
using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Dashboard;

namespace WindTurbineDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IAlertService _alertService;
        private readonly ITurbineService _turbineService;

        public DashboardController(IAlertService alertService, ITurbineService turbineService)
        {
            _alertService = alertService;
            _turbineService = turbineService;
        }

        [HttpGet]
        public IActionResult GetDashboardSummary()
        {
            var dto = new DashboardDto();

            // 1. GERÇEK VERİLER (Veritabanından)
            var alerts = _alertService.GetAllAlerts();
            var turbines = _turbineService.GetAllTurbines();

            dto.RiskliUyariSayisi = alerts.Count(x => x.Severity >= 3);
            dto.ToplamOneriSayisi = 89; // (Yapay zeka servisi gelince burası da dinamik olacak)
            dto.AktifTurbinSayisi = turbines.Count;

            // 2. SİMÜLASYON VERİLERİ (Şimdilik elle, sonra veritabanından)
            dto.ToplamUretimMW = 625.4;
            dto.VerimlilikYuzdesi = 98.2;

            dto.SaatEtiketleri = new List<string> { "00:00", "04:00", "08:00", "12:00", "16:00", "20:00", "23:59" };
            dto.GerceklesUretimSerisi = new List<double> { 40, 25, 60, 85, 90, 75, 50 };
            dto.BeklenenUretimSerisi = new List<double> { 35, 30, 55, 80, 85, 80, 55 };

            return Ok(dto);
        }
    }
}