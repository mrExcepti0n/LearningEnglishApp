using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class changeWordImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "VocabularyWords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordImageId",
                table: "VocabularyWords",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WordImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsThumbnail = table.Column<bool>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordImage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyWords_ThumbnailId",
                table: "VocabularyWords",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_VocabularyWords_WordImageId",
                table: "VocabularyWords",
                column: "WordImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VocabularyWords_WordImage_ThumbnailId",
                table: "VocabularyWords",
                column: "ThumbnailId",
                principalTable: "WordImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VocabularyWords_WordImage_WordImageId",
                table: "VocabularyWords",
                column: "WordImageId",
                principalTable: "WordImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VocabularyWords_WordImage_ThumbnailId",
                table: "VocabularyWords");

            migrationBuilder.DropForeignKey(
                name: "FK_VocabularyWords_WordImage_WordImageId",
                table: "VocabularyWords");

            migrationBuilder.DropTable(
                name: "WordImage");

            migrationBuilder.DropIndex(
                name: "IX_VocabularyWords_ThumbnailId",
                table: "VocabularyWords");

            migrationBuilder.DropIndex(
                name: "IX_VocabularyWords_WordImageId",
                table: "VocabularyWords");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "VocabularyWords");

            migrationBuilder.DropColumn(
                name: "WordImageId",
                table: "VocabularyWords");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "VocabularyWords",
                nullable: true);
        }
    }
}
