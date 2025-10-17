using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace FMMI_Domain
{
    public class FMMIContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Data> TelemetriData { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<DataType> DataTypes { get; set; }

        public FMMIContext(DbContextOptions<FMMIContext> options) : base(options) { }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedDataType(modelBuilder);
            SeedDeviceType(modelBuilder);
            SeedLocation(modelBuilder);
            SeedDevice(modelBuilder);
            SeedMachine(modelBuilder);
            SeedAlarm(modelBuilder);
        }

        private void SeedDataType(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DataType>().HasData(
                new DataType { Id = 1, TypeName = "Temperature", Unit = "°C", ConcurrencyStamp = "50933a24-f7cc-41dd-af11-be52b7bcc627" },
                new DataType { Id = 2, TypeName = "Humidity", Unit = "%", ConcurrencyStamp = "0d443d2c-144f-488d-b003-e84ca2b1a848" }
            );
        }
        
        private void SeedDeviceType(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<DeviceType>().HasData(
                new DeviceType { Id = 1, Name = "Sensor" , ConcurrencyStamp = "4f0490fb-2ec2-47df-86cd-b69e9a26effd" }
            );
        }

        private void SeedLocation(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Warehouse A", Description = "Placering af første maskine", ConcurrencyStamp = "5467f196-eee2-4316-9429-6e514072cb49" },
                new Location { Id = 2, Name = "Warehouse B", Description = "Placering af anden maskine", ConcurrencyStamp = "dbac44d6-336e-4557-9d5c-7adbd1425173" }
            );
        }

        private void SeedDevice(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Device>().HasData(
                new Device { Id = 1, Name = "TempSensor1", LocationID = 1, DeviceTypeID = 1, AlarmID = 1, ConcurrencyStamp = "283dbf03-6f12-47e6-acf4-970f87dda610" },
                new Device { Id = 2, Name = "HumiditySensor1", LocationID = 1, DeviceTypeID = 1, AlarmID = 1 , ConcurrencyStamp = "1b8ec008-2c2d-4077-9c1d-b3c224dc031f" },
                new Device { Id = 3, Name = "TempSensor2", LocationID = 2, DeviceTypeID = 1, AlarmID = 1 , ConcurrencyStamp = "bb1ad2b2-b9a1-403c-8dda-2f807b34d357" },
                new Device { Id = 4, Name = "HumiditySensor2", LocationID = 2, DeviceTypeID = 1, AlarmID = 1 , ConcurrencyStamp = "06360baf-4182-41b3-8194-28b23b08b727" }
            );
        }

        private void SeedMachine(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>().HasData(
                new Machine { Id = 1, MachineName = "Machine A", LocationID = 1 , ConcurrencyStamp = "5e9f19fd-2853-4679-9bce-d40d1de584c4" },
                new Machine { Id = 2, MachineName = "Machine B", LocationID = 2, ConcurrencyStamp = "828280ea-aed4-4d16-844c-0eb9fcee6457" }
            );
        }
        
        private void SeedAlarm(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Alarm>().HasData(
                new Alarm { Id = 1, Name = "Default Alarm", Description = "This is the default alarm.",Topics ="test", ConcurrencyStamp = "e4baec7a-02ac-4875-9c9f-97d7d4d0986b" }
            );
        }

    }
}
