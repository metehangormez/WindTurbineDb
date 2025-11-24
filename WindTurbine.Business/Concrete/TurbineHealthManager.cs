using System;
using System.Linq;
using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.Turbine;
using WindTurbine.Entities;

namespace WindTurbine.Business.Concrete
{
    public class TurbineHealthManager : ITurbineHealthService
    {
        private readonly ITurbineRepository _turbineRepository;
        private readonly IAlertRepository _alertRepository;

        public TurbineHealthManager(ITurbineRepository turbineRepository,
                                    IAlertRepository alertRepository)
        {
            _turbineRepository = turbineRepository;
            _alertRepository = alertRepository;
        }

        public TurbineHealthDto? GetTurbineHealth(int turbineId, int lastDays = 7)
        {
            var turbine = _turbineRepository.GetTurbineById(turbineId);
            if (turbine == null) return null;

            var fromDate = DateTime.UtcNow.AddDays(-lastDays);

            // Gerçek VERİ: DB'deki bu türbine ait son X günlük alarmlar
            var alerts = _alertRepository
                .GetAllAlerts()
                .Where(a => a.TurbineId == turbineId && a.Timestamp >= fromDate)
                .ToList();

            double Blades() => CalculateHealth(alerts, "blade", "kanat", "pitch");
            double Hub() => CalculateHealth(alerts, "hub");
            double PitchSystem() => CalculateHealth(alerts, "pitch");
            double Generator() => CalculateHealth(alerts, "generator", "jeneratör");
            double Gearbox() => CalculateHealth(alerts, "gearbox", "dişli", "dişli kutusu");
            double Ups() => CalculateHealth(alerts, "ups", "akü", "battery");
            double WindDetail() => CalculateHealth(alerts, "rüzgar", "wind speed", "anemometre");
            double YawSystem() => CalculateHealth(alerts, "yaw");
            double Tower() => CalculateHealth(alerts, "tower", "kule");
            double Transformer() => CalculateHealth(alerts, "trafo", "transformer");
            double ConverterInv() => CalculateHealth(alerts, "converter", "inverter", "rectifier");
            double Circuit() => CalculateHealth(alerts, "şalter", "breaker", "switchgear");

            return new TurbineHealthDto
            {
                TurbineId = turbine.TurbineId,
                TurbineName = turbine.Name,

                Blades = Blades(),
                Hub = Hub(),
                PitchSystem = PitchSystem(),

                Generator = Generator(),
                Gearbox = Gearbox(),
                Ups = Ups(),

                WindDetail = WindDetail(),
                YawSystem = YawSystem(),
                Tower = Tower(),

                Transformer = Transformer(),
                ConverterInverter = ConverterInv(),
                Circuit = Circuit()
            };
        }

        /// <summary>
        /// Sağlık hesabı:
        /// - ilgili keyword içeren alarmları al
        /// - severity toplamına göre 100'den puan düş
        /// - alt sınır 60, üst sınır 100
        /// Böylece "0 alarm" durumunda ≈ 98-100, çok alarm varsa 60 civarı.
        /// </summary>
        private static double CalculateHealth(
            System.Collections.Generic.IEnumerable<Alert> allAlerts,
            params string[] keywords)
        {
            var relevant = allAlerts
                .Where(a => keywords.Any(k =>
                    a.Message?.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0));

            if (!relevant.Any())
                return 98; // Bu parçayla ilgili hiç alarm yoksa neredeyse tam sağlıklı kabul et.

            var severitySum = relevant.Sum(a => a.Severity); // 1–5 arası

            var raw = 100 - severitySum * 3; // her severity puanı için 3 puan düş
            if (raw < 60) raw = 60;
            if (raw > 100) raw = 100;

            return raw;
        }
    }
}
