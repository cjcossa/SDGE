using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class CorrecaoMap : IEntityTypeConfiguration<Correcao>
    {
        public void Configure(EntityTypeBuilder<Correcao> builder)
        {
            builder.Property(c => c.Ficheiro)
             .HasColumnType("varchar(250)")
             .IsRequired();

            builder.Property(c => c.Observacoes)
               .HasColumnType("varchar(1000)")
               .IsRequired();
        }
    }
}
