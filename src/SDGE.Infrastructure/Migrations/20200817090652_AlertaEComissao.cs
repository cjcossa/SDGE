using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class AlertaEComissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "MembroOrganizador",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "MembroCientifico",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CriadoPor",
                table: "ComissaoOrganizadora",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CriadoPor",
                table: "ComissaoCientifica",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Alerta",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "MembroOrganizador");

            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "MembroCientifico");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "ComissaoOrganizadora");

            migrationBuilder.DropColumn(
                name: "CriadoPor",
                table: "ComissaoCientifica");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Alerta");
        }
    }
}
