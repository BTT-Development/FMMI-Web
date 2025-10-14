using FMMI_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Domain.Entities
{
    [NotMapped]
    public class DeviceSettings : BaseIdEntity
    {
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        #region Navigations property
        [ForeignKey("DeviceID")]
        public Device Devices { get; set; }
        #endregion
    }

}
