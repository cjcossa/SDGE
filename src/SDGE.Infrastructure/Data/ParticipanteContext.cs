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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participante>().ToTable("Participante");
        }
    }
}
