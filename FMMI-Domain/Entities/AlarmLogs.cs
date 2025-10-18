using FMMI_Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMMI_Domain.Entities;

public class AlarmLogs : BaseIdEntity
{
    [NotMapped]
    public string Date { get; set; }
    public DateTime Dates { get; set; }

    [ForeignKey("AlarmeID")]
    public Alarm Alarmer { get; set; }

    public bool newAlarm { get; set; }
}
