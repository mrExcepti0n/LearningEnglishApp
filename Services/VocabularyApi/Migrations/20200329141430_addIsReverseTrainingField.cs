using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class addIsReverseTrainingField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReverseTraining",
                table: "TrainingStatistic",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReverseTraining",
                table: "TrainingStatistic");
        }
    }
}
