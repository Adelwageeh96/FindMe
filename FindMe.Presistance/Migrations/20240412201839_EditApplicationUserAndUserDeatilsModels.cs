using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class EditApplicationUserAndUserDeatilsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gendre",
                schema: "FindMe",
                table: "UserDetails");

            migrationBuilder.AddColumn<string>(
                name: "Gendre",
                schema: "Account",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gendre",
                schema: "Account",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Gendre",
                schema: "FindMe",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
