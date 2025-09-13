using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PHMIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntaiMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "LabTestGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<short>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    HospitalId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    HospitalId = table.Column<int>(type: "INTEGER", nullable: true),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LabTestGroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitOfMeasurment = table.Column<string>(type: "TEXT", nullable: true),
                    NormalRange = table.Column<string>(type: "TEXT", nullable: true),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    LabTestGroupId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTests_LabTestGroups_LabTestGroupId",
                        column: x => x.LabTestGroupId,
                        principalTable: "LabTestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabTests_LabTestGroups_LabTestGroupId1",
                        column: x => x.LabTestGroupId1,
                        principalTable: "LabTestGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientLabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicId = table.Column<string>(type: "TEXT", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    LabTestId = table.Column<int>(type: "INTEGER", nullable: false),
                    LabOrderNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    ResultValue = table.Column<string>(type: "TEXT", nullable: true),
                    NumericResultValue = table.Column<decimal>(type: "TEXT", nullable: true),
                    ResultNotes = table.Column<string>(type: "TEXT", nullable: true),
                    NormalRange = table.Column<string>(type: "TEXT", nullable: true),
                    IsAbnormal = table.Column<bool>(type: "INTEGER", nullable: true),
                    OrderedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CollectedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReportedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    OrderedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceivedByLabAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CollectedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ReportedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AcknowledgedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLabTests_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientLabTests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Hospitals",
                columns: new[] { "Id", "Address", "AvailableBeds", "Category", "City", "Code", "Country", "CreatedBy", "DateCreated", "Email", "EmergencyBeds", "EmergencyPhone", "EmergencyServices", "EstablishedDate", "HasLaboratory", "HasOperationTheater", "HasPharmacy", "HasRadiology", "IcuBeds", "IsActive", "LastModifiedBy", "LastModifiedDate", "Latitude", "LicenseNumber", "Longitude", "Name", "Phone", "PublicId", "State", "TaxIdentificationNumber", "TotalBeds", "Type", "Website", "ZipCode" },
                values: new object[,]
                {
                    { 1, "100 Main St", null, null, "Springfield", "H-CENTRAL", "US", null, null, "central@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "Central Hospital", "+1-555-0001", "a4ec3c8d-2b05-4a3d-9b08-8c6f0d1b4a11", null, null, null, null, null, null },
                    { 2, "200 West Ave", null, null, "Springfield", "H-WEST", "US", null, null, "west@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "Westside Clinic", "+1-555-0002", "b8b2a2b3-6f02-4c8e-9b47-84a118e8f2c2", null, null, null, null, null, null },
                    { 3, "300 East Blvd", null, null, "Springfield", "H-EAST", "US", null, null, "east@example.com", null, null, null, null, null, null, null, null, null, true, null, null, null, null, null, "East Medical Center", "+1-555-0003", "c3f4d6e7-8a90-4b2c-b3d4-e5f6a7b8c9d0", null, null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "LabTestGroups",
                columns: new[] { "Id", "Description", "Name", "PublicId", "SortOrder" },
                values: new object[,]
                {
                    { 1, "Chemistry tests", "Chemistry", "9fa8e362-15c6-47a2-9cb3-6297d87829d4", (short)1 },
                    { 2, "Blood tests", "Hematology", "4ab294c4-1d43-4ecc-84ca-83fb468cf173", (short)2 },
                    { 3, "Microbiology tests", "Microbiology", "b7800a32-f461-4043-bde6-4a89cb6d81bd", (short)3 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "Gender", "HospitalId", "LastName", "Name", "PhoneNumber", "PublicId" },
                values: new object[,]
                {
                    { 1, "123 Main St, Springfield", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "Male", null, "Doe", "John", "555-1234", "42bd65f9-3503-4cd6-82a2-3e326b643582" },
                    { 2, "456 Elm St, Springfield", new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Female", null, "Smith", "Jane", "555-5678", "8405555c-6eda-4eec-8af5-0aed96c79c29" },
                    { 3, "789 Oak St, Springfield", new DateTime(1978, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "alex.johnson@example.com", "Other", null, "Johnson", "Alex", "555-9012", "cc290e42-e9ce-483b-9676-541422ccbc6b" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "HospitalId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "689e23d7-80aa-4873-8cf7-c1798d828394", "admin@local", true, "System", 1, "Admin", false, null, "ADMIN@LOCAL", "ADMIN", "AQAAAAIAAYagAAAAENkwj9M48uZBk1E57AFNjA0u9ssvvMbDjhbuYnUpD1v6AnUcfU/aeXEO4+LCsv/c9w==", null, false, "admin-seed", false, "admin" },
                    { 2, 0, "29f5727f-70a3-4fdf-be68-11adb37798c0", "user@local", true, "Default", 1, "User", false, null, "USER@LOCAL", "USER", "AQAAAAIAAYagAAAAELzLT0lI0Jj93NqA41PCZkJxD/SMJn4lo7zZXzDXKCKKb0CCWqTdDegcTz5dncKriQ==", null, false, "user-seed", false, "user" }
                });

            migrationBuilder.InsertData(
                table: "LabTests",
                columns: new[] { "Id", "Abbreviation", "Description", "IsActive", "LabTestGroupId", "LabTestGroupId1", "Name", "NormalRange", "Price", "PublicId", "UnitOfMeasurment" },
                values: new object[,]
                {
                    { 1, "FBS", "Fasting blood glucose", true, 1, null, "Glucose", "70-99", 10, "6f3e1457-8fea-4382-a748-2302a8546c79", "mg/dL" },
                    { 2, "LP", "Total Cholesterol, HDL, LDL, Triglycerides", true, 1, null, "Lipid Profile", "Varies", 25, "2c0d827f-d5af-4fdf-b063-b596c2c75dd5", "mg/dL" },
                    { 3, "CBC", "Complete Blood Count", true, 2, null, "CBC", "Varies", 20, "421137d3-c694-41b3-93af-946d5a35faf3", null },
                    { 4, "UC", "Microbiology culture", true, 3, null, "Urine Culture", "Negative", 30, "9d3ab0a1-0a99-4b7b-874d-2f67e0f276e4", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HospitalId",
                table: "AspNetUsers",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_LabTestGroupId",
                table: "LabTests",
                column: "LabTestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_LabTestGroupId1",
                table: "LabTests",
                column: "LabTestGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLabTests_LabTestId",
                table: "PatientLabTests",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLabTests_PatientId",
                table: "PatientLabTests",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HospitalId",
                table: "Patients",
                column: "HospitalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PatientLabTests");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "LabTestGroups");

            migrationBuilder.DropTable(
                name: "Hospitals");
        }
    }
}
