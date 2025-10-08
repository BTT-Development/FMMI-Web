using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class DeviceType
    {
        [Key]
        public int DeviceTypeID { get; set; }
        public string Name { get; set; }
    }
}
