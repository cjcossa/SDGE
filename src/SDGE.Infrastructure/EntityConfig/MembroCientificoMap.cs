using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class MembroCientificoMap : IEntityTypeConfiguration<MembroCientifico>
    {
        public void Configure(EntityTypeBuilder<MembroCientifico> builder)
        {
            builder.HasKey(p => p.MembroCientificoId);

            //com membro
            builder.HasOne(mt => mt.Membro)
                .WithMany(m => m.MembroCientificos)
                .HasForeignKey(m => m.MembroId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //com comissao cientifica
            builder.HasOne(mt => mt.ComissaoCientifica)
                .WithMany(m => m.MembroCientificos)
                .HasForeignKey(m => m.ComissaoCientificaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
