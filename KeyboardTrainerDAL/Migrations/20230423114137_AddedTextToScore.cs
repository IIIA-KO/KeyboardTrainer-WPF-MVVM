using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeyboardTrainerDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTextToScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TextId",
                table: "Scores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_TextId",
                table: "Scores",
                column: "TextId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Texts_TextId",
                table: "Scores",
                column: "TextId",
                principalTable: "Texts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Texts_TextId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_TextId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "TextId",
                table: "Scores");
        }
    }
}
