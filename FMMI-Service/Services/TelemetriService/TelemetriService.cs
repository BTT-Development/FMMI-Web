using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Data;
using FMMI_Service.Mapping.Data;
using FMMI_Service.Result;
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

        public List<Data> GetData()
        {
            return _context.TelemetriData.Include(x => x.DataType).ToList();
        }

        public async Task<Result<List<DataDTO>>> GetDataByDeviceIdAsync(int deviceId)
        {
            return Result<List<DataDTO>>.Succes(await _context.TelemetriData.Include(x => x.DataType).Where(t => t.Device.Id == deviceId)
                .MapDeviceToDTO().ToListAsync(), "Data fundet.");
        }

        public async Task<List<DataDTO>> GetRealTidsDataAsync(MqttTopic topic)
        {
            return await _context.TelemetriData.Include(x => x.DataType).Where(t => t.Device.MqttTopics.Any(p => p.Topic == topic.Topic.ToString()))
                .MapDeviceToDTO().ToListAsync();
        }

        public async Task InsertData(Data data)
        {
            await _context.TelemetriData.AddAsync(data);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
