using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class EventoParticipante
    {
        public EventoParticipante()
        {
            this.Removido = false;
            this.Confirmado = false;
        }

        public int EventoParticipanteId { get; set; }
        public string Comprovativo { get; set; }
        public bool Confirmado { get; set; }
        public bool Removido { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
    public class Inscricao
    {
        
        public int EventoParticipanteId { get; set; }
        public string Comprovativo { get; set; }
        public IFormFile File { get; set; }
        public string Status { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}
