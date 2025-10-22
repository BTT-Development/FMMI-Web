using FMMI_Domain;
using FMMI_Domain.Entities;
using FMMI_Service.DTO.Location;
using FMMI_Service.Result;
using FMMI_Service.Services.Base;
using FMMI_Service.Mapping.Location;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Service.Services.LocationServices;

internal class LocationService : BaseService<Locations>, ILocationService
{
    private FMMIContext _context;
    public LocationService(FMMIContext context) :base(context)
    {
        _context = context;
    }
    public async Task<Result<List<ShowLocationDTO>>> GetAllLocations()
    {
        List<ShowLocationDTO> list = new(); 
        list = await _context.Locations.AsNoTracking().MapToLocationDto().ToListAsync();
        if (list.Count > 0)
        {
            return Result<List<ShowLocationDTO>>.Succes(list, "Fundet data");
        }
        return Result<List<ShowLocationDTO>>.Fail("Ingen data fundet");
    }
}
