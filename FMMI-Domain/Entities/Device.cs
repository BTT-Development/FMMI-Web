using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }

        #region Navigation property
        public Location LocationID { get; set; }
        public DeviceType DeviceTypeID { get; set; }
        public Alarm AlarmID { get; set; }
        #endregion
    }
}
