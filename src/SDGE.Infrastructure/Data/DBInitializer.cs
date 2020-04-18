using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Data
{
    public static class DBInitializer
    {
        public static void Initialize(ParticipanteContext context)
        {
            if(context.Participantes.Any())
            {
                return;
            }

            /*var participantes = new Participante[]
            {
                new Participante
                {
                    Nome = "Charles",
                    Ocupacao = "Estudante",
                    Instituicao = "UP",
                    Email = "carlos@gmail.com",
                    Telefone = "+258825345637",
                    TituloAcademico ="Licenciado"
                },
                new Participante
                {
                    Nome = "Cossa",
                    Ocupacao = "Estudante",
                    Instituicao = "UP",
                    Email = "cossa@gmail.com",
                    Telefone = "+258825345639",
                    TituloAcademico ="Licenciado"
                }
            };
            context.AddRange(participantes);
            var tipos = new Tipo[]
            {
                new Tipo
                {
                    Titulo = "Resumo",
                    Ficheiro = "Regras"
                },
                new Tipo
                {
                    Titulo = "Artigo",
                    Ficheiro = "Regras"
                }
            };
            context.AddRange(tipos);
            var submissoes = new Submissao[]
            {
                new Submissao
                {
                    Titulo ="Conferencia",
                    Ficheiro = "Resumo.doc",
                    Status = "Activo",
                    Descricao = "Descriacao da conferencia",
                    Participante = participantes[0],
                    Tipo = tipos[0]
                },
                new Submissao
                {
                    Titulo ="Conferencia",
                    Ficheiro = "Artigo.doc",
                    Status = "Activo",
                    Descricao = "Descriacao da conferencia",
                    Participante = participantes[1],
                    Tipo = tipos[1]
                }
            };
            context.AddRange(submissoes);
            context.SaveChanges();*/
        }
    }
}
