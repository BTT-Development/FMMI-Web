using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FMMI_Domain.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MqttPubSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttPubSubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MachineName = table.Column<string>(type: "text", nullable: false),
                    LocationsId = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MqttTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MqttPubSubId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MqttTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MqttTopics_MqttPubSubs_MqttPubSubId",
                        column: x => x.MqttPubSubId,
                        principalTable: "MqttPubSubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MachineId = table.Column<int>(type: "integer", nullable: false),
                    DeviceTypeID = table.Column<int>(type: "integer", nullable: false),
                    AlarmID = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeID",
                        column: x => x.DeviceTypeID,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Topics = table.Column<string>(type: "text", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceMqttTopic",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "integer", nullable: false),
                    MqttTopicsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceMqttTopic", x => new { x.DevicesId, x.MqttTopicsId });
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceMqttTopic_MqttTopics_MqttTopicsId",
                        column: x => x.MqttTopicsId,
                        principalTable: "MqttTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelemetriData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Dates = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    DeviceID = table.Column<int>(type: "integer", nullable: false),
                    DataTypeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetriData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelemetriData_DataTypes_DataTypeID",
                        column: x => x.DataTypeID,
                        principalTable: "DataTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TelemetriData_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AlarmeID = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmLogs_Alarms_AlarmeID",
                        column: x => x.AlarmeID,
                        principalTable: "Alarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alarms",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DeviceId", "Name", "Topics", "Value" },
                values: new object[] { 1, "e4baec7a-02ac-4875-9c9f-97d7d4d0986b", "This is the default alarm.", null, "Default Alarm", "test", 0.0 });

            migrationBuilder.InsertData(
                table: "DataTypes",
                columns: new[] { "Id", "ConcurrencyStamp", "TypeName", "Unit" },
                values: new object[,]
                {
                    { 1, "50933a24-f7cc-41dd-af11-be52b7bcc627", "Temperature", "°C" },
                    { 2, "0d443d2c-144f-488d-b003-e84ca2b1a848", "Humidity", "%" }
                });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "ConcurrencyStamp", "Name" },
                values: new object[] { 1, "4f0490fb-2ec2-47df-86cd-b69e9a26effd", "Sensor" });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "5467f196-eee2-4316-9429-6e514072cb49", "Placering af første maskine", "Warehouse A" },
                    { 2, "dbac44d6-336e-4557-9d5c-7adbd1425173", "Placering af anden maskine", "Warehouse B" }
                });

            migrationBuilder.InsertData(
                table: "MqttPubSubs",
                columns: new[] { "Id", "ConcurrencyStamp", "Name" },
                values: new object[,]
                {
                    { 1, "316f7ea3-5a43-406a-a916-1bb29c556220", "Publish" },
                    { 2, "40751cd1-1511-4bc6-a200-23a76e090b8a", "Subcribe" }
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "Id", "ConcurrencyStamp", "LocationsId", "MachineName" },
                values: new object[,]
                {
                    { 1, "5e9f19fd-2853-4679-9bce-d40d1de584c4", 1, "Machine A" },
                    { 2, "828280ea-aed4-4d16-844c-0eb9fcee6457", 2, "Machine B" }
                });

            migrationBuilder.InsertData(
                table: "MqttTopics",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DeviceId", "MqttPubSubId", "Topic" },
                values: new object[] { 1, "03e9f1b2-e9c8-469c-91fa-00be8c5e7c51", "status", 1, 2, "device/esp32/status" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "AlarmID", "ConcurrencyStamp", "DeviceTypeID", "MachineId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "283dbf03-6f12-47e6-acf4-970f87dda610", 1, 1, "TempSensor1" },
                    { 2, 1, "1b8ec008-2c2d-4077-9c1d-b3c224dc031f", 1, 1, "HumiditySensor1" },
                    { 3, 1, "bb1ad2b2-b9a1-403c-8dda-2f807b34d357", 1, 2, "TempSensor2" },
                    { 4, 1, "06360baf-4182-41b3-8194-28b23b08b727", 1, 2, "HumiditySensor2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmLogs_AlarmeID",
                table: "AlarmLogs",
                column: "AlarmeID");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_DeviceId",
                table: "Alarms",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceMqttTopic_MqttTopicsId",
                table: "DeviceMqttTopic",
                column: "MqttTopicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeID",
                table: "Devices",
                column: "DeviceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MachineId",
                table: "Devices",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_LocationsId",
                table: "Machines",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_MqttTopics_MqttPubSubId",
                table: "MqttTopics",
                column: "MqttPubSubId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetriData_DataTypeID",
                table: "TelemetriData",
                column: "DataTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetriData_DeviceID",
                table: "TelemetriData",
                column: "DeviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmLogs");

            migrationBuilder.DropTable(
                name: "DeviceMqttTopic");

            migrationBuilder.DropTable(
                name: "TelemetriData");

            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "MqttTopics");

            migrationBuilder.DropTable(
                name: "DataTypes");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "MqttPubSubs");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
