﻿// <auto-generated />
using System;
using EFCoreCodeFirstDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreCodeFirstDemo.Migrations
{
    [DbContext(typeof(EFCoreDbContext))]
    [Migration("20250221112818_FortheNotMappedAttribute")]
    partial class FortheNotMappedAttribute
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            Name = "Human Resources"
                        },
                        new
                        {
                            DepartmentId = 2,
                            Name = "IT"
                        });
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DateOfBirth = new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfJoining = new DateTime(2015, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            FirstName = "Hina",
                            LastName = "Sharma"
                        },
                        new
                        {
                            EmployeeId = 2,
                            DateOfBirth = new DateTime(1992, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfJoining = new DateTime(2018, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            FirstName = "Pranaya",
                            LastName = "Rout"
                        },
                        new
                        {
                            EmployeeId = 3,
                            DateOfBirth = new DateTime(1985, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfJoining = new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            FirstName = "Rakesh",
                            LastName = "Singh"
                        },
                        new
                        {
                            EmployeeId = 4,
                            DateOfBirth = new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfJoining = new DateTime(2017, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            FirstName = "Priyanka",
                            LastName = "Tiwary"
                        });
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Employee", b =>
                {
                    b.HasOne("EFCoreCodeFirstDemo.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
