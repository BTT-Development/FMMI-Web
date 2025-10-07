using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class TelemetriData
    {
        public int TelemetriDataID { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }

        #region Navigation property
        public Device DeviceID { get; set; }
        #endregion
    }
}
