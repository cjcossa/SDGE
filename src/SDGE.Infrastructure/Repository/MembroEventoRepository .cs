using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class MembroEventoRepository : EFRepository<MembroEvento>, IMembroEventoRepository
    {

        public MembroEventoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        public override IEnumerable<MembroEvento> ObterTodos()
        {
            return _dbContext.Set<MembroEvento>().Include(m => m.Membro).Include(m => m.Evento);
        }
        public IEnumerable<MembroEvento> ObterJoinPorId(int id)
        {
            return ObterTodos().Where(m => m.MembroEventoId == id);
        }
    }
}
