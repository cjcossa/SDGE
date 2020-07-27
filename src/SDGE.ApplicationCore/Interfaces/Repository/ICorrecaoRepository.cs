using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface ICorrecaoRepository : IRepository<Correcao>
    {
        public IEnumerable<Correcao> ObterJoinPorId(int id);
        public IEnumerable<Correcao> ObterPorSubmissao(int id);
        public Correcao ObterPorCorrecao(int id);
        
    }
}
