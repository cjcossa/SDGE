using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class NewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComissaoCientificaId",
                table: "Evento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComissaoOrganizadoraId",
                table: "Evento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComissaoCientifica",
                columns: table => new
                {
                    ComissaoCientificaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissaoCientifica", x => x.ComissaoCientificaId);
                });

            migrationBuilder.CreateTable(
                name: "ComissaoOrganizadora",
                columns: table => new
                {
                    ComissaoOrganizadoraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissaoOrganizadora", x => x.ComissaoOrganizadoraId);
                });

            migrationBuilder.CreateTable(
                name: "DataImportante",
                columns: table => new
                {
                    DataImportanteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    DataInicio = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataFim = table.Column<string>(type: "varchar(50)", nullable: false),
                    Finalidade = table.Column<string>(type: "varchar(50)", nullable: false),
                    EventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataImportante", x => x.DataImportanteId);
                    table.ForeignKey(
                        name: "FK_DataImportante_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembroCientifico",
                columns: table => new
                {
                    MembroCientificoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComissaoCientificaId = table.Column<int>(nullable: false),
                    MembroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroCientifico", x => x.MembroCientificoId);
                    table.ForeignKey(
                        name: "FK_MembroCientifico_ComissaoCientifica_ComissaoCientificaId",
                        column: x => x.ComissaoCientificaId,
                        principalTable: "ComissaoCientifica",
                        principalColumn: "ComissaoCientificaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembroCientifico_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "MembroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembroOrganizador",
                columns: table => new
                {
                    MembroOrganizadorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComissaoOrganizadoraId = table.Column<int>(nullable: false),
                    MembroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroOrganizador", x => x.MembroOrganizadorId);
                    table.ForeignKey(
                        name: "FK_MembroOrganizador_ComissaoOrganizadora_ComissaoOrganizadoraId",
                        column: x => x.ComissaoOrganizadoraId,
                        principalTable: "ComissaoOrganizadora",
                        principalColumn: "ComissaoOrganizadoraId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembroOrganizador_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "MembroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_ComissaoCientificaId",
                table: "Evento",
                column: "ComissaoCientificaId");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_ComissaoOrganizadoraId",
                table: "Evento",
                column: "ComissaoOrganizadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_DataImportante_EventoId",
                table: "DataImportante",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroCientifico_ComissaoCientificaId",
                table: "MembroCientifico",
                column: "ComissaoCientificaId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroCientifico_MembroId",
                table: "MembroCientifico",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroOrganizador_ComissaoOrganizadoraId",
                table: "MembroOrganizador",
                column: "ComissaoOrganizadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroOrganizador_MembroId",
                table: "MembroOrganizador",
                column: "MembroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_ComissaoCientifica_ComissaoCientificaId",
                table: "Evento",
                column: "ComissaoCientificaId",
                principalTable: "ComissaoCientifica",
                principalColumn: "ComissaoCientificaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_ComissaoOrganizadora_ComissaoOrganizadoraId",
                table: "Evento",
                column: "ComissaoOrganizadoraId",
                principalTable: "ComissaoOrganizadora",
                principalColumn: "ComissaoOrganizadoraId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_ComissaoCientifica_ComissaoCientificaId",
                table: "Evento");

            migrationBuilder.DropForeignKey(
                name: "FK_Evento_ComissaoOrganizadora_ComissaoOrganizadoraId",
                table: "Evento");

            migrationBuilder.DropTable(
                name: "DataImportante");

            migrationBuilder.DropTable(
                name: "MembroCientifico");

            migrationBuilder.DropTable(
                name: "MembroOrganizador");

            migrationBuilder.DropTable(
                name: "ComissaoCientifica");

            migrationBuilder.DropTable(
                name: "ComissaoOrganizadora");

            migrationBuilder.DropIndex(
                name: "IX_Evento_ComissaoCientificaId",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_ComissaoOrganizadoraId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "ComissaoCientificaId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "ComissaoOrganizadoraId",
                table: "Evento");
        }
    }
}
