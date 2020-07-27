using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class EventoParticipanteRepository : EFRepository<EventoParticipante>, IEventoParticipanteRepository
    {

        public EventoParticipanteRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public override IEnumerable<EventoParticipante> ObterTodos()
        {
            return _dbContext.Set<EventoParticipante>().Include(m => m.Participante).Include(m => m.Evento)
                .Where(e => e.Removido == state && e.Participante.Removido == state && e.Evento.Removido == state);
        }
        public IEnumerable<EventoParticipante> ObterJoinPorId(int id)
        {
            return ObterTodos().Where(m => m.EventoParticipanteId == id);
        }

        public IEnumerable<EventoParticipante> ObterPorParticipante(int id)
        {
            return _dbContext.Set<EventoParticipante>().Include(m => m.Participante).Include(m => m.Evento)
                .Where(e => e.ParticipanteId == id && e.Removido == state && e.Participante.Removido == state && e.Evento.Removido == state);
        }

        public bool VerificarEvento(int eventoId, int participanteId, bool state = false)
        {
            var result = _dbContext.Set<EventoParticipante>()
                .Where(c => c.EventoId == eventoId && c.ParticipanteId == participanteId && c.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }

        public IEnumerable<EventoParticipante> ObterPorEvento(int id)
        {
            return _dbContext.Set<EventoParticipante>().Include(m => m.Participante).Include(m => m.Evento)
            .Where(e => e.EventoId == id && e.Removido == state && e.Participante.Removido == state && e.Evento.Removido == state);
        }

        public EventoParticipante ObterPorEventoParticipante(int eventoId, int participanteId)
        {
            return _dbContext.Set<EventoParticipante>().Include(m => m.Participante).Include(m => m.Evento)
                .Where(c => c.EventoId == eventoId && c.ParticipanteId == participanteId && c.Removido == state &&
                c.Participante.Removido == state && c.Evento.Removido == state).FirstOrDefault();
        }
        public override void Remover(EventoParticipante entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }

        public EventoParticipante ObterPorEventoParticipante(int eventoId, int participanteId, bool state = true)
        {
            return _dbContext.Set<EventoParticipante>()
                 .Where(c => c.EventoId == eventoId && c.ParticipanteId == participanteId && c.Removido == state).FirstOrDefault();
        }
    }
}
