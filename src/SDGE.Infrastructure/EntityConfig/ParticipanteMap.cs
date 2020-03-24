using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class ParticipanteMap : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.HasKey(p => p.ParticipanteId);

            //com submissao
            builder.HasMany(p => p.Submissoes)
                .WithOne(p => p.Participante)
                .HasForeignKey(p => p.ParticipanteId)
                .HasPrincipalKey(p => p.ParticipanteId)
                .OnDelete(DeleteBehavior.Restrict);

            //com Inscricao
            builder.HasMany(p => p.Inscricoes)
                .WithOne(p => p.Participante)
                .HasForeignKey(p => p.ParticipanteId)
                .HasPrincipalKey(p => p.ParticipanteId)
                .OnDelete(DeleteBehavior.Restrict);

            //com Alerta
            builder.HasMany(p => p.Alertas)
                .WithOne(p => p.Participante)
                .HasForeignKey(p => p.ParticipanteId)
                .HasPrincipalKey(p => p.ParticipanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Ocupacao)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Instituicao)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Telefone)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.TituloAcademico)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
