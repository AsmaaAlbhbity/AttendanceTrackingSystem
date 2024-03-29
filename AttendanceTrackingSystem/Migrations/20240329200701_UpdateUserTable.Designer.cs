﻿// <auto-generated />
using System;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    [DbContext(typeof(AttendanceDBContext))]
    [Migration("20240329200701_UpdateUserTable")]
    partial class UpdateUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<TimeOnly>("CheckIn")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("CheckOut")
                        .HasColumnType("time");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmployeeUserId")
                        .HasColumnType("int");

                    b.Property<int?>("InstructorUserId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId");

                    b.HasIndex("EmployeeUserId");

                    b.HasIndex("InstructorUserId");

                    b.HasIndex("UserId", "Date")
                        .IsUnique();

                    b.ToTable("Attendances");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Intake", b =>
                {
                    b.Property<int>("IntakeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IntakeId"));

                    b.Property<int>("ItiProgramId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.HasKey("IntakeId");

                    b.HasIndex("ItiProgramId");

                    b.ToTable("Intake");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.ItiProgram", b =>
                {
                    b.Property<int>("ItiProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItiProgramId"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("ItiProgramId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ItiProgram");

                    b.HasData(
                        new
                        {
                            ItiProgramId = 1,
                            Duration = 9,
                            Name = 0
                        },
                        new
                        {
                            ItiProgramId = 2,
                            Duration = 4,
                            Name = 1
                        },
                        new
                        {
                            ItiProgramId = 3,
                            Duration = 2,
                            Name = 2
                        });
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PermissionId");

                    b.HasIndex("UserId", "Date")
                        .IsUnique();

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Schdule", b =>
                {
                    b.Property<int>("SchduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchduleId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndPeriod")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartPeriod")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.HasKey("SchduleId");

                    b.HasIndex("TrackId");

                    b.ToTable("Schdules");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int?>("InstructorUserId")
                        .HasColumnType("int");

                    b.Property<int>("IntakeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("TrackId");

                    b.HasIndex("InstructorUserId");

                    b.HasIndex("IntakeId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasAnnotation("RegularExpression", "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasAnnotation("RegularExpression", "^(010|011|015)-\\d{8}$");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.StudentAttendance", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.Attendance");

                    b.Property<int?>("EmployeeUserId1")
                        .HasColumnType("int");

                    b.Property<int>("SchduleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("StudentUserId")
                        .HasColumnType("int");

                    b.HasIndex("EmployeeUserId1");

                    b.HasIndex("SchduleId");

                    b.HasIndex("StudentUserId");

                    b.ToTable("StudentAttendance");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Employee", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.ToTable("Users", t =>
                        {
                            t.Property("Salary")
                                .HasColumnName("Employee_Salary");
                        });

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Instructor", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.HasDiscriminator().HasValue("Instructor");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.HasBaseType("AttendanceTrackingSystem.Models.User");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<string>("Faculity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduationYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackId")
                        .HasColumnType("int");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("TrackId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Attendance", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Employee", null)
                        .WithMany("Attendances")
                        .HasForeignKey("EmployeeUserId");

                    b.HasOne("AttendanceTrackingSystem.Models.Instructor", null)
                        .WithMany("Attendances")
                        .HasForeignKey("InstructorUserId");

                    b.HasOne("AttendanceTrackingSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Intake", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.ItiProgram", "Program")
                        .WithMany()
                        .HasForeignKey("ItiProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Permission", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Schdule", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Track", "Track")
                        .WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Instructor", null)
                        .WithMany("Tracks")
                        .HasForeignKey("InstructorUserId");

                    b.HasOne("AttendanceTrackingSystem.Models.Intake", "Intake")
                        .WithMany()
                        .HasForeignKey("IntakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.User", "Instructor")
                        .WithMany()
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Intake");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.StudentAttendance", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Attendance", null)
                        .WithOne()
                        .HasForeignKey("AttendanceTrackingSystem.Models.StudentAttendance", "AttendanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.Employee", null)
                        .WithMany("StdAttendances")
                        .HasForeignKey("EmployeeUserId1");

                    b.HasOne("AttendanceTrackingSystem.Models.Schdule", "StudentSchdule")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("SchduleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AttendanceTrackingSystem.Models.Student", null)
                        .WithMany("Attendances")
                        .HasForeignKey("StudentUserId");

                    b.Navigation("StudentSchdule");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.HasOne("AttendanceTrackingSystem.Models.Track", "Track")
                        .WithMany("Students")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Schdule", b =>
                {
                    b.Navigation("StudentAttendances");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Track", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Employee", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("StdAttendances");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Instructor", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Tracks");
                });

            modelBuilder.Entity("AttendanceTrackingSystem.Models.Student", b =>
                {
                    b.Navigation("Attendances");
                });
#pragma warning restore 612, 618
        }
    }
}