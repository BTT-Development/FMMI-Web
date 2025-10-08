using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
    }
}
