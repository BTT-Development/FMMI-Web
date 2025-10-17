using FMMI_Service.DTO.Location;
using FMMI_Service.Result;

namespace FMMI_Service.Services.LocationServices;

public interface ILocationService
{
    Result<List<ShowLocationDTO>> GetAllLocations();
}
