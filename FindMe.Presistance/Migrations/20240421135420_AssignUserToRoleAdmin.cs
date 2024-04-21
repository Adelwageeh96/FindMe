using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class AssignUserToRoleAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Account].[UserRoles] (UserId, RoleId) SELECT '06a89fd5-c47d-43fd-b373-c29f9944b9d5', Id FROM [Account].[Roles] WHERE NormalizedName = 'ADMIN';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [Account].[UserRoles] where UserId = '06a89fd5-c47d-43fd-b373-c29f9944b9d5';");
        }
    }
}
