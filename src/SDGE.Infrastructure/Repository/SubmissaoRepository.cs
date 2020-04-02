using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class SubmissaoRepository : EFRepository<Submissao>, ISubmissaoRepository
    {
       
        public SubmissaoRepository(ParticipanteContext dbContext) : base(dbContext)
        {
            
        }
        public override IEnumerable<Submissao> ObterTodos()
        {
            return _dbContext.Set<Submissao>().Include(x => x.Participante).Include(x => x.Evento).Include(x => x.Tipo).AsEnumerable();
        }
    }
}
