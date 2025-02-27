using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class TablePerConcreteTypeInheritanceinEntityFrameworkCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "BaseEntitySequence");

            migrationBuilder.CreateTable(
                name: "DerivedTable1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    CommonProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedTable1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DerivedTable2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [BaseEntitySequence]"),
                    CommonProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedTable2", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DerivedTable1");

            migrationBuilder.DropTable(
                name: "DerivedTable2");

            migrationBuilder.DropSequence(
                name: "BaseEntitySequence");
        }
    }
}
