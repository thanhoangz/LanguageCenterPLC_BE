using Microsoft.EntityFrameworkCore.Migrations;

namespace LanguageCenterPLC.Data.EF.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalWorkday",
                table: "Timesheets",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalWorkday",
                table: "Timesheets",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
