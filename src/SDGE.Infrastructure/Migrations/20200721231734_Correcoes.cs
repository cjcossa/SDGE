using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class Correcoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Submissao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Submissao");

            migrationBuilder.CreateIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId",
                unique: true);
        }
    }
}
