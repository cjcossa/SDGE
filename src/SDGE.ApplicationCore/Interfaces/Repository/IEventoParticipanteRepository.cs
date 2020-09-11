using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IEventoParticipanteRepository : IRepository<EventoParticipante>
    {
        public IEnumerable<EventoParticipante> ObterJoinPorId(int id);
        public IEnumerable<EventoParticipante> ObterPorParticipante(int id);
        public bool VerificarEvento(int eventoId, int participanteId, bool state = false);
        public IEnumerable<EventoParticipante> ObterPorEvento(int id);
        public IEnumerable<EventoParticipante> ObterPorMembro(int id);
        public EventoParticipante ObterPorEventoParticipante(int eventoId, int participanteId);
        public EventoParticipante ObterPorEventoParticipante(int eventoId, int participanteId, bool state = true);
        public int Total(int id);
        
    }
}
