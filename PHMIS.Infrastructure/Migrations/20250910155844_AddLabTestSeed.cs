using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHMIS.Infrastructure.Migrations
{
    public partial class AddLabTestSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Description", "IsActive", "LabTestGroupId", "LabTestGroupId1", "Name", "Price" },
                values: new object[] { 1, "Fasting blood glucose", true, 1, null, "Glucose", 10 });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Description", "IsActive", "LabTestGroupId", "LabTestGroupId1", "Name", "Price" },
                values: new object[] { 2, "Total Cholesterol, HDL, LDL, Triglycerides", true, 1, null, "Lipid Profile", 25 });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Description", "IsActive", "LabTestGroupId", "LabTestGroupId1", "Name", "Price" },
                values: new object[] { 3, "Complete Blood Count", true, 2, null, "CBC", 20 });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Description", "IsActive", "LabTestGroupId", "LabTestGroupId1", "Name", "Price" },
                values: new object[] { 4, "Microbiology culture", true, 3, null, "Urine Culture", 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
