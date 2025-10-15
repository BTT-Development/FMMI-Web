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

        public async Task<List<Data>> GetTemperatureDataAsync()
        {
            return await _context.TelemetriData.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<List<Data>> GetTemperatureDataByDeviceIdAsync(int deviceId)
        {
            return await _context.TelemetriData.Where(t => t.Devices.Id == deviceId).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task InsertTemperatureData(Data data)
        {
            await _context.TelemetriData.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
