using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FMMI_Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Humidity",
                table: "Humidity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Temperature");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Temperature");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Humidity");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Humidity");

            migrationBuilder.AlterColumn<int>(
                name: "TempID",
                table: "Temperature",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "HumID",
                table: "Humidity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature",
                column: "TempID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Humidity",
                table: "Humidity",
                column: "HumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Humidity",
                table: "Humidity");

            migrationBuilder.AlterColumn<int>(
                name: "TempID",
                table: "Temperature",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Temperature",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Temperature",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "HumID",
                table: "Humidity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Humidity",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Humidity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Humidity",
                table: "Humidity",
                column: "Id");
        }
    }
}
