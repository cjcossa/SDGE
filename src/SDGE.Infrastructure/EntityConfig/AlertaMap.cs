using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.Infrastructure.EntityConfig
{
    public class AlertaMap : IEntityTypeConfiguration<Alerta>
    {
        public void Configure(EntityTypeBuilder<Alerta> builder)
        {

            //com participante
            builder.HasOne(s => s.Participante)
                  .WithMany(s => s.Alertas)
                  .HasForeignKey(s => s.ParticipanteId)
                  .HasPrincipalKey(s => s.ParticipanteId)
                  .OnDelete(DeleteBehavior.Restrict);

            //com comissao cientifica
            builder.HasOne(s => s.ComissaoCientifica)
                  .WithMany(s => s.Alertas)
                  .HasForeignKey(s => s.ComissaoCientificaId)
                  .HasPrincipalKey(s => s.ComissaoCientificaId)
                  .OnDelete(DeleteBehavior.Restrict);

            //com comissao  organizadora
            builder.HasOne(s => s.ComissaoOrganizadora)
                  .WithMany(s => s.Alertas)
                  .HasForeignKey(s => s.ComissaoOrganizadoraId)
                  .HasPrincipalKey(s => s.ComissaoOrganizadoraId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Messagem)
               .HasColumnType("varchar(250)")
               .IsRequired();

        }
    }
}
