using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Device : BaseIdEntity
    {
        [Key]
        public int DeviceID { get; set; }
        public string Name { get; set; }

        #region Navigation propert
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public Location Location { get; set; }

        public int DeviceTypeID { get; set; }
        [ForeignKey("DeviceTypeID")]
        public DeviceType DeviceType { get; set; }

        public int AlarmID { get; set; }
        [ForeignKey("AlarmID")]
        public Alarm Alarm { get; set; }
        #endregion
    }
}
