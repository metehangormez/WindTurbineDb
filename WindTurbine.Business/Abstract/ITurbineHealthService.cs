using System;
using WindTurbine.DTOs.Turbine;

namespace WindTurbine.Business.Abstract
{
    public interface ITurbineHealthService
    {
        /// <summary>
        /// Belirli bir türbin için son X gün içindeki alarmlara bakarak
        /// alt sistem sağlık yüzdelerini hesaplar.
        /// </summary>
        TurbineHealthDto? GetTurbineHealth(int turbineId, int lastDays = 7);
    }
}
