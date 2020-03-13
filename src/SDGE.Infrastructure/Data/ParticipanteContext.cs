using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SDGE.ApplicationCore.Entity;
using System.Text;

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

            #region Participante

            modelBuilder.Entity<Participante>().Property(e => e.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Participante>().Property(e => e.Ocupacao)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Participante>().Property(e => e.Instituicao)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Participante>().Property(e => e.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Participante>().Property(e => e.Telefone)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Participante>().Property(e => e.TituloAcademico)
                .HasColumnType("varchar(100)")
                .IsRequired();

            #endregion

            #region Submissao

            modelBuilder.Entity<Submissao>().Property(e => e.Titulo)
                .HasColumnType("varchar(250)")
                .IsRequired();
            modelBuilder.Entity<Submissao>().Property(e => e.Tipo)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<Submissao>().Property(e => e.Ficheiro)
                .HasColumnType("varchar(250)")
                .IsRequired();
            modelBuilder.Entity<Submissao>().Property(e => e.Status)
                .HasColumnType("varchar(50)")
                .IsRequired();
            modelBuilder.Entity<Submissao>().Property(e => e.Descricao)
                .HasColumnType("varchar(500)")
                .IsRequired();

            #endregion
        }
    }
}
