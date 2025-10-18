using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class Device : BaseIdEntity
{
    public string Name { get; set; }

    #region Navigation propert
    public int MachineId { get; set; }
    public Machine Machine { get; set; }

    public int DeviceTypeID { get; set; }
    public DeviceType DeviceType { get; set; }

    public int AlarmID { get; set; }
    public List<Alarm>? Alarm { get; set; }

    public List<Data>? TelemetryData { get; set; }

    public List<MqttTopic>? MqttTopics { get; set; }
    #endregion
}
