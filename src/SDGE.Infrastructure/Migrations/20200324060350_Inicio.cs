using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class Inicio : Migration
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
                    DataInico = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    Local = table.Column<string>(type: "varchar(300)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.EventoId);
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
                name: "Comissao",
                columns: table => new
                {
                    ComissaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    EventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissao", x => x.ComissaoId);
                    table.ForeignKey(
                        name: "FK_Comissao_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    InscricaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comprovativo = table.Column<string>(type: "varchar(250)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false),
                    EventoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.InscricaoId);
                    table.ForeignKey(
                        name: "FK_Inscricao_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "EventoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscricao_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissao_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissao_Tipo_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipo",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alerta",
                columns: table => new
                {
                    AlertaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Messagem = table.Column<string>(type: "varchar(250)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    ParticipanteId = table.Column<int>(nullable: false),
                    ComissaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerta", x => x.AlertaId);
                    table.ForeignKey(
                        name: "FK_Alerta_Comissao_ComissaoId",
                        column: x => x.ComissaoId,
                        principalTable: "Comissao",
                        principalColumn: "ComissaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alerta_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "ParticipanteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membro",
                columns: table => new
                {
                    MembroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    ComissaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membro", x => x.MembroId);
                    table.ForeignKey(
                        name: "FK_Membro_Comissao_ComissaoId",
                        column: x => x.ComissaoId,
                        principalTable: "Comissao",
                        principalColumn: "ComissaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Correcao",
                columns: table => new
                {
                    CorrecaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ficheiro = table.Column<string>(type: "varchar(250)", nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(1000)", nullable: false),
                    SubmissaoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correcao", x => x.CorrecaoId);
                    table.ForeignKey(
                        name: "FK_Correcao_Submissao_SubmissaoId",
                        column: x => x.SubmissaoId,
                        principalTable: "Submissao",
                        principalColumn: "SubmissaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembroTipo",
                columns: table => new
                {
                    MembroTipoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembroId = table.Column<int>(nullable: false),
                    TipoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroTipo", x => x.MembroTipoId);
                    table.ForeignKey(
                        name: "FK_MembroTipo_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "MembroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembroTipo_Tipo_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Tipo",
                        principalColumn: "TipoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_ComissaoId",
                table: "Alerta",
                column: "ComissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerta_ParticipanteId",
                table: "Alerta",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissao_EventoId",
                table: "Comissao",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Correcao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_EventoId",
                table: "Inscricao",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Membro_ComissaoId",
                table: "Membro",
                column: "ComissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroTipo_MembroId",
                table: "MembroTipo",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroTipo_TipoId",
                table: "MembroTipo",
                column: "TipoId");

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
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "MembroTipo");

            migrationBuilder.DropTable(
                name: "Submissao");

            migrationBuilder.DropTable(
                name: "Membro");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Tipo");

            migrationBuilder.DropTable(
                name: "Comissao");

            migrationBuilder.DropTable(
                name: "Evento");
        }
    }
}
