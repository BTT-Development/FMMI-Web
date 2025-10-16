using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class AlarmLogs : BaseIdEntity
    {
        public DateTime Date { get; set; }

        [ForeignKey("AlarmeID")]
        public Alarm Alarmer { get; set; }
    }
}
