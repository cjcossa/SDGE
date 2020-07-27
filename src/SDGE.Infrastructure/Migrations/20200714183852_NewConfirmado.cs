using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class NewConfirmado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Submissao");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "Submissao",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "Submissao");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Submissao",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
