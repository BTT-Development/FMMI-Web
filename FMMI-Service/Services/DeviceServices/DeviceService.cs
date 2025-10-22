using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Mapping.Device;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using MQTTnet.Client;

namespace FMMI_Service.Services.DeviceServices;

internal class DeviceService : BaseService<Device>, IDeviceService
{
    private FMMIContext _context;

    private IMqttClient _mqttClient;
    
    private readonly MqttClientOptionsBuilder _mqttClientOptionsBuilder;


    public DeviceService(FMMIContext context, IMqttClient mqttClient, MqttClientOptionsBuilder mqttClientOption) : base(context)
    {
        _context = context;
        _mqttClient = mqttClient;
        _mqttClientOptionsBuilder = mqttClientOption;
       
    }
    public async Task<Result<List<ShowDeviceDTO>>> GetDevicesByMachineId(int id)
    {
        List<ShowDeviceDTO> devices = new();
        devices = await _context.Devices.AsNoTracking()
            .Include(x => x.MqttTopics.Where(x => x.Description == "status"))
            .Include(x => x.DeviceType)
            .Where(x => x.MachineId == id)
            .MapDeviceToDTO().ToListAsync();
        if (devices.Count > 0)
        {
            foreach (var item in devices)
            {
                item.online = _context.Alarms.Include(x => x.AlarmLogs).Where(x => x.DeviceId == item.Id && x.Name == "Status").Select(x => x.AlarmLogs.OrderByDescending(x => x.Dates).Select(x => x.NewAlarm).FirstOrDefault()).FirstOrDefault();
            }
            return Result<List<ShowDeviceDTO>>.Succes(devices, "Data fundet.");
        }
        return Result<List<ShowDeviceDTO>>.Fail("Data fundet.");
    }

    public async  Task<Result<ShowDeviceDTO>> GetDeviceById(int id)
    {
        ShowDeviceDTO dto = _context.Devices.Where(x => x.Id == id).MapDeviceToDTO().FirstOrDefault();
        if (dto is not null)
        {
            return Result<ShowDeviceDTO>.Succes(dto, "Succes");
        }
        else
        {
            return Result<ShowDeviceDTO>.Fail("Fejl");
        }
    }

    public Result<Device> CreateDevice(ShowDeviceDTO deviceDTO)
    {
        Device device = deviceDTO.MapDTOtoDevice();
        _context.Devices.Add(device);
        return Result<Device>.Succes(device, "Device oprettet.");
    }

    public Result<Device> UpdateDevice(ShowDeviceDTO deviceDTO)
    {
        Device? device = _context.Devices.FirstOrDefault(x => x.Id == deviceDTO.Id);
        if (device == null)
        {
            return Result<Device>.Fail("Device ikke fundet.");
        }
        device = deviceDTO.MapDTOtoDevice();
        _context.Devices.Update(device);
        return Result<Device>.Succes(device, "Device opdateret.");
    }

    public Result<bool> DeleteDevice(int id)
    {
        Device? device = _context.Devices.FirstOrDefault(x => x.Id == id);
        if (device == null)
        {
            return Result<bool>.Fail("Device ikke fundet.");
        }
        _context.Devices.Remove(device);
        return Result<bool>.Succes(true, "Device slettet.");
    }
    public async Task<Result<bool>> GetOnlineStatusByDeviceId(int id)
    {
        bool status = await _context.Alarms.Include(x => x.AlarmLogs).Where(x => x.DeviceId == id && x.Name == "Status").Select(x => x.AlarmLogs.OrderByDescending(x => x.Dates).Select(x => x.NewAlarm).FirstOrDefault()).FirstOrDefaultAsync();
      
        return Result<bool>.Succes(status, "status");
    }
    //public async Task<Result> UpdateTimeInterval()
    //{

    //}

    //public Result<ShowDeviceDTO> CreateDevice(ShowDeviceDTO deviceDTO) => base.CreateAsync(deviceDTO);
}
