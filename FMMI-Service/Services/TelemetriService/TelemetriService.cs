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

        #region Data methods
        public async Task<List<Data>> GetDataAsync()
        {
            return await _context.TelemetriData.ToListAsync();
        }

        public async Task<List<Data>> GetDataByDeviceIdAsync(int deviceId)
        {
            return await _context.TelemetriData.Where(t => t.Device.Id == deviceId).ToListAsync();
        }

        public async Task<List<Data>> GetDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TelemetriData
                .Where(t => t.Dates >= startDate && t.Dates <= endDate)
                .ToListAsync();
        }


        public async Task InsertData(Data data)
        {
            await _context.TelemetriData.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
