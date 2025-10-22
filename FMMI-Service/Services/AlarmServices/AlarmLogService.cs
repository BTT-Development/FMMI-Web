using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Alarm;
using FMMI_Service.Mapping.Alarm;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;


namespace FMMI_Service.Services.AlarmServices;

internal class AlarmLogService : BaseService<AlarmLogs>, IAlarmLogService
{
    private FMMIContext _context;
    public AlarmLogService(FMMIContext context) : base(context)
    {
       _context = context; 
    }
    public async Task<Result<List<ShowAlarmLogDTO>>> GetAllActiveAlarmlogs()
    {
        List<ShowAlarmLogDTO> alarmlogs = new();
        alarmlogs = await _context.AlarmLogs
            .Include(x => x.Alarmer)
            .ThenInclude(x => x.Device)
            .ThenInclude(x => x.Machine)
            .ThenInclude(x => x.Locations)
            .Where(x => x.NewAlarm == true)
            .MapAlarmLogToDTO()
            .ToListAsync();
        return Result<List<ShowAlarmLogDTO>>.Succes(alarmlogs, "data fundet.");
    }

    public async Task<Result<int>> GetCountOnActiceAlarmlogs()
    {
        int count = await _context.AlarmLogs.Where(x => x.NewAlarm == true).CountAsync();
        if (count > 0)
        {
            return Result<int>.Succes(count, $"Der er {count} alarmer.");
        }
        return Result<int>.Fail("Ingen alarmer.");
        
    }

}
