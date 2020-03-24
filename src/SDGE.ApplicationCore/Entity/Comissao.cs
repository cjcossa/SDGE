using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Comissao
    {
        public Comissao()
        { 
        
        }

        public int ComissaoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public ICollection<Membro> Membros { get; set; }
        public ICollection<Alerta> Alertas { get; set; }

    }
}
