using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Machine : BaseIdEntity
    {
        public string MachineName { get; set; }


        public int LocationID { get; set; }
        public Location Locations { get; set; }
    }
}
