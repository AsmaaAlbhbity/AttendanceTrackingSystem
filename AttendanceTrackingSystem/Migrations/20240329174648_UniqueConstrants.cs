using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstrants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tracks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ItiProgram",
                columns: new[] { "ItiProgramId", "Duration", "Name" },
                values: new object[,]
                {
                    { 1, 9, 0 },
                    { 2, 4, 1 },
                    { 3, 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId_Date",
                table: "Permissions",
                columns: new[] { "UserId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItiProgram_Name",
                table: "ItiProgram",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId_Date",
                table: "Attendances",
                columns: new[] { "UserId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserId_Date",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_ItiProgram_Name",
                table: "ItiProgram");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_UserId_Date",
                table: "Attendances");

            migrationBuilder.DeleteData(
                table: "ItiProgram",
                keyColumn: "ItiProgramId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItiProgram",
                keyColumn: "ItiProgramId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ItiProgram",
                keyColumn: "ItiProgramId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tracks");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");
        }
    }
}
