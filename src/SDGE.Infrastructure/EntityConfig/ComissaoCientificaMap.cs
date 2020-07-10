using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class ComissaoCientificaMap : IEntityTypeConfiguration<ComissaoCientifica>
    {
        public void Configure(EntityTypeBuilder<ComissaoCientifica> builder)
        {

            //com evento
            builder.HasMany(e => e.Eventos)
                  .WithOne(c => c.ComissaoCientifica)
                  .HasForeignKey(s => s.ComissaoCientificaId)
                  .HasPrincipalKey(s => s.ComissaoCientificaId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Codigo)
               .HasColumnType("varchar(250)")
               .IsRequired();
        }
    }
}
