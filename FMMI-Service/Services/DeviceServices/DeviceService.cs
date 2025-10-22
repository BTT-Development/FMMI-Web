using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Mapping.Device;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using MQTTnet.Client;
using Microsoft.AspNetCore.SignalR.Client;



namespace FMMI_Service.Services.DeviceServices;

internal class DeviceService : BaseService<Device>, IDeviceService
{
    private FMMIContext _context;

    private IMqttClient _mqttClient;
    
    private readonly MqttClientOptionsBuilder _mqttClientOptionsBuilder;

    private HubConnection _hubConnection;

    public DeviceService(FMMIContext context, IMqttClient mqttClient, MqttClientOptionsBuilder mqttClientOption) : base(context)
    {
        _context = context;
        _mqttClient = mqttClient;
        _mqttClientOptionsBuilder = mqttClientOption;
        _hubConnection = new HubConnectionBuilder()
          .WithUrl("http://localhost:5147/alarmHub")
          .Build();
    }
    public Result<List<ShowDeviceDTO>> GetDevicesByMachineId(int id)
    {
        List<ShowDeviceDTO> devices = new();
        devices = _context.Devices.Include(x => x.MqttTopics.Where(x => x.Description == "status")).Include(x => x.DeviceType).Where(x => x.MachineId == id).MapDeviceToDTO().ToList();
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

    public async Task<Result<bool>> GetOnlineStatusByDeviceId(int id)
    {
        bool status = await _context.Alarms.Include(x => x.AlarmLogs).Where(x => x.DeviceId == id && x.Name == "Status").Select(x => x.AlarmLogs.OrderByDescending(x => x.Dates).Select(x => x.NewAlarm).FirstOrDefault()).FirstOrDefaultAsync();
      
        return Result<bool>.Succes(status, "status");
    }

    //public Result<ShowDeviceDTO> CreateDevice(ShowDeviceDTO deviceDTO) => base.CreateAsync(deviceDTO);
}
