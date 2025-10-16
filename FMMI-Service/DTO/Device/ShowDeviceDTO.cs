namespace FMMI_Service.DTO.Device;

public class ShowDeviceDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool online { get; set; }
    public int MachineId { get; set; }
}
