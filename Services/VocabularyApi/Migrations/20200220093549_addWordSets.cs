using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class addWordSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordSetItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true),
                    Translation = table.Column<string>(nullable: true),
                    WordSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSetItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordSetItem_WordSets_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordSetItem_WordSetId",
                table: "WordSetItem",
                column: "WordSetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordSetItem");

            migrationBuilder.DropTable(
                name: "WordSets");
        }
    }
}
