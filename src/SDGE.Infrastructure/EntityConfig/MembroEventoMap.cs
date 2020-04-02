using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class MembroEventoMap : IEntityTypeConfiguration<MembroEvento>
    {
        public void Configure(EntityTypeBuilder<MembroEvento> builder)
        {
            builder.HasKey(mt => mt.MembroEventoId);

            //com evento
            builder.HasOne(mt => mt.Evento)
                .WithMany(m => m.MembroEventos)
                .HasForeignKey(m => m.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            //com membro
            builder.HasOne(mt => mt.Membro)
                .WithMany(m => m.MembroEventos)
                .HasForeignKey(m => m.MembroId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
