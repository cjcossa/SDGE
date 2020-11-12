using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class DirectorMap : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasKey(p => p.DirectorId);

            //com faculdade
            builder.HasOne(d => d.Faculdade)
                .WithMany(d => d.Directors)
                .HasForeignKey(s => s.FaculdadeId)
                .HasPrincipalKey(s => s.FaculdadeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(m => m.Nome)
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(m => m.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(m => m.Telefone)
               .HasColumnType("varchar(20)")
               .IsRequired();
        }
    }
}
