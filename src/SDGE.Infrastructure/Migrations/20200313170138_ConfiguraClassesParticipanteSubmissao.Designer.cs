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
    [Migration("20200313170138_ConfiguraClassesParticipanteSubmissao")]
    partial class ConfiguraClassesParticipanteSubmissao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Ficheiro")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.HasKey("SubmissaoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Submissao");
                });

            modelBuilder.Entity("SDGE.ApplicationCore.Entity.Submissao", b =>
                {
                    b.HasOne("SDGE.ApplicationCore.Entity.Participante", "Participante")
                        .WithMany("Submissoes")
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}