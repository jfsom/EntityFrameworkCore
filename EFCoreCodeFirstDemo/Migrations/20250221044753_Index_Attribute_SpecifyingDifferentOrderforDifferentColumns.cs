using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class Index_Attribute_SpecifyingDifferentOrderforDifferentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students",
                columns: new[] { "RegistrationNumber", "RollNumber" },
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students",
                columns: new[] { "RegistrationNumber", "RollNumber" },
                descending: new bool[0]);
        }
    }
}
