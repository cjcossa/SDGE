using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class FaculdadeMap : IEntityTypeConfiguration<Faculdade>
    {
        public void Configure(EntityTypeBuilder<Faculdade> builder)
        {
            builder.HasKey(p => p.FaculdadeId);

            //com director
            builder.HasMany(p => p.Directors)
                .WithOne(p => p.Faculdade)
                .HasForeignKey(p => p.FaculdadeId)
                .HasPrincipalKey(p => p.FaculdadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.Designacao)
            .HasColumnType("varchar(200)")
            .IsRequired();
        }
    }
}
