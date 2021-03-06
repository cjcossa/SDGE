﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDGE.Infrastructure.Data;

namespace SDGE.Infrastructure.Migrations
{
    [DbContext(typeof(ParticipanteContext))]
    [Migration("20200609222326_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Alerta", b =>
                {
                    b.Property<int>("AlertaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Messagem")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("AlertaId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Alerta");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Correcao", b =>
                {
                    b.Property<int>("CorrecaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ficheiro")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("MembroId")
                        .HasColumnType("int");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("SubmissaoId")
                        .HasColumnType("int");

                    b.HasKey("CorrecaoId");

                    b.HasIndex("MembroId");

                    b.HasIndex("SubmissaoId")
                        .IsUnique();

                    b.ToTable("Correcao");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Evento", b =>
                {
                    b.Property<int>("EventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DataFim")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DataInicio")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Lema")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("EventoId");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.EventoParticipante", b =>
                {
                    b.Property<int>("EventoParticipanteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comprovativo")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("EventoParticipanteId");

                    b.HasIndex("EventoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("EventoParticipante");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Membro", b =>
                {
                    b.Property<int>("MembroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("MembroId");

                    b.ToTable("Membro");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.MembroEvento", b =>
                {
                    b.Property<int>("MembroEventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comissao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("MembroId")
                        .HasColumnType("int");

                    b.HasKey("MembroEventoId");

                    b.HasIndex("EventoId");

                    b.HasIndex("MembroId");

                    b.ToTable("MembroEvento");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Participante", b =>
                {
                    b.Property<int>("ParticipanteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Instituicao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Ocupacao")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TituloAcademico")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ParticipanteId");

                    b.ToTable("Participante");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Submissao", b =>
                {
                    b.Property<int>("SubmissaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<string>("Ficheiro")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("SubmissaoId");

                    b.HasIndex("EventoId");

                    b.HasIndex("ParticipanteId");

                    b.HasIndex("TipoId");

                    b.ToTable("Submissao");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Tipo", b =>
                {
                    b.Property<int>("TipoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ficheiro")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("TipoId");

                    b.ToTable("Tipo");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Alerta", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Participante", "Participante")
                        .WithMany("Alertas")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Correcao", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Membro", "Membro")
                        .WithMany("Correcoes")
                        .HasForeignKey("MembroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SDGE.ApplicationCore.Entity.Submissao", "Submissao")
                        .WithOne("Correcao")
                        .HasForeignKey("SDGE.ApplicationCore.Entity.Correcao", "SubmissaoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.EventoParticipante", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Evento", "Evento")
                        .WithMany("EventoParticipantes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SDGE.ApplicationCore.Entity.Participante", "Participante")
                        .WithMany("EventoParticipantes")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.MembroEvento", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Evento", "Evento")
                        .WithMany("MembroEventos")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SDGE.ApplicationCore.Entity.Membro", "Membro")
                        .WithMany("MembroEventos")
                        .HasForeignKey("MembroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Submissao", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Evento", "Evento")
                        .WithMany("Submissoes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SDGE.ApplicationCore.Entity.Participante", "Participante")
                        .WithMany("Submissoes")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SDGE.ApplicationCore.Entity.Tipo", "Tipo")
                        .WithMany("Submissoes")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
