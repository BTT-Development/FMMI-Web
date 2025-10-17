using FMMI_Domain;
using FMMI_Service.DTO.Machine;
using FMMI_Service.Mapping.Machine;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using Machine = FMMI_Domain.Entities.Machine;

namespace FMMI_Service.Services.MachineServices;

internal class MachineService : BaseService<Machine>, IMachineService
{
    private FMMIContext _context;
    public MachineService(FMMIContext context) : base(context)
    {
        _context = context;
    }
    public Result<List<ShowMachineDTO>> GetMachinesByLocationId(int id)
    {
        List<ShowMachineDTO> list = new();
        list = _context.Machines.Where(x => x.LocationsId == id).MapMachineToDTO().ToList();
        if (list.Count > 0)
        {
            return Result<List<ShowMachineDTO>>.Succes(list, "Data fundet.");
        }
        return Result<List<ShowMachineDTO>>.Fail("Data ikke fundet.");
    }
}
