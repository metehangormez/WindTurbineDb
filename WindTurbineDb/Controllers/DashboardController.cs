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

            // 1. GERÇEK VERİLERİ ÇEK
            var alerts = _alertService.GetAllAlerts();
            var turbines = _turbineService.GetAllTurbines();

            // Kart: Riskli Uyarılar
            dto.RiskliUyariSayisi = alerts.Count(x => x.Severity >= 3);

            // Kart: Toplam Türbin
            dto.ToplamTurbinSayisi = turbines.Count;

            // Kart: Aktif Türbin Sayısı (Burada da Bakımdakileri dahil edelim mi? 
            // Genelde 'Aktif' denince üretime katkı verenler anlaşılır, o yüzden dahil ediyorum)
            dto.AktifTurbinSayisi = turbines.Count(t =>
                t.LastStatus != "Arızalı" && t.LastStatus != "5");

            dto.ToplamOneriSayisi = 89;

            // --- GÜNCELLENEN KISIM: GÜÇ TOPLAMI ---
            // Mantık: Arızalı OMAYAN her şeyi topla (Aktif + Bakımda)
            double anlikToplamGuc = turbines
                .Where(t => t.LastStatus != "Arızalı" && t.LastStatus != "5")
                .Sum(t => t.CurrentPower);

            dto.ToplamUretimMW = Math.Round(anlikToplamGuc, 2);
            // --------------------------------------

            dto.VerimlilikYuzdesi = 98.2;

            // 2. GRAFİK VERİLERİ (Simülasyonun tabanı da güncellenmiş toplama göre olacak)
            dto.SaatEtiketleri = new List<string> { "00:00", "04:00", "08:00", "12:00", "16:00", "20:00", "23:59" };

            var random = new Random();
            var historyData = new List<double>();
            var expectedData = new List<double>();

            for (int i = 0; i < 7; i++)
            {
                double simValue = anlikToplamGuc * (0.8 + (random.NextDouble() * 0.4));
                if (simValue < 0) simValue = 0;
                historyData.Add(Math.Round(simValue, 1));

                double expValue = anlikToplamGuc * 1.1;
                expectedData.Add(Math.Round(expValue, 1));
            }

            if (historyData.Count > 0)
            {
                historyData[historyData.Count - 1] = dto.ToplamUretimMW;
            }

            dto.GerceklesUretimSerisi = historyData;
            dto.BeklenenUretimSerisi = expectedData;

            return Ok(dto);
        
    }
    }
}