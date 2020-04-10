using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserVocabularies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Word = table.Column<string>(nullable: true),
                    Translation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVocabularies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VocabularyWords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true),
                    Trasncription = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    AudioRecord = table.Column<byte[]>(nullable: true),
                    Language = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabularyWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WordId = table.Column<int>(nullable: false),
                    Translation = table.Column<string>(nullable: true),
                    Language = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordTranslations_VocabularyWords_WordId",
                        column: x => x.WordId,
                        principalTable: "VocabularyWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslations_WordId",
                table: "WordTranslations",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVocabularies");

            migrationBuilder.DropTable(
                name: "WordTranslations");

            migrationBuilder.DropTable(
                name: "VocabularyWords");
        }
    }
}
