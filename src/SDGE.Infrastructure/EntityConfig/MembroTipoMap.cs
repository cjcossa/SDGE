using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class MembroTipoMap : IEntityTypeConfiguration<MembroTipo>
    {
        public void Configure(EntityTypeBuilder<MembroTipo> builder)
        {
            builder.HasKey(mt => mt.MembroTipoId);

            builder.HasOne(mt => mt.Membro)
                .WithMany(m => m.MembroTipos)
                .HasForeignKey(m => m.MembroId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mt => mt.Tipo)
                .WithMany(m => m.MembroTipos)
                .HasForeignKey(m => m.TipoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
