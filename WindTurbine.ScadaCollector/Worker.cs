using WindTurbine.Business.Abstract;
using WindTurbine.DTOs.Alerts;
using System.Text;

namespace WindTurbine.ScadaCollector
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private const string CSV_FILE_NAME = "73.csv";

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

            _logger.LogInformation($"--- SCADA SÝMÜLASYONU BAÞLADI: {CSV_FILE_NAME} ---");

            using var reader = new StreamReader(csvFilePath);
            string? headerLine = await reader.ReadLineAsync(); 

            int satirSayaci = 0; 

            while (!stoppingToken.IsCancellationRequested && !reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                satirSayaci++; 

               
                bool sahteAnomaliVar = (satirSayaci % 100 == 0);

                if (sahteAnomaliVar)
                {
                    _logger.LogWarning($"!!! TEST ANOMALÝSÝ ÜRETÝLDÝ !!! (Satýr: {satirSayaci})");

                    // Veritabanýna Kaydet
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();
                        try
                        {
                            alertService.CreateAlert(new AlertCreateDto
                            {
                                Timestamp = DateTime.UtcNow,
                                Message = $"SCADA Test Anomalisi (Satýr: {satirSayaci})",
                                Severity = 2,
                                Confidence = 0.85,
                                Status = "Yeni",
                                TurbineId = 1
                            });
                            _logger.LogInformation("-> DB'ye yazýldý.");
                        }
                        catch (Exception ex) { _logger.LogError(ex.Message); }
                    }

                    
                    await Task.Delay(2000000, stoppingToken);
                }
                else
                {
                   
                }
            }
        }
    }
}