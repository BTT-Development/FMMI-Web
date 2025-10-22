using FMMI_Service.DTO.Device;
using FMMI_Service.Result;

namespace FMMI_Service.Services.DeviceServices;

public interface IDeviceService
{
    Result<List<ShowDeviceDTO>> GetDevicesByMachineId(int id);
    Task<Result<bool>> GetOnlineStatusByDeviceId(int id);
}
