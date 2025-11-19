using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Context; 
using WindTurbine.Entities; 
using WindTurbine.DTOs.Alerts;

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

            if (!File.Exists(csvFilePath)) { _logger.LogError("Dosya yok!"); return; }

            using var reader = new StreamReader(csvFilePath);
            string? headerLine = await reader.ReadLineAsync();
            var headers = headerLine?.Split(';');

            // Sütun Ýndekslerini Bul
            int statusIndex = Array.IndexOf(headers, "status_type_id");
            int powerIndex = Array.IndexOf(headers, "power_29_avg"); 
            int windIndex = Array.IndexOf(headers, "wind_speed_3_avg"); 
            int timeIndex = Array.IndexOf(headers, "time_stamp");

            _logger.LogInformation("SCADA Veri Akýþý Baþlýyor...");

            while (!stoppingToken.IsCancellationRequested && !reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var values = line.Split(';');

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    
                    var dbContext = scope.ServiceProvider.GetRequiredService<WindTurbineDbContext>();
                    var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

                   
                    string statusStr = values[statusIndex];
                    double power = double.Parse(values[powerIndex].Replace('.', ',')); 
                    double wind = double.Parse(values[windIndex].Replace('.', ','));

                    
                    string statusText = (statusStr == "5") ? "Arýzalý" : "Aktif";
                    if (statusStr == "4") statusText = "Bakýmda";

                    
                    var turbine = dbContext.Turbines.FirstOrDefault(t => t.TurbineId == 1);
                    if (turbine != null)
                    {
                        turbine.CurrentPower = power;
                        turbine.CurrentWindSpeed = wind;
                        turbine.LastStatus = statusText;

                        dbContext.SaveChanges(); 
                       
                    }

                    
                    if (statusStr == "5")
                    {
                        alertService.CreateAlert(new AlertCreateDto
                        {
                            Timestamp = DateTime.UtcNow,
                            Message = $"KRÝTÝK ARIZA (Kod: 5)",
                            Severity = 3,
                            Status = "Yeni",
                            TurbineId = 1
                        });
                        _logger.LogWarning("!!! ALARM OLUÞTURULDU !!!");
                        await Task.Delay(200000, stoppingToken); 
                    }
                }

               
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}