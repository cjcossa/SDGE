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

            var participantes = new Participante[]
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
            var submissoes = new Submissao[]
            {
                new Submissao
                {
                    Titulo ="Conferencia",
                    Tipo = "Resumo",
                    Ficheiro = "Resumo.doc",
                    Status = "Activo",
                    Descricao = "Descriacao da conferencia",
                    Participante = participantes[0]
                },
                new Submissao
                {
                    Titulo ="Conferencia",
                    Tipo = "Artigo",
                    Ficheiro = "Artigo.doc",
                    Status = "Activo",
                    Descricao = "Descriacao da conferencia",
                    Participante = participantes[1]
                }
            };
            context.AddRange(submissoes);
            context.SaveChanges();
        }
    }
}
