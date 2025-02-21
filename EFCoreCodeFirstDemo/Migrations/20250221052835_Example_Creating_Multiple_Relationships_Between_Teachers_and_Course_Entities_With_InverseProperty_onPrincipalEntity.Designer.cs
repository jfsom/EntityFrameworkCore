﻿// <auto-generated />
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
    [Migration("20250221052835_Example_Creating_Multiple_Relationships_Between_Teachers_and_Course_Entities_With_InverseProperty_onPrincipalEntity")]
    partial class Example_Creating_Multiple_Relationships_Between_Teachers_and_Course_Entities_With_InverseProperty_onPrincipalEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OfflineTeacherId")
                        .HasColumnType("int");

                    b.Property<int?>("OnlineTeacherId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("OfflineTeacherId");

                    b.HasIndex("OnlineTeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Course", b =>
                {
                    b.HasOne("EFCoreCodeFirstDemo.Entities.Teacher", "OfflineTeacher")
                        .WithMany("OfflineCourses")
                        .HasForeignKey("OfflineTeacherId");

                    b.HasOne("EFCoreCodeFirstDemo.Entities.Teacher", "OnlineTeacher")
                        .WithMany("OnlineCourses")
                        .HasForeignKey("OnlineTeacherId");

                    b.Navigation("OfflineTeacher");

                    b.Navigation("OnlineTeacher");
                });

            modelBuilder.Entity("EFCoreCodeFirstDemo.Entities.Teacher", b =>
                {
                    b.Navigation("OfflineCourses");

                    b.Navigation("OnlineCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
