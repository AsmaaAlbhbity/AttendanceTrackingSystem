using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class studentAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Schdules_SchduleId",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Users_EmployeeUserId1",
                table: "StudentAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendance_Users_StudentUserId",
                table: "StudentAttendance");

            migrationBuilder.DropTable(
                name: "Schdules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendance",
                table: "StudentAttendance");

            migrationBuilder.RenameTable(
                name: "StudentAttendance",
                newName: "StudentAttendances");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendance_StudentUserId",
                table: "StudentAttendances",
                newName: "IX_StudentAttendances_StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendance_SchduleId",
                table: "StudentAttendances",
                newName: "IX_StudentAttendances_SchduleId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendance_EmployeeUserId1",
                table: "StudentAttendances",
                newName: "IX_StudentAttendances_EmployeeUserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances",
                column: "AttendanceId");

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TrackId",
                table: "Schedules",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceId",
                table: "StudentAttendances",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "AttendanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Schedules_SchduleId",
                table: "StudentAttendances",
                column: "SchduleId",
                principalTable: "Schedules",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Users_EmployeeUserId1",
                table: "StudentAttendances",
                column: "EmployeeUserId1",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Users_StudentUserId",
                table: "StudentAttendances",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Attendances_AttendanceId",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Schedules_SchduleId",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Users_EmployeeUserId1",
                table: "StudentAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Users_StudentUserId",
                table: "StudentAttendances");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttendances",
                table: "StudentAttendances");

            migrationBuilder.RenameTable(
                name: "StudentAttendances",
                newName: "StudentAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendances_StudentUserId",
                table: "StudentAttendance",
                newName: "IX_StudentAttendance_StudentUserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendances_SchduleId",
                table: "StudentAttendance",
                newName: "IX_StudentAttendance_SchduleId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAttendances_EmployeeUserId1",
                table: "StudentAttendance",
                newName: "IX_StudentAttendance_EmployeeUserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttendance",
                table: "StudentAttendance",
                column: "AttendanceId");

            migrationBuilder.CreateTable(
                name: "Schdules",
                columns: table => new
                {
                    SchduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartPeriod = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schdules", x => x.SchduleId);
                    table.ForeignKey(
                        name: "FK_Schdules_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schdules_TrackId",
                table: "Schdules",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Attendances_AttendanceId",
                table: "StudentAttendance",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "AttendanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendance_Schdules_SchduleId",
                table: "StudentAttendance",
                column: "SchduleId",
                principalTable: "Schdules",
                principalColumn: "SchduleId",
                onDelete: ReferentialAction.Restrict);

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
        }
    }
}
