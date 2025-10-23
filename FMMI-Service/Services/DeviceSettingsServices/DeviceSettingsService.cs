using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Device;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using MQTTnet;
using MQTTnet.Client;

namespace FMMI_Service.Services.DeviceSettingsServices;

internal class DeviceSettingsService : BaseService<DeviceSettings>
{
    private FMMIContext _context;

    private IMqttClient _mqttClient;

    private readonly MqttClientOptionsBuilder _mqttClientOptionsBuilder;
    public DeviceSettingsService(FMMIContext context, IMqttClient mqttClient, MqttClientOptionsBuilder mqttClientOption) : base(context)
    {
        _context = context;
        _mqttClient = mqttClient;
        _mqttClientOptionsBuilder = mqttClientOption;
    }
    public async Task<Result.Result> UpdateDeviceSettings(DeviceSettingsDTO settings)
    {
        DeviceSettings foundSettings = await _context.DeviceSettings.FindAsync(settings.Id);
        if (foundSettings.RealtimeInterval != settings.RealtimeInterval)
        {
            await _context.DeviceSettings.Where(x => x.Id == settings.Id).ExecuteUpdateAsync(x => x.SetProperty(p => p.RealtimeInterval, settings.RealtimeInterval));
        }
        if (foundSettings.DataInterval != settings.DataInterval)
        {
            await _context.DeviceSettings.Where(x => x.Id == settings.Id).ExecuteUpdateAsync(x => x.SetProperty(p => p.DataInterval, settings.DataInterval));

        }
        MqttFactory mqttFactory = new MqttFactory();
        using (_mqttClient = mqttFactory.CreateMqttClient())
        {
            await _mqttClient.ConnectAsync(_mqttClientOptionsBuilder.WithCredentials("", "").Build(), CancellationToken.None);
            
        
        
        }
        return Result.Result.Succes("opdateret");
    }
}
