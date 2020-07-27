using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IComissaoCientificaRepository : IRepository<ComissaoCientifica>
    {
        public ComissaoCientifica ObterPorCodigo(string codigo);
        public string GenerateCode();
        public bool VerificarCodigo(string codigo);
        public IEnumerable<ComissaoCientifica> ObterPorMembro(int membroId);
    }
}
