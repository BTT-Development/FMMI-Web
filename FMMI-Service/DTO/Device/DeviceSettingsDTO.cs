

namespace FMMI_Service.DTO.Device;

public class DeviceSettingsDTO
{
    public int Id { get; set; }
    public int RealtimeInterval { get; set; }
    public int DataInterval { get; set; }

    public int DeviceId { get; set; }

    public string Topic { get; set; }
}
