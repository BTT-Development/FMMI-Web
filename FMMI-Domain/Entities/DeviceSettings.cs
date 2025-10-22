using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{

    public class DeviceSettings : Concurrency
    {
        public int RealtimeInterval { get; set; }
        public int DataInterval { get; set; }

        public int DeviceId { get; set; }
        public Device Devices { get; set; }
     
    }

}
