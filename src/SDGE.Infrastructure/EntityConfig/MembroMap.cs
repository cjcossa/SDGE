using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class MembroMap : IEntityTypeConfiguration<Membro>
    {
        public void Configure(EntityTypeBuilder<Membro> builder)
        {
            //com Comissao
            builder.HasOne(s => s.Comissao)
                  .WithMany(s => s.Membros)
                  .HasForeignKey(s => s.ComissaoId)
                  .HasPrincipalKey(s => s.ComissaoId)
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
