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
    }
}
