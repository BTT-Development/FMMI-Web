using FMMI_Service.DTO.Data;
using FMMI_Service.DTO.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Service.Mapping.Data
{
    public static class DataMapping
    {
        public static IQueryable<DataDTO> MapDeviceToDTO(this IQueryable<FMMI_Domain.Entities.Data> quere)
        {
            return quere.Select(x => new DataDTO
            {
                Temperature = x.DataType.TypeName == "Temperature" ? x.Value : 0,
                Humidity = x.DataType.TypeName == "Humidity" ? x.Value : 0
            });
        }
    }
}
