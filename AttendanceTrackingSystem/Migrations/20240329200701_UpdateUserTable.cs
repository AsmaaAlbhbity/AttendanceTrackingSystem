using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Employee_Salary",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Faculity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GraduationYear",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "InstructorUserId",
                table: "Tracks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeUserId1",
                table: "StudentAttendance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentUserId",
                table: "StudentAttendance",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeUserId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstructorUserId",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TrackId",
                table: "Users",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_InstructorUserId",
                table: "Tracks",
                column: "InstructorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_EmployeeUserId1",
                table: "StudentAttendance",
                column: "EmployeeUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendance_StudentUserId",
                table: "StudentAttendance",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeUserId",
                table: "Attendances",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_InstructorUserId",
                table: "Attendances",
                column: "InstructorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_EmployeeUserId",
                table: "Attendances",
                column: "EmployeeUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Users_InstructorUserId",
                table: "Attendances",
                column: "InstructorUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Users_EmployeeUserId1",
                table: "StudentAttendance",
                column: "EmployeeUserId1",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Users_StudentUserId",
                table: "StudentAttendance",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_InstructorUserId",
                table: "Tracks",
                column: "InstructorUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tracks_TrackId",
                table: "Users",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_EmployeeUserId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Users_InstructorUserId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Users_EmployeeUserId1",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Users_StudentUserId",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_InstructorUserId",
                table: "Tracks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tracks_TrackId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TrackId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_InstructorUserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendance_EmployeeUserId1",
                table: "StudentAttendance");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendance_StudentUserId",
                table: "StudentAttendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmployeeUserId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_InstructorUserId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Employee_Salary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Faculity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InstructorUserId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "EmployeeUserId1",
                table: "StudentAttendance");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "StudentAttendance");

            migrationBuilder.DropColumn(
                name: "EmployeeUserId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "InstructorUserId",
                table: "Attendances");
        }
    }
}
