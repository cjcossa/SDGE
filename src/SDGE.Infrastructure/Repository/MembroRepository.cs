using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class MembroRepository : EFRepository<Membro>, IMembroRepository
    {

        public MembroRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        public Membro ObterPorTipo(int membroId)
        {
            return Buscar(m => m.MembroTipos.Any(t => t.MembroId == membroId))
                 .FirstOrDefault();
        }
    }
}
