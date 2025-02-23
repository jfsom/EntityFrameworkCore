using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddEnrollmentDateToStudentCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses");

            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "StudentCourses",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "StudentCourses",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentsId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_CourseId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "StudentCourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "StudentCourses");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "StudentCourses",
                newName: "StudentsId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentCourses",
                newName: "CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
