using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class TipoMap : IEntityTypeConfiguration<Tipo>
    {
        public void Configure(EntityTypeBuilder<Tipo> builder)
        {
            builder.HasKey(p => p.TipoId);

            //com submissao
            builder.HasMany(p => p.Submissoes)
                .WithOne(p => p.Tipo)
                .HasForeignKey(p => p.TipoId)
                .HasPrincipalKey(p => p.TipoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Titulo)
               .HasColumnType("varchar(500)")
               .IsRequired();

            builder.Property(t => t.Ficheiro)
               .HasColumnType("varchar(250)")
               .IsRequired();
        }
    }
}
