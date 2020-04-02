using SDGE.ApplicationCore.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroEventoRepository : IRepository<MembroEvento>
    {
        public IEnumerable<MembroEvento> ObterJoinPorId(int id);
    }
}
