using FindMe.Domain.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                schema: "Account",
                columnTypes: new[] { "nvarchar(max)", "nvarchar(max)", "nvarchar(max)", "nvarchar(max)" },
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), Roles.USER, Roles.USER.ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
               table: "Roles",
               schema: "Account",
               columnTypes: new[] { "nvarchar(max)", "nvarchar(max)", "nvarchar(max)", "nvarchar(max)" },
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), Roles.ADMIN, Roles.ADMIN.ToUpper(), Guid.NewGuid().ToString() }
            );
            migrationBuilder.InsertData(
               table: "Roles",
               schema: "Account",
               columnTypes: new[] { "nvarchar(max)", "nvarchar(max)", "nvarchar(max)", "nvarchar(max)" },
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), Roles.ORGANIZATION, Roles.ORGANIZATION.ToUpper(), Guid.NewGuid().ToString() }
            );

        }



        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [Roles] ");
        }
    }
}
