using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class ExtendingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Branches_BranchId",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "CourseSubject");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_BranchId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_BranchId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_TeacherId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Fees",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TeacherName",
                table: "Teachers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsTenured",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Teachers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WorkHours",
                table: "Teachers",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "GPA",
                table: "Students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Students",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<double>(
                name: "Credits",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TeacherId",
                table: "Addresses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_TeacherId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsTenured",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "WorkHours",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "GPA",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Teachers",
                newName: "TeacherName");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fees",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "CourseSubject",
                columns: table => new
                {
                    CoursesCourseId = table.Column<int>(type: "int", nullable: false),
                    SubjectsSubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubject", x => new { x.CoursesCourseId, x.SubjectsSubjectId });
                    table.ForeignKey(
                        name: "FK_CourseSubject_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubject_Subjects_SubjectsSubjectId",
                        column: x => x.SubjectsSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    SubjectsSubjectId = table.Column<int>(type: "int", nullable: false),
                    TeachersTeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.SubjectsSubjectId, x.TeachersTeacherId });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subjects_SubjectsSubjectId",
                        column: x => x.SubjectsSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Teachers_TeachersTeacherId",
                        column: x => x.TeachersTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_BranchId",
                table: "Teachers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BranchId",
                table: "Students",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_TeacherId",
                table: "Addresses",
                column: "TeacherId",
                unique: true,
                filter: "[TeacherId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubject_SubjectsSubjectId",
                table: "CourseSubject",
                column: "SubjectsSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeachersTeacherId",
                table: "SubjectTeacher",
                column: "TeachersTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Branches_BranchId",
                table: "Students",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Branches_BranchId",
                table: "Teachers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
