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
            Database.EnsureCreated();
        }
        
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<EventoParticipante> EventoParticipantes { get; set; }
        public DbSet<Submissao> Submissoes { get; set; }
        public DbSet<Correcao> Correcoes { get; set; }
        public DbSet<MembroEvento> MembroEventos { get; set; }
        public DbSet<DataImportante> DataImportantes { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<ComissaoOrganizadora> ComissaoOrganizadoras { get; set; }
        public DbSet<ComissaoCientifica> ComissaoCientificas { get; set; }
        public DbSet<MembroCientifico> MembroCientificos { get; set; }
        public DbSet<MembroOrganizador> MembroOrganizadors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participante>().ToTable("Participante");
            modelBuilder.Entity<Submissao>().ToTable("Submissao");
            modelBuilder.Entity<Evento>().ToTable("Evento");
            modelBuilder.Entity<Tipo>().ToTable("Tipo");
            modelBuilder.Entity<Membro>().ToTable("Membro");
            modelBuilder.Entity<EventoParticipante>().ToTable("EventoParticipante");
            modelBuilder.Entity<Correcao>().ToTable("Correcao");
            modelBuilder.Entity<MembroEvento>().ToTable("MembroEvento");
            modelBuilder.Entity<Alerta>().ToTable("Alerta");
            modelBuilder.Entity<ComissaoCientifica>().ToTable("ComissaoCientifica");
            modelBuilder.Entity<ComissaoOrganizadora>().ToTable("ComissaoOrganizadora");
            modelBuilder.Entity<MembroOrganizador>().ToTable("MembroOrganizador");
            modelBuilder.Entity<MembroCientifico>().ToTable("MembroCientifico");
            modelBuilder.Entity<DataImportante>().ToTable("DataImportante");

            modelBuilder.ApplyConfiguration(new ParticipanteMap());
            modelBuilder.ApplyConfiguration(new SubmissaoMap());
            modelBuilder.ApplyConfiguration(new EventoMap());
            modelBuilder.ApplyConfiguration(new TipoMap());
            modelBuilder.ApplyConfiguration(new MembroMap());
            modelBuilder.ApplyConfiguration(new CorrecaoMap());
            modelBuilder.ApplyConfiguration(new EventoParticipanteMap());
            modelBuilder.ApplyConfiguration(new AlertaMap());
            modelBuilder.ApplyConfiguration(new MembroEventoMap());
            modelBuilder.ApplyConfiguration(new MembroCientificoMap());
            modelBuilder.ApplyConfiguration(new MembroOrganizadorMap());
            modelBuilder.ApplyConfiguration(new ComissaoOrganizadoraMap());
            modelBuilder.ApplyConfiguration(new ComissaoCientificaMap());
            modelBuilder.ApplyConfiguration(new DataImportanteMap());

        }
    }
}
