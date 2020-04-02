using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroRepository : IRepository<Membro>
    {
        Membro ObterPorEvento(int membroId);
        public IEnumerable<Membro> ObterPorComissao(string comissao);
    }
}
