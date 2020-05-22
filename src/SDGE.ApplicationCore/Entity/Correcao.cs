using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Correcao
    {
        public Correcao()
        {

        }

        public int CorrecaoId { get; set; }
        public string Ficheiro { get; set; }
        public string Observacoes { get; set; }
        public int SubmissaoId { get; set; }
        public Submissao Submissao { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
    public class SubmeterCorrecao
    {
        public int CorrecaoId { get; set; }
        public string Ficheiro { get; set; }
        public IFormFile File { get; set; }
        public string Observacoes { get; set; }
        public int SubmissaoId { get; set; }
        public Submissao Submissao { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
}
