using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class EventoRepository : EFRepository<Evento>, IEventoRepository
    {

        public EventoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
    }
}
