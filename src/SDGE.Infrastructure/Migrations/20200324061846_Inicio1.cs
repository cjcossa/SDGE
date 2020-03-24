using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class Inicio1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Comissao_ComissaoId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Participante_ParticipanteId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Comissao_Evento_EventoId",
                table: "Comissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Correcao_Submissao_SubmissaoId",
                table: "Correcao");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Evento_EventoId",
                table: "Inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Membro_Comissao_ComissaoId",
                table: "Membro");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroTipo_Membro_MembroId",
                table: "MembroTipo");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroTipo_Tipo_TipoId",
                table: "MembroTipo");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Evento_EventoId",
                table: "Submissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Participante_ParticipanteId",
                table: "Submissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Tipo_TipoId",
                table: "Submissao");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Comissao_ComissaoId",
                table: "Alerta",
                column: "ComissaoId",
                principalTable: "Comissao",
                principalColumn: "ComissaoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Participante_ParticipanteId",
                table: "Alerta",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comissao_Evento_EventoId",
                table: "Comissao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Correcao_Submissao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId",
                principalTable: "Submissao",
                principalColumn: "SubmissaoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Evento_EventoId",
                table: "Inscricao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Membro_Comissao_ComissaoId",
                table: "Membro",
                column: "ComissaoId",
                principalTable: "Comissao",
                principalColumn: "ComissaoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroTipo_Membro_MembroId",
                table: "MembroTipo",
                column: "MembroId",
                principalTable: "Membro",
                principalColumn: "MembroId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroTipo_Tipo_TipoId",
                table: "MembroTipo",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Evento_EventoId",
                table: "Submissao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Participante_ParticipanteId",
                table: "Submissao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Tipo_TipoId",
                table: "Submissao",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Comissao_ComissaoId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Alerta_Participante_ParticipanteId",
                table: "Alerta");

            migrationBuilder.DropForeignKey(
                name: "FK_Comissao_Evento_EventoId",
                table: "Comissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Correcao_Submissao_SubmissaoId",
                table: "Correcao");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Evento_EventoId",
                table: "Inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao");

            migrationBuilder.DropForeignKey(
                name: "FK_Membro_Comissao_ComissaoId",
                table: "Membro");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroTipo_Membro_MembroId",
                table: "MembroTipo");

            migrationBuilder.DropForeignKey(
                name: "FK_MembroTipo_Tipo_TipoId",
                table: "MembroTipo");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Evento_EventoId",
                table: "Submissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Participante_ParticipanteId",
                table: "Submissao");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissao_Tipo_TipoId",
                table: "Submissao");

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Comissao_ComissaoId",
                table: "Alerta",
                column: "ComissaoId",
                principalTable: "Comissao",
                principalColumn: "ComissaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alerta_Participante_ParticipanteId",
                table: "Alerta",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comissao_Evento_EventoId",
                table: "Comissao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Correcao_Submissao_SubmissaoId",
                table: "Correcao",
                column: "SubmissaoId",
                principalTable: "Submissao",
                principalColumn: "SubmissaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Evento_EventoId",
                table: "Inscricao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membro_Comissao_ComissaoId",
                table: "Membro",
                column: "ComissaoId",
                principalTable: "Comissao",
                principalColumn: "ComissaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroTipo_Membro_MembroId",
                table: "MembroTipo",
                column: "MembroId",
                principalTable: "Membro",
                principalColumn: "MembroId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MembroTipo_Tipo_TipoId",
                table: "MembroTipo",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Evento_EventoId",
                table: "Submissao",
                column: "EventoId",
                principalTable: "Evento",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Participante_ParticipanteId",
                table: "Submissao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "ParticipanteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissao_Tipo_TipoId",
                table: "Submissao",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
