using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Service.Services.TelemetriService
{
    public interface ITelemetriService
    {
        #region Temperature methods
        Task<List<Temp>> GetTemperatureDataAsync();
        Task<List<Temp>> GetTemperatureDataByDeviceIdAsync(int deviceId);
        Task InsertTemperatureData(Temp data);
        #endregion

        #region Humidity methods
        Task<List<Hum>> GetHumidityDataAsync();
        Task<List<Hum>> GetHumidityDataByDeviceIdAsync(int deviceId);
        Task InsertHumidityData(Hum data);
        #endregion
    }
}
