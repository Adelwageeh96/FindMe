using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMe.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecognitionRequestResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FirstSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SecondSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ThirdSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult");

            migrationBuilder.DropColumn(
                name: "SecondSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult");

            migrationBuilder.DropColumn(
                name: "ThirdSimilarityPercent",
                schema: "FindMe",
                table: "RecognitionRequestsResult");
        }
    }
}
