using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vehicle.Tracking.DataAccess.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rights_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rights_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Long = table.Column<double>(type: "float", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AddedBy = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationHistories_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "AddedBy", "AddedDate", "ChangedBy", "ChangedDate", "Name", "Status" },
                values: new object[] { 1, 1, new DateTime(2021, 10, 24, 20, 33, 50, 107, DateTimeKind.Local).AddTicks(6310), null, null, "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AddedBy", "AddedDate", "ChangedBy", "ChangedDate", "Email", "Name", "NameSurname", "Password", "RefreshToken", "RefreshTokenExpiresDateTime", "Status", "Surname" },
                values: new object[] { 1, 1, new DateTime(2021, 10, 24, 20, 33, 50, 100, DateTimeKind.Local).AddTicks(6151), null, null, "test@test.com", "Test", "Test Test", "", null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "Test" });

            migrationBuilder.InsertData(
                table: "Rights",
                columns: new[] { "Id", "AddedBy", "AddedDate", "ChangedBy", "ChangedDate", "RoleId", "Status", "UserId" },
                values: new object[] { 1, 1, new DateTime(2021, 10, 24, 20, 33, 50, 107, DateTimeKind.Local).AddTicks(9911), null, null, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_LocationHistories_VehicleId",
                table: "LocationHistories",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rights_RoleId",
                table: "Rights",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rights_UserId",
                table: "Rights",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationHistories");

            migrationBuilder.DropTable(
                name: "Rights");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
