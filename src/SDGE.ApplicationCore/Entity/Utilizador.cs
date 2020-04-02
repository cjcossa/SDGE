using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Utilizador
    {
        public Utilizador()
        {

        }

        public int UtilizadorId { get; set; }
        public string Titulo { get; set; }
        public string Ficheiro { get; set; }
        public ICollection<Submissao> Submissoes { get; set; }

    }
}
