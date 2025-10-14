using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Domain
{
    public class FMMIContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Hum> Humidity { get; set; }
        public DbSet<Temp> Temperature { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Location> locations { get; set; }

        public FMMIContext(DbContextOptions<FMMIContext> options) : base(options) { }
    }
}
