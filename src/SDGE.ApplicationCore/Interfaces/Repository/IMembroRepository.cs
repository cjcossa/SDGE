using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroRepository : IRepository<Membro>
    {
        public Membro ObterPorEvento(int membroId);
        public IEnumerable<Membro> ObterPorComissao(string comissao);
        public IEnumerable<Membro> ObterPorComissaoOrganizadora(int id);
        public Membro ObterPorEmail(string email);

    }
}
