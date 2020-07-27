using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class Alertas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Alerta",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AddColumn<int>(
                name: "ComissaoCientificaId",
                table: "Alerta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComissaoOrganizadoraId",
                table: "Alerta",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Destino",
                table: "Alerta",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_ComissaoCientificaId",
                table: "Alerta",
                column: "ComissaoCientificaId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_ComissaoOrganizadoraId",
                table: "Alerta",
                column: "ComissaoOrganizadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_ComissaoCientifica_ComissaoCientificaId",
                table: "Alerta",
                column: "ComissaoCientificaId",
                principalTable: "ComissaoCientifica",
                principalColumn: "ComissaoCientificaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_ComissaoOrganizadora_ComissaoOrganizadoraId",
                table: "Alerta",
                column: "ComissaoOrganizadoraId",
                principalTable: "ComissaoOrganizadora",
                principalColumn: "ComissaoOrganizadoraId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_ComissaoCientifica_ComissaoCientificaId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_ComissaoOrganizadora_ComissaoOrganizadoraId",
                table: "Alerta");

            migrationBuilder.DropIndex(
                name: "IX_Alerta_ComissaoCientificaId",
                table: "Alerta");

            migrationBuilder.DropIndex(
                name: "IX_Alerta_ComissaoOrganizadoraId",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "ComissaoCientificaId",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "ComissaoOrganizadoraId",
                table: "Alerta");

            migrationBuilder.DropColumn(
                name: "Destino",
                table: "Alerta");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Alerta",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
