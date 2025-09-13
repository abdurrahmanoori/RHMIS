using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PHMIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "AvailableBeds", "Category", "City", "Code", "Country", "CreatedBy", "DateCreated", "Email", "EmergencyBeds", "EmergencyPhone", "EmergencyServices", "EstablishedDate", "HasLaboratory", "HasOperationTheater", "HasPharmacy", "HasRadiology", "IcuBeds", "IsActive", "LastModifiedBy", "LastModifiedDate", "Latitude", "LicenseNumber", "Longitude", "Name", "Phone", "PublicId", "State", "TaxIdentificationNumber", "TotalBeds", "Type", "Website", "ZipCode" },
                values: new object[,]
                {
                    { 1, "100 Main St", null, null, "Springfield", "H-CENTRAL", "US", null, null, "central@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "Central Hospital", "+1-555-0001", "a4ec3c8d-2b05-4a3d-9b08-8c6f0d1b4a11", null, null, null, null, null, null },
                    { 2, "200 West Ave", null, null, "Springfield", "H-WEST", "US", null, null, "west@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "Westside Clinic", "+1-555-0002", "b8b2a2b3-6f02-4c8e-9b47-84a118e8f2c2", null, null, null, null, null, null },
                    { 3, "300 East Blvd", null, null, "Springfield", "H-EAST", "US", null, null, "east@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "East Medical Center", "+1-555-0003", "c3f4d6e7-8a90-4b2c-b3d4-e5f6a7b8c9d0", null, null, null, null, null, null }
                });

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "ac67ba48-1deb-4dcb-88f3-bbf652c9656f");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "bf52b17e-db9e-4851-b7a0-6e6059bdde50");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "42d1b06e-db7f-49d3-8c03-1e50cc4d6b87");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "ac67918f-c1ff-4871-9745-dbf7ae00ee3c");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "593aeb15-aae1-41dc-a770-452a348c0549");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "f4a5fa31-3286-47b8-ae78-e7b0bfcce3c3");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublicId",
                value: "817c2166-63ae-4bb5-8ae8-ccb22999f1ba");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "134b3466-7189-4c0c-a1a9-5aff378c7cba");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "73aad0d9-f487-4e5e-8480-55e4776e13b4");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "f75b0747-4068-4678-a62f-474255da4d53");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hospitals",
                keyColumn: "Id",
                keyValue: 3);

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
                column: "PublicId",
                value: "f7393c6e-1c6b-4be0-91cd-ff6ea6e5de43");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "d1cd5f66-8c1c-4b2c-8e00-6ed643f04450");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "b7dd26da-9ba5-496a-abd7-a7b21e222798");
        }
    }
}
