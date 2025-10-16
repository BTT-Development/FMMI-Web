using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Alarm : BaseIdEntity
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public string Topics { get; set; }
    }
}
