using FMMI_Domain;
using FMMI_Service.DTO.Machine;
using FMMI_Service.Mapping.Machine;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Microsoft.EntityFrameworkCore;
using Machine = FMMI_Domain.Entities.Machine;

namespace FMMI_Service.Services.MachineServices;

internal class MachineService : BaseService<Machine>, IMachineService
{
    private FMMIContext _context;
    public MachineService(FMMIContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Result<List<ShowMachineDTO>>> GetMachinesByLocationId(int id)
    {
        List<ShowMachineDTO> list = new();
        list = await _context.Machines.AsNoTracking().Where(x => x.LocationsId == id).MapMachineToDTO().ToListAsync();
        if (list.Count > 0)
        {
            return Result<List<ShowMachineDTO>>.Succes(list, "Data fundet.");
        }
        return Result<List<ShowMachineDTO>>.Fail("Data ikke fundet.");
    }
}
