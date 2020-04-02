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
            //com membro
            builder.HasOne(s => s.Membro)
                  .WithMany(s => s.Correcoes)
                  .HasForeignKey(s => s.MembroId)
                  .HasPrincipalKey(s => s.MembroId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Ficheiro)
             .HasColumnType("varchar(250)")
             .IsRequired();

            builder.Property(c => c.Observacoes)
               .HasColumnType("varchar(1000)")
               .IsRequired();
        }
    }
}
