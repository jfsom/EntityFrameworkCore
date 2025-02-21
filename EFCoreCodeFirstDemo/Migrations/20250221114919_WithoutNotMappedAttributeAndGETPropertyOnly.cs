using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class WithoutNotMappedAttributeAndGETPropertyOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, "Human Resources" },
                    { 2, "IT" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfJoining", "DepartmentId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Hina", "Sharma" },
                    { 2, new DateTime(1992, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pranaya", "Rout" },
                    { 3, new DateTime(1985, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Rakesh", "Singh" },
                    { 4, new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Priyanka", "Tiwary" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
