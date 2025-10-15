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
        Task<List<Data>> GetTemperatureDataAsync();
        Task<List<Data>> GetTemperatureDataByDeviceIdAsync(int deviceId);
        Task InsertTemperatureData(Data data);
        #endregion

     
    }
}
