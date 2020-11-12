using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class FaculdadeRepository : EFRepository<Faculdade>, IFaculdadeRepository
    {

        public FaculdadeRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public override IEnumerable<Faculdade> ObterTodos()
        {
            return base.ObterTodos().Where(t => t.Removido == state);
        }

        public bool VerificarDesignacao(string designacao)
        {
            var result = _dbContext.Set<Faculdade>().Where(c => c.Designacao.Equals(designacao) && c.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }

        public override void Remover(Faculdade entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
    }
}
