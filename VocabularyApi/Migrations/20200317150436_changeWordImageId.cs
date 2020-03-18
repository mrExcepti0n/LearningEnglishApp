using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class changeWordImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VocabularyWords_WordImage_WordImageId",
                table: "VocabularyWords");

            migrationBuilder.DropIndex(
                name: "IX_VocabularyWords_WordImageId",
                table: "VocabularyWords");

            migrationBuilder.DropColumn(
                name: "WordImageId",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "VocabularyWords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyWords_ImageId",
                table: "VocabularyWords",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VocabularyWords_WordImage_ImageId",
                table: "VocabularyWords",
                column: "ImageId",
                principalTable: "WordImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VocabularyWords_WordImage_ImageId",
                table: "VocabularyWords");

            migrationBuilder.DropIndex(
                name: "IX_VocabularyWords_ImageId",
                table: "VocabularyWords");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<int>(
                name: "WordImageId",
                table: "VocabularyWords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyWords_WordImageId",
                table: "VocabularyWords",
                column: "WordImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VocabularyWords_WordImage_WordImageId",
                table: "VocabularyWords",
                column: "WordImageId",
                principalTable: "WordImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
