
using FMMI_Service.DTO.Alarm;
using FMMI_Service.Result;

namespace FMMI_Service.Services.AlarmServices;

public interface IAlarmLogService
{
    Task<Result<List<ShowAlarmLogDTO>>> GetAllActiveAlarmlogs();
    Task<Result<int>> GetCountOnActiceAlarmlogs();

    Task<Result<List<ShowAlarmLogDTO>>> GetAllAlarmLogByDeviceIdAsync(int deviceId);
}
