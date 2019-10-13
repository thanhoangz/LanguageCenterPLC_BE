using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageCenterPLC.Data.EF.Migrations
{
    public partial class initialize_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageClasses_Courses_CourseId",
                table: "LanguageClasses");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "LanguageClasses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageClasses_Courses_CourseId",
                table: "LanguageClasses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageClasses_Courses_CourseId",
                table: "LanguageClasses");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "LanguageClasses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageClasses_Courses_CourseId",
                table: "LanguageClasses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
