using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class NonPrimaryKeyTableinEFCoreusingFluentAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
