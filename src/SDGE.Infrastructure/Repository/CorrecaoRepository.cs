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
        private bool state = false;
        public override IEnumerable<Correcao> ObterTodos()
        {
            return _dbContext.Set<Correcao>().Include(c => c.Submissao).Include(c => c.Membro);
        }
        public IEnumerable<Correcao> ObterJoinPorId(int id)
        {
            return ObterTodos().Where(c => c.CorrecaoId == id);
        }

        public IEnumerable<Correcao> ObterPorSubmissao(int id)
        {
            return _dbContext.Set<Correcao>().Include(c => c.Submissao).Include(c => c.Membro)
                .Where(c => c.SubmissaoId == id && c.Removido == state && c.Submissao.Removido == state && c.Membro.Removido == state)
                .AsEnumerable();
        }

        public Correcao ObterPorCorrecao(int id)
        {
            return _dbContext.Set<Correcao>().Include(c => c.Submissao).Include(c => c.Membro)
               .Where(c => c.CorrecaoId == id && c.Removido == state && c.Submissao.Removido == state && c.Membro.Removido == state)
               .FirstOrDefault();
        }
        public override void Remover(Correcao entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
    }
}
