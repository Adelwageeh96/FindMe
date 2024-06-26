using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "EmbeddingVector",
                schema: "FindMe",
                table: "UserDetails",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmbeddingVector",
                schema: "FindMe",
                table: "UserDetails");
        }
    }
}
