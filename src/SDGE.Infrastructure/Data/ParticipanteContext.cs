using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SDGE.ApplicationCore.Entity;
using System.Text;
using SDGE.Infrastructure.EntityConfig;

namespace SDGE.Infrastructure.Data
{
    public class ParticipanteContext : DbContext 
    {
        public ParticipanteContext(DbContextOptions<ParticipanteContext> options) : base(options)
        {

        }
        
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Submissao> Submissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participante>().ToTable("Participante");
            modelBuilder.Entity<Submissao>().ToTable("Submissao");
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<Tipo>().ToTable("Tipo");
            modelBuilder.Entity<Membro>().ToTable("Membro");
            modelBuilder.Entity<Inscricao>().ToTable("Inscricao");
            modelBuilder.Entity<Correcao>().ToTable("Correcao");
            modelBuilder.Entity<Comissao>().ToTable("Comissao");
            modelBuilder.Entity<Alerta>().ToTable("Alerta");
            modelBuilder.Entity<MembroTipo>().ToTable("MembroTipo");

            modelBuilder.ApplyConfiguration(new ParticipanteMap());
            modelBuilder.ApplyConfiguration(new SubmissaoMap());
            modelBuilder.ApplyConfiguration(new EventoMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new MembroMap());
            modelBuilder.ApplyConfiguration(new InscricaoMap());
            modelBuilder.ApplyConfiguration(new CorrecaoMap());
            modelBuilder.ApplyConfiguration(new ComissaoMap());
            modelBuilder.ApplyConfiguration(new AlertaMap());
            modelBuilder.ApplyConfiguration(new MembroTipoMap());

        }
    }
}
