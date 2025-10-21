using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class Alarm : Concurrency
{
    public string Name { get; set; }
    public double? Value { get; set; }
    public string Description { get; set; }

    public int? MqttTopicId { get; set; }
    public MqttTopic? Topics { get; set; }

    public int DeviceId { get; set; }
    public Device Device { get; set; }

    public List<AlarmLogs> AlarmLogs { get; set; }
    
}
