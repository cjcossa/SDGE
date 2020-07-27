using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroCientificoRepository : IRepository<MembroCientifico>
    {
        public bool VerificarMembro(int membroId, int comissaoId, bool state);
        public IEnumerable<MembroCientifico> ObterPorComissao(int id);
        public MembroCientifico ObterPorMembroComissao(int membroId, int comissaoId, bool state);
    }
}
