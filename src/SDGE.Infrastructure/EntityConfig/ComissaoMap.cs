using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class ComissaoMap : IEntityTypeConfiguration<Comissao>
    {
        public void Configure(EntityTypeBuilder<Comissao> builder)
        {
            builder.HasKey(p => p.ComissaoId);

            //com Membro
            builder.HasMany(p => p.Membros)
                .WithOne(p => p.Comissao)
                .HasForeignKey(p => p.ComissaoId)
                .HasPrincipalKey(p => p.ComissaoId)
                .OnDelete(DeleteBehavior.Restrict);

            //com Alerta
            builder.HasMany(p => p.Alertas)
                .WithOne(p => p.Comissao)
                .HasForeignKey(p => p.ComissaoId)
                .HasPrincipalKey(p => p.ComissaoId)
                .OnDelete(DeleteBehavior.Restrict);

            //com evento
            builder.HasOne(s => s.Evento)
                .WithMany(s => s.Comissoes)
                .HasForeignKey(s => s.EventoId)
                .HasPrincipalKey(s => s.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Nome)
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(c => c.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();
        }
    }
}
