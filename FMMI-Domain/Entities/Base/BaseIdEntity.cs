using FMMI_Domain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities.Base
{
    public class BaseIdEntity : IBaseIdEntity
    {
        [Key]
        public int Id { get; set; }


    }
}
