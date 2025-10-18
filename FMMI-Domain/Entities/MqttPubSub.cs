using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class MqttPubSub : BaseIdEntity
{
    public string Name { get; set; }
}
