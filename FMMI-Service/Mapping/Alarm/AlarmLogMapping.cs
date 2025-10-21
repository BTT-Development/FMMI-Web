using FMMI_Domain.Entities;
using FMMI_Service.DTO.Alarm;

namespace FMMI_Service.Mapping.Alarm;

public static class AlarmLogMapping
{
    public static IQueryable<ShowAlarmLogDTO> MapAlarmLogToDTO(this IQueryable<AlarmLogs> quere)
    {
        return quere.Select(x => new ShowAlarmLogDTO
        {
            Id = x.Id,
            Date = x.Dates,
            Description = x.Alarmer.Description,
            Machine = x.Alarmer.Device.Machine.MachineName,
            Location = x.Alarmer.Device.Machine.Locations.Name,
            DeviceName = x.Alarmer.Device.Name
        });
    }
}
