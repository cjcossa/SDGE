using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "EventoParticipante");

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Tipo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Submissao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Participante",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "MembroOrganizador",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "MembroCientifico",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Membro",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmado",
                table: "EventoParticipante",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "EventoParticipante",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Evento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "DataImportante",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Correcao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "ComissaoOrganizadora",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "ComissaoCientifica",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removido",
                table: "Alerta",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Tipo");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Submissao");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Participante");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "MembroOrganizador");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "MembroCientifico");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Membro");

            migrationBuilder.DropColumn(
                name: "Confirmado",
                table: "EventoParticipante");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "EventoParticipante");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "DataImportante");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Correcao");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "ComissaoOrganizadora");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "ComissaoCientifica");

            migrationBuilder.DropColumn(
                name: "Removido",
                table: "Alerta");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EventoParticipante",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }
    }
}
