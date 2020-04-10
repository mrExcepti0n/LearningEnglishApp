using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class addTrainingStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserVocabularyWords");

            migrationBuilder.AddColumn<int>(
                name: "UserVocabularyId",
                table: "UserVocabularyWords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrainingStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingType = table.Column<int>(nullable: false),
                    UserVocabularyWordId = table.Column<int>(nullable: false),
                    LastWrongAnswerDate = table.Column<DateTime>(nullable: true),
                    LastRightAnswerDate = table.Column<DateTime>(nullable: true),
                    WrongAnswerCount = table.Column<int>(nullable: false),
                    RightAnswerCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingStatistic_UserVocabularyWords_UserVocabularyWordId",
                        column: x => x.UserVocabularyWordId,
                        principalTable: "UserVocabularyWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVocabularies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    WordSetId = table.Column<int>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVocabularies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVocabularies_WordSets_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabularyWords_UserVocabularyId",
                table: "UserVocabularyWords",
                column: "UserVocabularyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingStatistic_UserVocabularyWordId",
                table: "TrainingStatistic",
                column: "UserVocabularyWordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVocabularies_WordSetId",
                table: "UserVocabularies",
                column: "WordSetId");



            migrationBuilder.AddForeignKey(
                name: "FK_UserVocabularyWords_UserVocabularies_UserVocabularyId",
                table: "UserVocabularyWords",
                column: "UserVocabularyId",
                principalTable: "UserVocabularies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVocabularyWords_UserVocabularies_UserVocabularyId",
                table: "UserVocabularyWords");

            migrationBuilder.DropTable(
                name: "TrainingStatistic");

            migrationBuilder.DropTable(
                name: "UserVocabularies");

            migrationBuilder.DropIndex(
                name: "IX_UserVocabularyWords_UserVocabularyId",
                table: "UserVocabularyWords");

            migrationBuilder.DropColumn(
                name: "UserVocabularyId",
                table: "UserVocabularyWords");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserVocabularyWords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
