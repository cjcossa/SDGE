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
    public class CorrecaoRepository : EFRepository<Correcao>, ICorrecaoRepository
    {

        public CorrecaoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        public override IEnumerable<Correcao> ObterTodos()
        {
            return _dbContext.Set<Correcao>().Include(c => c.Submissao).Include(c => c.Membro);
        }
        public IEnumerable<Correcao> ObterJoinPorId(int id)
        {
            return ObterTodos().Where(c => c.CorrecaoId == id);
        }
    }
}
