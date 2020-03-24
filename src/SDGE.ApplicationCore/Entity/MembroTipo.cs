using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class MembroTipo
    {
        public MembroTipo()
        {

        }

        public int MembroTipoId { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
    }
}
