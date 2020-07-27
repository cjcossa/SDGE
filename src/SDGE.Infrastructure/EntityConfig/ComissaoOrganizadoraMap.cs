using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class ComissaoOrganizadoraMap : IEntityTypeConfiguration<ComissaoOrganizadora>
    {
        public void Configure(EntityTypeBuilder<ComissaoOrganizadora> builder)
        {

            //com evento
            builder.HasMany(e => e.Eventos)
                  .WithOne(c => c.ComissaoOrganizadora)
                  .HasForeignKey(s => s.ComissaoOrganizadoraId)
                  .HasPrincipalKey(s => s.ComissaoOrganizadoraId)
                  .OnDelete(DeleteBehavior.Restrict);

            //com Alerta
            builder.HasMany(p => p.Alertas)
                .WithOne(p => p.ComissaoOrganizadora)
                .HasForeignKey(p => p.ComissaoOrganizadoraId)
                .HasPrincipalKey(p => p.ComissaoOrganizadoraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Codigo)
               .HasColumnType("varchar(250)")
               .IsRequired();

          
        }
    }
}
