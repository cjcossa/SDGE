using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Tipo
    {
        public Tipo()
        {
            this.Removido = false;
        }

        public int TipoId { get; set; }
        public string Titulo { get; set; }
        public string Ficheiro { get; set; }
        public bool Removido { get; set; }
        public ICollection<Submissao> Submissoes { get; set; }

    }
}
