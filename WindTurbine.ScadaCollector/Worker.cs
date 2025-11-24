using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.DTOs.Alerts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WindTurbine.ScadaCollector
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private const string CSV_FILE_NAME = "73.csv"; // Projenizde bu dosyanýn olduðundan emin olun

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string csvFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CSV_FILE_NAME);

            if (!File.Exists(csvFilePath))
            {
                _logger.LogError($"DOSYA BULUNAMADI: {csvFilePath}");
                return;
            }

            _logger.LogInformation($"--- SCADA CANLI VERÝ AKIÞI BAÞLADI: {CSV_FILE_NAME} ---");

            using var reader = new StreamReader(csvFilePath);
            string? headerLine = await reader.ReadLineAsync(); // Baþlýðý oku ve atla
            var headers = headerLine?.Split(';');

            if (headers == null) return;

            // CSV Sütun Eþleþtirmesi (Mapping)
            int statusIndex = Array.IndexOf(headers, "status_type_id");
            int powerIndex = Array.IndexOf(headers, "power_29_avg");
            int windIndex = Array.IndexOf(headers, "wind_speed_3_avg");

            // Detay Veriler için Eþleþtirme (CSV'deki sensör sütunlarýný kullanýyoruz)
            int pitchIndex = Array.IndexOf(headers, "sensor_0_avg");   // Pitch
            int vibIndex = Array.IndexOf(headers, "sensor_1_avg");     // Titreþim
            int hubTempIndex = Array.IndexOf(headers, "sensor_2_avg"); // Hub Isýsý
            int gearTempIndex = Array.IndexOf(headers, "sensor_3_avg");// Þanzýman Isýsý
            int genTempIndex = Array.IndexOf(headers, "sensor_4_avg"); // Jeneratör Isýsý
            int genRpmIndex = Array.IndexOf(headers, "sensor_5_avg");  // RPM

            while (!stoppingToken.IsCancellationRequested && !reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var values = line.Split(';');

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<WindTurbineDbContext>();
                    var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

                    // 1. Deðerleri Oku ve Dönüþtür (Helper fonksiyon)
                    double Parse(int index)
                    {
                        if (index == -1 || index >= values.Length) return 0;
                        // Nokta/Virgül hatasýný önlemek için Replace kullanýyoruz
                        return double.TryParse(values[index].Replace('.', ','), out double v) ? v : 0;
                    }

                    string statusCode = values[statusIndex];
                    double power = Parse(powerIndex);
                    double wind = Parse(windIndex);

                    // 2. Veritabanýný Güncelle (ID: 1 olan türbin için)
                    var turbine = dbContext.Turbines.FirstOrDefault(t => t.TurbineId == 1);
                    if (turbine != null)
                    {
                        // Ana Veriler
                        turbine.CurrentPower = power;
                        turbine.CurrentWindSpeed = wind;

                        // Detay Veriler (CSV'den gelen ham veriyi biraz ölçekliyoruz ki gerçekçi dursun)
                        turbine.PitchAngle = Math.Abs(Parse(pitchIndex));
                        turbine.BladeVibration = Math.Abs(Parse(vibIndex));
                        turbine.HubTemperature = Math.Abs(Parse(hubTempIndex)) * 2; // Örn: 20 yerine 40 derece görünsün
                        turbine.GearboxOilTemp = Math.Abs(Parse(gearTempIndex)) * 2;
                        turbine.GeneratorTemp = Math.Abs(Parse(genTempIndex)) * 2;
                        turbine.GeneratorRPM = Math.Abs(Parse(genRpmIndex)) * 100;

                        // CSV'de olmayanlarý diðerlerinden türetelim (Simülasyon)
                        turbine.GearboxVibration = turbine.BladeVibration / 1.5;
                        turbine.MainBearingTemp = turbine.GearboxOilTemp * 0.9;
                        turbine.TransformerTemp = turbine.GeneratorTemp * 0.8;

                        // Durum Metni
                        if (statusCode == "1" || statusCode == "0") turbine.LastStatus = "Aktif";
                        else if (statusCode == "5") turbine.LastStatus = "Arýzalý";
                        else if (statusCode == "4") turbine.LastStatus = "Bakýmda";
                        else turbine.LastStatus = "Beklemede";

                        dbContext.SaveChanges(); // SQL'e yaz
                    }

                    // 3. Alarm Üret (Sadece Arýza Durumunda)
                    if (statusCode == "5")
                    {
                        // Çok sýk alarm üretmemek için basit bir kontrol eklenebilir
                        alertService.CreateAlert(new AlertCreateDto
                        {
                            Timestamp = DateTime.UtcNow,
                            Message = $"KRÝTÝK ARIZA (Kod: 5) - Titreþim: {turbine?.BladeVibration:F2}",
                            Severity = 3,
                            Status = "Yeni",
                            TurbineId = 1
                        });

                        // Arýza durumunda akýþý yavaþlat ki kullanýcý görebilsin
                        await Task.Delay(2000, stoppingToken);
                    }
                }

                // Akýþ Hýzý (100ms = 10x Gerçek Zaman)
                await Task.Delay(100, stoppingToken);
            }
        }
    }
}