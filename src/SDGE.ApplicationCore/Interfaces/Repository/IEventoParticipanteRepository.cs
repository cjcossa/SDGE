using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IEventoParticipanteRepository : IRepository<EventoParticipante>
    {
        public IEnumerable<EventoParticipante> ObterJoinPorId(int id);
    }
}
