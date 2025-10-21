using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities;

public class DeviceType : Concurrency
{
    public string Name { get; set; }
}
