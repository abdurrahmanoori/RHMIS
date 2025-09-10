using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHMIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LabStatusStringEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "LabTests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalRange",
                table: "LabTests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurment",
                table: "LabTests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Abbreviation", "NormalRange", "UnitOfMeasurment" },
                values: new object[] { "FBS", "70-99", "mg/dL" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Abbreviation", "NormalRange", "UnitOfMeasurment" },
                values: new object[] { "LP", "Varies", "mg/dL" });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Abbreviation", "NormalRange", "UnitOfMeasurment" },
                values: new object[] { "CBC", "Varies", null });

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Abbreviation", "NormalRange", "UnitOfMeasurment" },
                values: new object[] { "UC", "Negative", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "NormalRange",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurment",
                table: "LabTests");
        }
    }
}
