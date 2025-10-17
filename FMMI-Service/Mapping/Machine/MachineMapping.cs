using FMMI_Domain.Entities;
using FMMI_Service.DTO.Machine;

namespace FMMI_Service.Mapping.Machine;

public static class MachineMapping
{
    public static IQueryable<ShowMachineDTO> MapMachineToDTO(this IQueryable<FMMI_Domain.Entities.Machine> quere)
    {
        return quere.Select(x => new ShowMachineDTO
        {
            Id = x.Id,
            Name = x.MachineName
        });
    }
}
