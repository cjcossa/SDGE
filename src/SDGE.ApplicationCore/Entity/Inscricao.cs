using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class Inscricao
    {
        public Inscricao()
        {
        }

        public int InscricaoId { get; set; }
        public string  Comprovativo { get; set; }
        public string  Status { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}
