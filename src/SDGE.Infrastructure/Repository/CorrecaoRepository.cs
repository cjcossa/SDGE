using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class CorrecaoRepository : EFRepository<Correcao>, ICorrecaoRepository
    {

        public CorrecaoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
    }
}
