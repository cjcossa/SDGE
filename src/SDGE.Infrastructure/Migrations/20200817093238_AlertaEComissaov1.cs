using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class AlertaEComissaov1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "ComissaoOrganizadora");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "ComissaoCientifica");

            migrationBuilder.AddColumn<int>(
                name: "CriadoPorId",
                table: "ComissaoOrganizadora",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CriadoPorId",
                table: "ComissaoCientifica",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "ComissaoOrganizadora");

            migrationBuilder.DropColumn(
                name: "CriadoPorId",
                table: "ComissaoCientifica");

            migrationBuilder.AddColumn<int>(
                name: "CriadoPor",
                table: "ComissaoOrganizadora",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CriadoPor",
                table: "ComissaoCientifica",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
