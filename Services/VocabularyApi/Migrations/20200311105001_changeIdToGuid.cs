using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VocabularyApi.Migrations
{
    public partial class changeIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserVocabularies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserVocabularies",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
