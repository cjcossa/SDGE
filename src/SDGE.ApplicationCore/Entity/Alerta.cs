using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Alerta
    {
        public Alerta()
        {
            this.Removido = false;
        }

        public int AlertaId { get; set; }
        public string Messagem { get; set; }
        public string Status { get; set; }
        public bool Removido { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
       
    }
}
