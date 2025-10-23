using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Result;

namespace FMMI_Service.Services.DeviceServices;

public interface IDeviceService
{
    Task<Result<List<ShowDeviceDTO>>> GetDevicesByMachineId(int id);
    Task<Result<ShowDeviceDTO>> GetDeviceById(int id);
    Result<Device> CreateDevice(ShowDeviceDTO deviceDTO);
    Result<Device> UpdateDevice(ShowDeviceDTO deviceDTO);
    Result<bool> DeleteDevice(int id);
    Task<Result<bool>> GetOnlineStatusByDeviceId(int id);
}
