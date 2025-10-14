using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Service.Services.TelemetriService
{
    internal class TelemetriService : ITelemetriService
    {
        private readonly FMMIContext _context;
        public TelemetriService(FMMIContext context)
        {
             _context = context;
        }

        #region Temperature methods

        public async Task<List<Temp>> GetTemperatureDataAsync()
        {
            return await _context.Temperature.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<List<Temp>> GetTemperatureDataByDeviceIdAsync(int deviceId)
        {
            return await _context.Temperature.Where(t => t.Devices.Id == deviceId).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task InsertTemperatureData(Temp data)
        {
            await _context.Temperature.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Humidity methods
        public async Task<List<Hum>> GetHumidityDataAsync()
        {
            return await _context.Humidity.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<List<Hum>> GetHumidityDataByDeviceIdAsync(int deviceId)
        {
            return await _context.Humidity.Where(t => t.Devices.Id == deviceId).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task InsertHumidityData(Hum data)
        {
            await _context.Humidity.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
