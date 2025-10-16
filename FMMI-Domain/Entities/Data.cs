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
        [NotMapped]
        public string Date { get; set; }

        public int ID { get; set; } // Relational DB ID
        public DateTime Dates { get; set; }
        public double Value { get; set; }


        #region Navigations property
        public int DeviceID { get; set; }
        public Device Device { get; set; }
        
        public int DataTypeID { get; set; }
        public DataType DataType { get; set; }
        #endregion
    }
}
