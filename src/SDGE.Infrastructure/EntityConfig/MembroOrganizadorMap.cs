using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class MembroOrganizadorMap : IEntityTypeConfiguration<MembroOrganizador>
    {
        public void Configure(EntityTypeBuilder<MembroOrganizador> builder)
        {
            builder.HasKey(p => p.MembroOrganizadorId);

            //com membro
            builder.HasOne(mt => mt.Membro)
                .WithMany(m => m.MembroOrganizadors)
                .HasForeignKey(m => m.MembroId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //com comissao cientifica
            builder.HasOne(mt => mt.ComissaoOrganizadora)
                .WithMany(m => m.MembroOrganizadors)
                .HasForeignKey(m => m.ComissaoOrganizadoraId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
