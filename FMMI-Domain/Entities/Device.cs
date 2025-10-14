using FMMI_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
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
        public string Name { get; set; }

        #region Navigation propert
        public int LocationID { get; set; }
        public Location Location { get; set; }

        public int DeviceTypeID { get; set; }
        public DeviceType DeviceType { get; set; }

        public int AlarmID { get; set; }
        public Alarm Alarm { get; set; }
        #endregion
    }
}
