using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Data;
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
            return await _context.TelemetriData.Include(x=> x.DataType).ToListAsync();
        }

        public async Task<List<Data>> GetDataByDeviceIdAsync(int deviceId)
        {
            return await _context.TelemetriData.Where(t => t.Device.Id == deviceId).ToListAsync();
        }

        public async Task<List<Data>> GetRealTidsDataAsync(MqttTopic topic)
        {
            return await _context.TelemetriData.Include(x => x.DataType).Where(t => t.Device.MqttTopics.Any(p => p.Topic == topic.Topic.ToString())).ToListAsync();
        }

        public async Task InsertData(Data data)
        {
            await _context.TelemetriData.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Realtidsdata

        public async Task<DataDTO> GetLatestRealtimeDataAsync()
        {
            var latestTemp = await _context.TelemetriData
                .Include(d => d.DataType)
                .Where(d => d.DataType.TypeName == "Temperature")
                .OrderByDescending(d => d.Dates)
                .FirstOrDefaultAsync();

            var latestHumidity = await _context.TelemetriData
                .Include(d => d.DataType)
                .Where(d => d.DataType.TypeName == "Humidity")
                .OrderByDescending(d => d.Dates)
                .FirstOrDefaultAsync();

            return new DataDTO
            {
                Temperature = latestTemp?.Value ?? 0,
                Humidity = latestHumidity?.Value ?? 0
            };
        }
        #endregion
    }
}
