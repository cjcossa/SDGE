﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Entity
{
    public class EventoParticipante
    {
        public EventoParticipante()
        {

        }

        public int EventoParticipanteId { get; set; }
        public string Comprovativo { get; set; }
        public string Status { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}