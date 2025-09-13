using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHMIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ADDhOSPITAL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patients",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Patients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    LicenseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    TaxIdentificationNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    EmergencyPhone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true),
                    EstablishedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmergencyServices = table.Column<bool>(type: "INTEGER", nullable: true),
                    TotalBeds = table.Column<int>(type: "INTEGER", nullable: true),
                    AvailableBeds = table.Column<int>(type: "INTEGER", nullable: true),
                    IcuBeds = table.Column<int>(type: "INTEGER", nullable: true),
                    EmergencyBeds = table.Column<int>(type: "INTEGER", nullable: true),
                    HasPharmacy = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasLaboratory = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasRadiology = table.Column<bool>(type: "INTEGER", nullable: true),
                    HasOperationTheater = table.Column<bool>(type: "INTEGER", nullable: true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "2584fb4b-0d75-4c48-a5d8-5073e690576c");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "6b78445d-c6d5-4848-a638-fcd146c59d87");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "249d0548-ce61-4b61-9e77-e3a31fb9534d");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "66c2d88c-f7fe-4434-ae45-d53b6f128d4a");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "80947ecb-87ee-4bbf-bfe7-784af44b558f");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "04fa2544-c6b0-4951-8d65-b50a79c65ac2");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublicId",
                value: "0c8538fb-2423-4f1b-941b-70143508c6c0");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HospitalId", "PublicId" },
                values: new object[] { null, "f7393c6e-1c6b-4be0-91cd-ff6ea6e5de43" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HospitalId", "PublicId" },
                values: new object[] { null, "d1cd5f66-8c1c-4b2c-8e00-6ed643f04450" });

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HospitalId", "PublicId" },
                values: new object[] { null, "b7dd26da-9ba5-496a-abd7-a7b21e222798" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HospitalId",
                table: "Patients",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_HospitalId",
                table: "Patients",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_HospitalId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Patients_HospitalId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HospitalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patients",
                newName: "FirstName");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "69b9bd1d-8adf-4cb9-b5f0-fc8acdbde392");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "2ecc704f-c117-41a9-9cc0-850ec14254f2");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "b0fa7be7-0ddd-4a2e-9ff7-cd9857d294ea");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "5089843c-ebb6-4610-a57f-79b0a77f57e6");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "5705d529-5ecf-4eaf-9163-ae519a3d29ab");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "172d56b8-c029-49f5-bca1-3a88c860a37b");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublicId",
                value: "78b3760b-4b85-455e-a036-6f35031dafc8");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "a25c02ff-109d-4dc5-a3e9-dffeb88b054e");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "5dc69bc3-0e31-4db7-a1c6-d145e83a1b78");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "78937e0f-a045-4c58-8df6-2c774df066e6");
        }
    }
}
