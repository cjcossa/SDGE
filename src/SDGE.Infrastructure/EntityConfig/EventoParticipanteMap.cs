using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class EventoParticipanteMap : IEntityTypeConfiguration<EventoParticipante>
    {
        public void Configure(EntityTypeBuilder<EventoParticipante> builder)
        {
            builder.HasKey(p => p.EventoParticipanteId);

            //com evento
            builder.HasOne(mt => mt.Evento)
                .WithMany(m => m.EventoParticipantes)
                .HasForeignKey(m => m.EventoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //com participante
            builder.HasOne(mt => mt.Participante)
                .WithMany(m => m.EventoParticipantes)
                .HasForeignKey(m => m.ParticipanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(i => i.Comprovativo)
           .HasColumnType("varchar(250)")
           .IsRequired();

        }
    }
}
