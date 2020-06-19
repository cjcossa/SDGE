using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    EventoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(500)", nullable: false),
                    Lema = table.Column<string>(type: "varchar(500)", nullable: false),
                    Email = table.Column<string>(type: "varchar(300)", nullable: false),
                    DataInico = table.Column<string>(type: "varchar(50)", nullable: false),
                    DataFim = table.Column<string>(type: "varchar(50)", nullable: false),
                    Local = table.Column<string>(type: "varchar(300)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.EventoId);
                });

            migrationBuilder.CreateTable(
                name: "Membro",
                columns: table => new
                {
                    MembroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membro", x => x.MembroId);
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    ParticipanteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Ocupacao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Instituicao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(100)", nullable: false),
                    TituloAcademico = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.ParticipanteId);
                });

            migrationBuilder.CreateTable(
                name: "Tipo",
                columns: table => new
                {
                    TipoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(500)", nullable: false),
                    Ficheiro = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.TipoId);
                });

            migrationBuilder.CreateTable(
                name: "MembroEvento",
                columns: table => new
                {
                    MembroEventoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comissao = table.Column<string>(nullable: true),
                    EventoId = table.Column<int>(nullable: false),
                    MembroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroEvento", x => x.MembroEventoId);
                    table.ForeignKey(
                        name: "FK_MembroEvento_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembroEvento_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "MembroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    AlertaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Messagem = table.Column<string>(type: "varchar(250)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.AlertaId);
                    table.ForeignKey(
                        name: "FK_Alerta_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventoParticipante",
                columns: table => new
                {
                    EventoParticipanteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comprovativo = table.Column<string>(type: "varchar(250)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    EventoId = table.Column<int>(nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoParticipante", x => x.EventoParticipanteId);
                    table.ForeignKey(
                        name: "FK_EventoParticipante_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventoParticipante_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Submissao",
                columns: table => new
                {
                    SubmissaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(500)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Ficheiro = table.Column<string>(type: "varchar(250)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    TipoId = table.Column<int>(nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false),
                    EventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissao", x => x.SubmissaoId);
                    table.ForeignKey(
                        name: "FK_Submissao_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissao_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissao_Tipo_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipo",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Correcao",
                columns: table => new
                {
                    CorrecaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ficheiro = table.Column<string>(type: "varchar(250)", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(1000)", nullable: false),
                    SubmissaoId = table.Column<int>(nullable: false),
                    MembroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correcao", x => x.CorrecaoId);
                    table.ForeignKey(
                        name: "FK_Correcao_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "MembroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Correcao_Submissao_SubmissaoId",
                        column: x => x.SubmissaoId,
                        principalTable: "Submissao",
                        principalColumn: "SubmissaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_ParticipanteId",
                table: "Alerta",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Correcao_MembroId",
                table: "Correcao",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventoParticipante_EventoId",
                table: "EventoParticipante",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoParticipante_ParticipanteId",
                table: "EventoParticipante",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEvento_EventoId",
                table: "MembroEvento",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEvento_MembroId",
                table: "MembroEvento",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissao_EventoId",
                table: "Submissao",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissao_ParticipanteId",
                table: "Submissao",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissao_TipoId",
                table: "Submissao",
                column: "TipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alerta");

            migrationBuilder.DropTable(
                name: "Correcao");

            migrationBuilder.DropTable(
                name: "EventoParticipante");

            migrationBuilder.DropTable(
                name: "MembroEvento");

            migrationBuilder.DropTable(
                name: "Submissao");

            migrationBuilder.DropTable(
                name: "Membro");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Tipo");
        }
    }
}
