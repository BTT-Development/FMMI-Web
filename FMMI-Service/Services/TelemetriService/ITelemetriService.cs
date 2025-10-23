using FMMI_Domain.Entities;
using FMMI_Service.DTO.Data;
using FMMI_Service.Result;
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
        #region Data methods
        List<Data> GetData();
        Task<Result<List<Data>>> GetDataByDeviceIdAsync(int deviceId);
        Task InsertData(Data data);
        Task<List<DataDTO>> GetRealTidsDataAsync(MqttTopic topic);
        #endregion


    }
}
