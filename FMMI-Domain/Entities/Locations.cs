using FMMI_Domain.Entities.Base;

namespace FMMI_Domain.Entities
{
    public class Locations : Concurrency
    {
        public string? Name { get; set; }
        public string Description { get; set; }

        public List<Machine> Machines { get; set; }
    }
}
