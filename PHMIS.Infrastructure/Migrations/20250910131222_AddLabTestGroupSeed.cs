using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHMIS.Infrastructure.Migrations
{
    public partial class AddLabTestGroupSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTestGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<short>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestGroups", x => x.Id);
                });

            

            migrationBuilder.InsertData(
                table: "LabTestGroups",
                columns: new[] { "Id", "Description", "Name", "SortOrder" },
                values: new object[] { 1, "Chemistry tests", "Chemistry", (short)1 });

            migrationBuilder.InsertData(
                table: "LabTestGroups",
                columns: new[] { "Id", "Description", "Name", "SortOrder" },
                values: new object[] { 2, "Blood tests", "Hematology", (short)2 });

            migrationBuilder.InsertData(
                table: "LabTestGroups",
                columns: new[] { "Id", "Description", "Name", "SortOrder" },
                values: new object[] { 3, "Microbiology tests", "Microbiology", (short)3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabTestGroups");

          
        }
    }
}
