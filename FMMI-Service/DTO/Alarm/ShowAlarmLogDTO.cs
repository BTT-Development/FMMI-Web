namespace FMMI_Service.DTO.Alarm;

public class ShowAlarmLogDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string Machine { get; set; }
    public string DeviceName { get; set; }
    public DateTime Date { get; set; }
}
