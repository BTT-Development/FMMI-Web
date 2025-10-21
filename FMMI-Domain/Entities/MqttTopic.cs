using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class MqttTopic : Concurrency
{
    public string Topic { get; set; }
    public string Description { get; set; }

    public int MqttPubSubId { get; set; }
    public MqttPubSub MqttPubSub { get; set; }

    public int? DeviceId { get; set; }
    public List<Device>? Devices { get; set; }

    public int? AlarmId { get; set; }
    public List<Alarm>? Alarm { get; set; }
}
