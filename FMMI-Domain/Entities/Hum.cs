using FMMI_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    public class Hum
    {
        [NotMapped]
        public ObjectId Id { get; set; }

        public string Sensor { get; set; }
        public string Date { get; set; }
        public double Humidity { get; set; }

        #region Navigations property
        [ForeignKey("DevicesID")]
        public Device Devices { get; set; }
        #endregion
    }
}
