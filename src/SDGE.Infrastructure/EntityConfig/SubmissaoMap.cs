using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class SubmissaoMap : IEntityTypeConfiguration<Submissao>
    {
        public void Configure(EntityTypeBuilder<Submissao> builder)
        {
            //com participante
            builder.HasOne(s => s.Participante)
                  .WithMany(s => s.Submissoes)
                  .HasForeignKey(s => s.ParticipanteId)
                  .HasPrincipalKey(s => s.ParticipanteId)
                  .OnDelete(DeleteBehavior.Restrict);

            //com correcao
            builder.HasMany(p => p.Correcoes)
               .WithOne(p => p.Submissao)
               .HasForeignKey(p => p.SubmissaoId)
               .HasPrincipalKey(p => p.SubmissaoId)
               .OnDelete(DeleteBehavior.Restrict);

            //com evento
            builder.HasOne(s => s.Evento)
                .WithMany(s => s.Submissoes)
                .HasForeignKey(s => s.EventoId)
                .HasPrincipalKey(s => s.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            //com tipo
            builder.HasOne(s => s.Tipo)
                .WithMany(s => s.Submissoes)
                .HasForeignKey(s => s.TipoId)
                .HasPrincipalKey(s => s.TipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.Titulo)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(s => s.Ficheiro)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(s => s.Status)
                .HasColumnType("varchar(50)");

            builder.Property(s => s.Descricao)
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.Property(s => s.Observacoes)
                .HasColumnType("varchar(1000)");
        }
    }
}
