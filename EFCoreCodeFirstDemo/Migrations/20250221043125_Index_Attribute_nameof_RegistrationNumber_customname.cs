using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class Index_Attribute_nameof_RegistrationNumber_customname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Students_RegistrationNumber",
                table: "Students",
                newName: "Index_RegistrationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Index_RegistrationNumber",
                table: "Students",
                newName: "IX_Students_RegistrationNumber");
        }
    }
}
