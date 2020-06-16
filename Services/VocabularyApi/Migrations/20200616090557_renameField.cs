using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class renameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Trasncription",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                table: "VocabularyWords",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transcription",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<string>(
                name: "Trasncription",
                table: "VocabularyWords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
