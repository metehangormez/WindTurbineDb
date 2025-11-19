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

            // --- 1. VERİTABANINDAN GERÇEK VERİLERİ ÇEK ---
            var alerts = _alertService.GetAllAlerts();
            var turbines = _turbineService.GetAllTurbines();

            // Kart: Riskli Uyarılar
            dto.RiskliUyariSayisi = alerts.Count(x => x.Severity >= 3);

            // Kart: Toplam Türbin
            dto.ToplamTurbinSayisi = turbines.Count;

            // Kart: Aktif Türbinler (Sadece 'Aktif' durumunda olanlar)
            dto.AktifTurbinSayisi = turbines.Count(t => t.LastStatus == "Aktif");

            // Kart: AI Önerileri (Şimdilik sabit, AI servisi bağlanınca değişecek)
            dto.ToplamOneriSayisi = 89;

            // Kart: Günlük Üretim (Tüm türbinlerin o anki güçlerinin toplamı)
            // Worker servisi veritabanını güncelledikçe bu sayı değişecek.
            double anlikToplamGuc = turbines.Sum(t => t.CurrentPower);
            dto.ToplamUretimMW = Math.Round(anlikToplamGuc, 2);

            // Kart: Verimlilik (Şimdilik sabit bir değer veya basit bir oran)
            dto.VerimlilikYuzdesi = 98.2;


            // --- 2. GRAFİK VERİLERİ (YARI GERÇEK SİMÜLASYON) ---
            // Gerçek bir "Geçmiş Üretim Tablosu" olmadığı için, 
            // anlık gücü baz alarak mantıklı bir geçmiş grafiği üretiyoruz.

            dto.SaatEtiketleri = new List<string> { "00:00", "04:00", "08:00", "12:00", "16:00", "20:00", "23:59" };

            var random = new Random();
            var historyData = new List<double>();
            var expectedData = new List<double>();

            for (int i = 0; i < 7; i++)
            {
                // Gerçekleşen: Anlık toplama yakın rastgele dalgalanma (+-%20)
                double simValue = anlikToplamGuc * (0.8 + (random.NextDouble() * 0.4));
                if (simValue < 0) simValue = 0;
                historyData.Add(Math.Round(simValue, 1));

                // Beklenen: İdeal üretim (Anlık güçten biraz daha fazla)
                double expValue = anlikToplamGuc * 1.1;
                expectedData.Add(Math.Round(expValue, 1));
            }

            // Grafiğin son noktası, kartta yazan "Gerçek Değer" ile aynı olsun
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