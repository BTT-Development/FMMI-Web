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
        Task<List<TelemetriData>> GetTelemetriDataAsync();
        Task<TelemetriData?> GetTelemetriDataByIdAsync(int id);
        Task<List<TelemetriData>> GetTelemetriDataByDeviceIdAsync(int deviceId);
        Task<List<TelemetriData>> GetTelemetriDataByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task InsertTelemetriData(TelemetriData data);
    }
}
