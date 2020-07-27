using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class ParticipanteRepository : EFRepository<Participante>, IParticipanteRepository
    {

        public ParticipanteRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public override Participante ObterPorId(int id)
        {
            return _dbContext.Set<Participante>().Where(c => c.ParticipanteId == id && c.Removido == state).FirstOrDefault();
        }
    }
}
