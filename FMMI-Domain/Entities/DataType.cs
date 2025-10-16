using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class DataType : BaseIdEntity
    {
        public string TypeName { get; set; }
        public string Unit { get; set; }
    }
}
