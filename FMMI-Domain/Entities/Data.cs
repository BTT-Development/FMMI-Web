using FMMI_Domain.Entities.Base;
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
    public class Data
    {
        [NotMapped]
        public ObjectId Id { get; set; } // MongoDB internal ID

        public int ID { get; set; } // Relational DB ID
        public string Date { get; set; }
        public double Value { get; set; }


        #region Navigations property
        [ForeignKey("DevicesID")]
        public Device Devices { get; set; }
        [ForeignKey("DataTypeID")]
        public DataType Type { get; set; }
        #endregion
    }
}
