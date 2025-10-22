using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace FMMI_Domain.Entities;

public class DeviceSettings : Concurrency
{
    public int RealtimeInterval { get; set; }
    public int DataInterval { get; set; }

    public int DeviceId { get; set; }
    public Device Device { get; set; }

    public string Topic { get; set; }

}
