using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Users_InstructorUserId",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_InstructorUserId",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "InstructorUserId",
                table: "Tracks");

            migrationBuilder.RenameColumn(
                name: "University",
                table: "Users",
                newName: "StudentUniversity");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Users",
                newName: "StudentDegree");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Users",
                newName: "StudentSpecialization");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Users",
                newName: "InstructorSalary");

            migrationBuilder.RenameColumn(
                name: "GraduationYear",
                table: "Users",
                newName: "StudentGraduationYear");

            migrationBuilder.RenameColumn(
                name: "Faculity",
                table: "Users",
                newName: "StudentFaculity");

            migrationBuilder.RenameColumn(
                name: "Employee_Salary",
                table: "Users",
                newName: "EmployeeSalary");

            migrationBuilder.RenameColumn(
                name: "Degree",
                table: "Users",
                newName: "EmployeeType");

            migrationBuilder.CreateTable(
                name: "InstructorTrack",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorTrack", x => new { x.InstructorId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_InstructorTrack_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstructorTrack_Users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructorTrack_TrackId",
                table: "InstructorTrack",
                column: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorTrack");

            migrationBuilder.RenameColumn(
                name: "StudentUniversity",
                table: "Users",
                newName: "University");

            migrationBuilder.RenameColumn(
                name: "StudentSpecialization",
                table: "Users",
                newName: "Specialization");

            migrationBuilder.RenameColumn(
                name: "StudentGraduationYear",
                table: "Users",
                newName: "GraduationYear");

            migrationBuilder.RenameColumn(
                name: "StudentFaculity",
                table: "Users",
                newName: "Faculity");

            migrationBuilder.RenameColumn(
                name: "StudentDegree",
                table: "Users",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "InstructorSalary",
                table: "Users",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                table: "Users",
                newName: "Degree");

            migrationBuilder.RenameColumn(
                name: "EmployeeSalary",
                table: "Users",
                newName: "Employee_Salary");

            migrationBuilder.AddColumn<int>(
                name: "InstructorUserId",
                table: "Tracks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_InstructorUserId",
                table: "Tracks",
                column: "InstructorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Users_InstructorUserId",
                table: "Tracks",
                column: "InstructorUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
