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
        public override IEnumerable<EventoParticipante> ObterTodos()
        {
            return _dbContext.Set<EventoParticipante>().Include(m => m.Participante).Include(m => m.Evento);
        }
        public IEnumerable<EventoParticipante> ObterJoinPorId(int id)
        {
            return ObterTodos().Where(m => m.EventoParticipanteId == id);
        }
    }
}
