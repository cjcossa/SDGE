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
            /*if(context.Participantes.Any())
            {
                return;
            }*/
            if (context.Tipos.Any())
            {
                return;
            }
            var tipos = new Tipo[]
            {
                new Tipo
                {
                    Titulo = "Resumo",
                    Ficheiro = "Regras.pdf"
                },
                new Tipo
                {
                    Titulo = "Artigo",
                    Ficheiro = "Regras.pdf"
                }
            };
            context.AddRange(tipos);
            context.SaveChanges();
        }
    }
}
