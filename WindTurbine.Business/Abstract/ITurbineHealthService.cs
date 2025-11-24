using System;
using WindTurbine.DTOs.Turbine;

namespace WindTurbine.Business.Abstract
{
    public interface ITurbineHealthService
    {
        
        TurbineHealthDto? GetTurbineHealth(int turbineId, int lastDays = 7);
    }
}
