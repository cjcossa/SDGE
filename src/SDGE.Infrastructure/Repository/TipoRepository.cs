using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class TipoRepository : EFRepository<Tipo>, ITipoRepository
    {

        public TipoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public override IEnumerable<Tipo> ObterTodos()
        {
            return base.ObterTodos().Where(t => t.Removido == state);
        }
    }
}
