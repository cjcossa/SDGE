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

            //com Comissao
            builder.HasOne(s => s.Comissao)
                  .WithMany(s => s.Alertas)
                  .HasForeignKey(s => s.ComissaoId)
                  .HasPrincipalKey(s => s.ComissaoId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.Messagem)
               .HasColumnType("varchar(250)")
               .IsRequired();

            builder.Property(a => a.Status)
               .HasColumnType("varchar(50)")
               .IsRequired();
        }
    }
}
