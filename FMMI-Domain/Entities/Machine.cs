using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class Machine : Concurrency
{
    public string MachineName { get; set; }


    public int LocationsId { get; set; }
    public Locations Locations { get; set; }
}
