using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class MqttTopic : BaseIdEntity
{
    public string Topic { get; set; }
    public string Description { get; set; }

    public int MqttPubSubId { get; set; }
    public MqttPubSub MqttPubSub { get; set; }

    public int DeviceId { get; set; }
    public List<Device> Devices { get; set; }
}
