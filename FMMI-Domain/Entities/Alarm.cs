using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class Alarm : BaseIdEntity
{
    public string Name { get; set; }
    public double Value { get; set; }
    public string Description { get; set; }
    public string Topics { get; set; }

    public List<AlarmLogs> AlarmLogs { get; set; }
}
