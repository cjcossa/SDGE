using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class MembroCientifico
    {
        public MembroCientifico()
        {
            this.Removido = false;
            this.Confirmado = false;
        }
        public int MembroCientificoId { get; set; }
        public bool Removido { get; set; }
        public bool Confirmado { get; set; }
        public int ComissaoCientificaId { get; set; }
        public ComissaoCientifica ComissaoCientifica { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
}
