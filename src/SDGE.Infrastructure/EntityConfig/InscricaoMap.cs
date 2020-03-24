using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class InscricaoMap : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            //com participante
            builder.HasOne(s => s.Participante)
                  .WithMany(s => s.Inscricoes)
                  .HasForeignKey(s => s.ParticipanteId)
                  .HasPrincipalKey(s => s.ParticipanteId)
                  .OnDelete(DeleteBehavior.Restrict);

            //com evento
            builder.HasOne(s => s.Evento)
                .WithMany(s => s.Inscricoes)
                .HasForeignKey(s => s.EventoId)
                .HasPrincipalKey(s => s.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(i => i.Comprovativo)
            .HasColumnType("varchar(250)")
            .IsRequired();

            builder.Property(i => i.Status)
               .HasColumnType("varchar(50)")
               .IsRequired();
        }
    }
}
