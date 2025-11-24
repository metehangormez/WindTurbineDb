using WindTurbine.Business.Abstract;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DTOs.Turbines;
using WindTurbine.Entities;
using System.Linq;
using System.Collections.Generic;

namespace WindTurbine.Business.Concrete
{
    public class TurbineManager : ITurbineService
    {
        private readonly ITurbineRepository _turbineRepository;

        public TurbineManager(ITurbineRepository turbineRepository)
        {
            _turbineRepository = turbineRepository;
        }

        public Turbine CreateTurbine(TurbineCreateDto turbineDto)
        {
            var turbine = new Turbine
            {
                Name = turbineDto.Name,
                Model = turbineDto.Model,
                Latitude = turbineDto.Latitude,
                Longitude = turbineDto.Longitude,
                WindFarmId = turbineDto.WindFarmId,
                LastStatus = turbineDto.InitialStatus
            };
            return _turbineRepository.CreateTurbine(turbine);
        }

        public void UpdateTurbine(TurbineDto turbineDto)
        {
            var existing = _turbineRepository.GetTurbineById(turbineDto.TurbineId);
            if (existing != null)
            {
                existing.Name = turbineDto.Name;
                existing.Model = turbineDto.Model;
                existing.WindFarmId = turbineDto.WindFarmId;
                // Not: Canlı verileri (Power, Wind vb.) burada güncellemiyoruz, 
                // onları Worker servisi güncelliyor.

                _turbineRepository.UpdateTurbine(existing);
            }
        }

        public void DeleteTurbine(int id)
        {
            _turbineRepository.DeleteTurbine(id);
        }

        public List<TurbineDto> GetAllTurbines()
        {
            var turbines = _turbineRepository.GetAllTurbines();

            // --- BURASI EN ÖNEMLİ KISIM ---
            // Veritabanındaki Entity'i, API DTO'suna dönüştürüyoruz.
            return turbines.Select(t => new TurbineDto
            {
                TurbineId = t.TurbineId,
                Name = t.Name,
                Model = t.Model,
                WindFarmId = t.WindFarmId,
                Latitude = t.Latitude,
                Longitude = t.Longitude,

                // Canlı Ana Veriler
                CurrentPower = t.CurrentPower,
                CurrentWindSpeed = t.CurrentWindSpeed,
                LastStatus = t.LastStatus,

                // Detaylı Sensör Verileri (Bunları eklemeyi unutursak ekranda 0 görünür)
                PitchAngle = t.PitchAngle,
                BladeVibration = t.BladeVibration,
                HubTemperature = t.HubTemperature,
                GearboxOilTemp = t.GearboxOilTemp,
                GearboxVibration = t.GearboxVibration,
                GeneratorTemp = t.GeneratorTemp,
                GeneratorRPM = t.GeneratorRPM,
                MainBearingTemp = t.MainBearingTemp,
                TransformerTemp = t.TransformerTemp

            }).ToList();
        }
    }
}