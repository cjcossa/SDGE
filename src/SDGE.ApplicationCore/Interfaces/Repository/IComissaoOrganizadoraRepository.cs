using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IComissaoOrganizadoraRepository : IRepository<ComissaoOrganizadora>
    {
        public ComissaoOrganizadora ObterPorCodigo(string codigo);
        public string GenerateCode();
        public bool VerificarCodigo(string codigo);
        public IEnumerable<ComissaoOrganizadora> ObterPorMembro(int membroId);
       
    }
}
