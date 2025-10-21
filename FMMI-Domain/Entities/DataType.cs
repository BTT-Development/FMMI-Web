using FMMI_Domain.Entities.Base;
using Microsoft.Identity.Client.TelemetryCore.TelemetryClient;

namespace FMMI_Domain.Entities;

public class DataType : Concurrency
{
    public string TypeName { get; set; }
    public string Unit { get; set; }

    public List<Data> TelemetryData { get; set; }
}
