using FMMI_Domain.Entities;

using FMMI_Service.DTO.Device;

namespace FMMI_Service.Mapping.Device;

public static class DeviceMapping
{
    public static IQueryable<ShowDeviceDTO> MapDeviceToDTO(this IQueryable<FMMI_Domain.Entities.Device> quere)
    {
        return quere.Select(x => new ShowDeviceDTO
        {
            Id = x.Id,
            Name = x.Name,
            Type = x.DeviceType.Name,
            MqttTopic = x.MqttTopics.Select(x => x.Topic).First()
        });
    }

    
}
