using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Result;

namespace FMMI_Service.Services.DeviceServices;

public interface IDeviceService
{
    Task<Result<List<ShowDeviceDTO>>> GetDevicesByMachineId(int id);
    Task<Result<ShowDeviceDTO>> GetDeviceById(int id);
    Task<Result.Result> CreateDeviceAsync(CreateDeviceDTO deviceDTO);
    Result<Device> UpdateDevice(ShowDeviceDTO deviceDTO);
    Task<Result.Result> DeleteDevice(int id);
    Task<Result<bool>> GetOnlineStatusByDeviceId(int id);
}
