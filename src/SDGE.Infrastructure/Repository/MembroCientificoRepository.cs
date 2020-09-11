using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class MembroCientificoRepository : EFRepository<MembroCientifico>, IMembroCientificoRepository
    {

        public MembroCientificoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public IEnumerable<MembroCientifico> ObterPorComissao(int id, bool confirmado = false)
        {
            return _dbContext.Set<MembroCientifico>().Include(c => c.ComissaoCientifica).Include(m => m.Membro)
                 .Where(c => c.ComissaoCientificaId == id && c.Removido == state && c.ComissaoCientifica.Removido == state && c.Confirmado == confirmado &&
                 c.ComissaoCientifica.CriadoPorId != c.MembroId).AsEnumerable();
        }

        public MembroCientifico ObterPorMembroComissao(int membroId, int comissaoId, bool _state)
        {
            return _dbContext.Set<MembroCientifico>().Include(c => c.ComissaoCientifica).Include(m => m.Membro)
                .Where(c => c.ComissaoCientifica.ComissaoCientificaId == comissaoId && c.Membro.MembroId == membroId && c.Removido == _state &&
                c.ComissaoCientifica.Removido == state && c.Membro.Removido == state).FirstOrDefault();
        }

        public bool VerificarMembro(int membroId, int comissaoId, bool _state)
        {
            var result = _dbContext.Set<MembroCientifico>().Include(c => c.ComissaoCientifica).Include(m => m.Membro)
                .Where(c => c.ComissaoCientificaId == comissaoId && c.MembroId == membroId && c.Removido == _state &&
                 c.ComissaoCientifica.Removido == state && c.Membro.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }
        public override void Actualizar(MembroCientifico entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
        public IEnumerable<MembroCientifico> ObterPorMembro(int id)
        {
            return _dbContext.Set<MembroCientifico>().Include(c => c.ComissaoCientifica).Include(m => m.ComissaoCientifica.Eventos)
               .Where(c => c.MembroId == id && c.Removido == state &&
                c.ComissaoCientifica.Removido == state && c.Confirmado == !state).AsEnumerable();
        }
        public void Confirmar(MembroCientifico entity)
        {
            base.Actualizar(entity);
        }
    }
}
