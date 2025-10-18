using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FMMI_Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "AlarmLogs",
                newName: "Dates");

            migrationBuilder.AddColumn<bool>(
                name: "newAlarm",
                table: "AlarmLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newAlarm",
                table: "AlarmLogs");

            migrationBuilder.RenameColumn(
                name: "Dates",
                table: "AlarmLogs",
                newName: "Date");
        }
    }
}
