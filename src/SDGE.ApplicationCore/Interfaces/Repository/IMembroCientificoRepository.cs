using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroCientificoRepository : IRepository<MembroCientifico>
    {
        public bool VerificarMembro(int membroId, int comissaoId, bool state);
        public IEnumerable<MembroCientifico> ObterPorComissao(int id, bool confirmado = false);
        public MembroCientifico ObterPorMembroComissao(int membroId, int comissaoId, bool state);
        public IEnumerable<MembroCientifico> ObterPorMembro(int id);
        public void Confirmar(MembroCientifico entity);
    }
}
