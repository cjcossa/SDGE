using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class MembroEvento
    {
        public MembroEvento()
        {

        }

        public int MembroEventoId { get; set; }
        public string Comissao { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
}
