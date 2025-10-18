using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Mapping.Device;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Service.Services.DeviceServices;

internal class DeviceService : BaseService<Device>, IDeviceService
{
    private FMMIContext _context;
    public DeviceService(FMMIContext context) : base(context)
    {
        _context = context;
    }
    public Result<List<ShowDeviceDTO>> GetDevicesByMachineId(int id)
    {
        List<ShowDeviceDTO> devices = new();
        devices = _context.Devices.Include(x => x.MqttTopics.Where(x => x.Description == "status")).Include(x => x.DeviceType).Where(x => x.MachineId == id).MapDeviceToDTO().ToList();
        if (devices.Count > 0)
        {
            return Result<List<ShowDeviceDTO>>.Succes(devices, "Data fundet.");
        }
        return Result<List<ShowDeviceDTO>>.Fail("Data fundet.");
    }
}
