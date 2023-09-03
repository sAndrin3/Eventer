using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Management.Migrations
{
    /// <inheritdoc />
    public partial class mige : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserRegistrations_UserId",
                table: "UserRegistrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRegistrations_Users_UserId",
                table: "UserRegistrations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRegistrations_Users_UserId",
                table: "UserRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_UserRegistrations_UserId",
                table: "UserRegistrations");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
