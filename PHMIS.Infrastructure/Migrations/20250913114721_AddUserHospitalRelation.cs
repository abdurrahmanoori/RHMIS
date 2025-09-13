using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PHMIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserHospitalRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "e7028603-a49e-4176-863c-34d6b50d83aa");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "662155dc-36c7-4526-91f8-97fa69faf6e8");

            migrationBuilder.UpdateData(
                table: "LabTestGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "31ff63a9-1af2-4859-b6d6-7b5f08d03980");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "2f539d1b-74af-4cb7-9db4-bff22e6e7420");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "1189386f-aa14-4031-a1d3-337009234d29");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "53ee01ed-9b39-407c-b6b0-3a27fe4fd87f");

            migrationBuilder.UpdateData(
                table: "LabTests",
                keyColumn: "Id",
                keyValue: 4,
                column: "PublicId",
                value: "e96edb23-4927-45c1-89f2-cc18d437929c");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublicId",
                value: "503c7733-5651-41d9-810b-c0425ea48e54");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublicId",
                value: "021f64a1-efab-431f-8872-2520e2227fbe");

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublicId",
                value: "661f28c2-747b-4bc6-a5a6-3c0c176ca0ff");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hospitals_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }
    }
}
