using FMMI_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FMMI_Domain;

public class FMMIContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<Data> TelemetriData { get; set; }
    public DbSet<Alarm> Alarms { get; set; }
    public DbSet<AlarmLogs> AlarmLogs { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Locations> Locations { get; set; }
    public DbSet<DataType> DataTypes { get; set; }
    public DbSet<MqttTopic> MqttTopics { get; set; }
    public DbSet<MqttPubSub> MqttPubSubs { get; set; }
    public DbSet<DeviceSettings> DeviceSettings { get; set; }

    public FMMIContext(DbContextOptions<FMMIContext> options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceSettings>()
            .HasOne(x => x.Device)
            .WithOne(x => x.Settings)
            .HasForeignKey<Device>(x => x.DeviceSettingsId);

        SeedDataType(modelBuilder);
        SeedDeviceType(modelBuilder);
        SeedLocation(modelBuilder);
        SeedMachine(modelBuilder);
        SeedDevice(modelBuilder);
        SeedMqttPubSub(modelBuilder);
        SeedMqttTopics(modelBuilder);
        SeedDeviceSettings(modelBuilder);
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
        modelbuilder.Entity<Locations>().HasData(
            new Locations { Id = 1, Name = "Warehouse A", Description = "Placering af første maskine", ConcurrencyStamp = "5467f196-eee2-4316-9429-6e514072cb49" },
            new Locations { Id = 2, Name = "Warehouse B", Description = "Placering af anden maskine", ConcurrencyStamp = "dbac44d6-336e-4557-9d5c-7adbd1425173" }
        );
    }

    private void SeedDevice(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Device>().HasData(
            new Device { Id = 1, Name = "Esp32-s3", MachineId = 1, DeviceTypeID = 1, AlarmId = 1, ConcurrencyStamp = "283dbf03-6f12-47e6-acf4-970f87dda610"  },
            new Device { Id = 2, Name = "HumiditySensor1", MachineId = 1, DeviceTypeID = 1, AlarmId = 1 , ConcurrencyStamp = "1b8ec008-2c2d-4077-9c1d-b3c224dc031f" },
            new Device { Id = 3, Name = "TempSensor2", MachineId = 2, DeviceTypeID = 1, AlarmId = 1 , ConcurrencyStamp = "bb1ad2b2-b9a1-403c-8dda-2f807b34d357" },
            new Device { Id = 4, Name = "HumiditySensor2", MachineId = 2, DeviceTypeID = 1, AlarmId = 1 , ConcurrencyStamp = "06360baf-4182-41b3-8194-28b23b08b727" }
        );
    }

    private void SeedMachine(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Machine>().HasData(
            new Machine { Id = 1, MachineName = "Machine A", LocationsId = 1 , ConcurrencyStamp = "5e9f19fd-2853-4679-9bce-d40d1de584c4" },
            new Machine { Id = 2, MachineName = "Machine B", LocationsId = 2, ConcurrencyStamp = "828280ea-aed4-4d16-844c-0eb9fcee6457" }
        );
    }
    
    private void SeedAlarm(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Alarm>().HasData(
            new Alarm { Id = 1, Name = "Status", Description = "This is for connection", ConcurrencyStamp = "e4baec7a-02ac-4875-9c9f-97d7d4d0986b", DeviceId = 1 , MqttTopicId = 1},
            new Alarm { Id = 2, Name = "Dht11", Description = "This is for error on dht11 sensor", ConcurrencyStamp = "e4baec7a-02ac-4875-9c9f-97d7d4d0986c", DeviceId = 1, MqttTopicId = 2 }
        );
    }
    private void SeedMqttPubSub(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<MqttPubSub>().HasData(
            new MqttPubSub { Id = 1, Name = "Publish" },
            new MqttPubSub { Id = 2, Name = "Subcribe" }
            );
    }
    private void SeedMqttTopics(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<MqttTopic>().HasData(
            new MqttTopic { Id = 1, Description = "status", Topic = "device/esp32/alarm/status", MqttPubSubId = 2, ConcurrencyStamp = "03e9f1b2-e9c8-469c-91fa-00be8c5e7c51" },
            new MqttTopic { Id = 2, Description = "Dht11", Topic = "device/esp32/alarm/dht11", MqttPubSubId = 2, ConcurrencyStamp = "03e9f1b2-e9c8-469c-91fa-00be8c5f7c51" }
            );
    }
    private void SeedDeviceSettings(ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<DeviceSettings>().HasData(
            new DeviceSettings { Id = 1, RealtimeInterval = 1000, DataInterval = 10000, Topic = "/device/esp32/settings", DeviceId = 1, ConcurrencyStamp = "03f9f1b2-e9c8-469c-91fa-00be8c5f7c51" }
            );
    }
  

}
