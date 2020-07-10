using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class DataImportanteMap : IEntityTypeConfiguration<DataImportante>
    {
        public void Configure(EntityTypeBuilder<DataImportante> builder)
        {
            //com evento
            builder.HasOne(s => s.Evento)
                  .WithMany(s => s.DataImportantes)
                  .HasForeignKey(s => s.EventoId)
                  .HasPrincipalKey(s => s.EventoId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(s => s.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(s => s.DataInicio)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(s => s.DataFim)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(s => s.Finalidade)
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
