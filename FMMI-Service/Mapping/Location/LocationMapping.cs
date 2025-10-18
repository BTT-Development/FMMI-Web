using FMMI_Domain.Entities;
using FMMI_Service.DTO.Location;

namespace FMMI_Service.Mapping.Location;

public static class LocationMapping
{
    public static IQueryable<ShowLocationDTO> MapToLocationDto(this IQueryable<Locations> quere)
    {
        return quere.Select(x => new ShowLocationDTO
        {
            ID = x.Id,
            Name = x.Name,
        });

        
    }
}
