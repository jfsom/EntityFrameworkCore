using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class Index_Attribute_IndexSortOrderinEFCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_RegistrationNumber",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "RollNumber",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students",
                columns: new[] { "RegistrationNumber", "RollNumber" },
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Index_RegistrationNumber_RollNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RollNumber",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "Index_RegistrationNumber",
                table: "Students",
                column: "RegistrationNumber",
                unique: true);
        }
    }
}
