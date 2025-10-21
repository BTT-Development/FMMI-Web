using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Alarm;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;


namespace FMMI_Service.Services.AlarmServices;

internal class AlarmLogService : BaseService<AlarmLogs>
{
    private FMMIContext _context;
    public AlarmLogService(FMMIContext context) : base(context)
    {
       _context = context; 
    }
    public Result<List<AlarmLogs>> GetAllActiveAlarmlogs()
    {
        List<ShowAlarmLogDTO> alarmlogs = new();

    } 
}
