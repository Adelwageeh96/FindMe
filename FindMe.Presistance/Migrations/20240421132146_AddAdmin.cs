using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Account].[Users] ([Id], [Name], [Address], [Photo], [FCMToken], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Gendre]) VALUES (N'06a89fd5-c47d-43fd-b373-c29f9944b9d5', N'Admin', NULL, NULL, NULL, N'admin', N'ADMIN', N'Admin@admin.com', N'ADMIN@ADMIN.COM', 0, N'AQAAAAIAAYagAAAAEC8opYWKhotltGlk+pKqhCw7D6cBd6qLfs66Qop1EROpoBXhCfvQQN+oVTvwRzfoCw==', N'B6RCKQYFMSD32BNQMUXZM3RP6FJQXCQM', N'650a49c4-de12-4251-9575-3a65f5d92fab', N'11111111111', 0, 0, NULL, 1, 0, 0)\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Account].[Users] WHERE id = '06a89fd5-c47d-43fd-b373-c29f9944b9d5'");
        }
    }
}
