using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Service.Services.TelemetriService
{
    public interface ITelemetriService
    {
        #region Temperature methods
        Task<List<Data>> GetDataAsync();
        Task<List<Data>> GetDataByDeviceIdAsync(int deviceId);
        Task InsertData(Data data);
        #endregion

     
    }
}
