using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.HasKey(p => p.EventoId);

            //com submissao
            builder.HasMany(p => p.Submissoes)
                .WithOne(p => p.Evento)
                .HasForeignKey(p => p.EventoId)
                .HasPrincipalKey(p => p.EventoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Titulo)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(e => e.Lema)
               .HasColumnType("varchar(500)")
               .IsRequired();

            builder.Property(e => e.Local)
               .HasColumnType("varchar(300)")
               .IsRequired();

            builder.Property(e => e.Email)
               .HasColumnType("varchar(300)")
               .IsRequired();

            builder.Property(e => e.Descricao)
               .HasColumnType("varchar(1000)")
               .IsRequired();

            builder.Property(e => e.Categoria)
               .HasColumnType("varchar(100)")
               .IsRequired();
        }
    }
}
