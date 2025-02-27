using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class ExampletoUnderstandTablePerTypeTPTinEFCore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommonProperty = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DerivedTable1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Property1 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedTable1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerivedTable1_BaseTable_Id",
                        column: x => x.Id,
                        principalTable: "BaseTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DerivedTable2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Property2 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedTable2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerivedTable2_BaseTable_Id",
                        column: x => x.Id,
                        principalTable: "BaseTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DerivedTable1");

            migrationBuilder.DropTable(
                name: "DerivedTable2");

            migrationBuilder.DropTable(
                name: "BaseTable");
        }
    }
}
