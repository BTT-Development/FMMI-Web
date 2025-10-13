using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Service.Services.TelemetriService
{
    internal class TelemetriService : BaseService<TelemetriData>
    {
        private readonly FMMIContext _context;
        public TelemetriService(FMMIContext context) : base(context)
        {
                _context = context;
        }

        public async Task<List<TelemetriData>> GetTelemetriDataAsync()
        {
            return await _context.Telemetri.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<TelemetriData?> GetTelemetriDataByIdAsync(int id)
        {
            return await _context.Telemetri.FirstOrDefaultAsync(t => t.TelemetriDataID == id);
        }

        public async Task<List<TelemetriData>> GetTelemetriDataByDeviceIdAsync(int deviceId)
        {
            return await _context.Telemetri.Where(t => t.DeviceID == deviceId).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task<List<TelemetriData>> GetTelemetriDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Telemetri.Where(t => t.Date >= startDate && t.Date <= endDate).OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task InsertTelemetriData(TelemetriData data)
        {
            await _context.Telemetri.AddAsync(data);
        }
    }
}
