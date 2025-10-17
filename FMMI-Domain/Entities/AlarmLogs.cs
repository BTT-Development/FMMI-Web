using FMMI_Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMMI_Domain.Entities;

public class AlarmLogs : BaseIdEntity
{
    public DateTime Date { get; set; }

    [ForeignKey("AlarmeID")]
    public Alarm Alarmer { get; set; }
}
