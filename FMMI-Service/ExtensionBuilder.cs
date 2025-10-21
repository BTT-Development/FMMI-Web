using FMMI_Service.BackgroundWorker;
using FMMI_Service.Services.AlarmServices;
using FMMI_Service.Services.DeviceServices;
using FMMI_Service.Services.LocationServices;
using FMMI_Service.Services.MachineServices;
using FMMI_Service.Services.TelemetriService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMMI_Service
{
    public static class ExtensionBuilder
    {
        public static WebApplicationBuilder AppendServiceConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddServices();

            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITelemetriService, TelemetriService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMachineService, MachineService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IAlarmLogService, AlarmLogService>();


            return services;
        }

    }
}
