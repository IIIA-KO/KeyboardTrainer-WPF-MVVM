using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyboardTrainerDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccuracyToScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Accuracy",
                table: "Scores",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "Scores");
        }
    }
}
