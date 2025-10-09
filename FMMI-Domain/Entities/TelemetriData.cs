using FMMI_Domain.Entities.Base;
using FMMI_Domain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class TelemetriData : BaseIdEntity
    {
        [Key]
        public int TelemetriDataID { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Date { get; set; }

        #region Navigation property
        public int DeviceID { get; set; }
        [ForeignKey("DeviceID")]
        public Device Device { get; set; }
        #endregion
    }
}
