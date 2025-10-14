using FMMI_Domain.Entities.Base;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FMMI_Domain.Entities
{
    public class Temp
    {
        [NotMapped]
        public ObjectId Id { get; set; }

        public string Sensor { get; set; }
        public string Date { get; set; }
        public double Temperature { get; set; }

        #region Navigations property
        [ForeignKey("DevicesID")]
        public Device Devices { get; set; }
        #endregion
    }
}
