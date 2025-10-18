using FMMI_Service.DTO.Machine;
using FMMI_Service.Result;

namespace FMMI_Service.Services.MachineServices;

public interface IMachineService
{
    Result<List<ShowMachineDTO>> GetMachinesByLocationId(int id);
}
