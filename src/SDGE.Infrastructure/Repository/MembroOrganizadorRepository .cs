﻿using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SDGE.Infrastructure.Repository
{
    public class MembroOrganizadorRepository : EFRepository<MembroOrganizador>, IMembroOrganizadorRepository
    {

        public MembroOrganizadorRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;

        public IEnumerable<MembroOrganizador> ObterPorComissao(int id, bool confirmado = false)
        {
            return _dbContext.Set<MembroOrganizador>().Include(c => c.ComissaoOrganizadora).Include(m => m.Membro)
                .Where(c => c.ComissaoOrganizadoraId == id && c.Removido == state &&
                 c.ComissaoOrganizadora.Removido == state && c.Membro.Removido == state &&  c.Confirmado == confirmado).AsEnumerable();
        }

        public bool VerificarMembro(int membroId, int comissaoId, bool _state)
        {
            var result = _dbContext.Set<MembroOrganizador>().Include(c => c.ComissaoOrganizadora).Include(m => m.Membro)
                .Where(c => c.ComissaoOrganizadoraId == comissaoId && c.MembroId == membroId && c.Removido == _state &&
                 c.ComissaoOrganizadora.Removido == state && c.Membro.Removido == state).FirstOrDefault();
            if (result == null)
                return false;
            
            return true;
        }

        public MembroOrganizador ObterPorMembroComissao(int membroId, int comissaoId, bool _state)
        {
           return _dbContext.Set<MembroOrganizador>().Include(c => c.ComissaoOrganizadora).Include(m => m.Membro)
                .Where(c => c.ComissaoOrganizadora.ComissaoOrganizadoraId == comissaoId && c.Membro.MembroId == membroId && c.Removido == _state &&
                 c.ComissaoOrganizadora.Removido == state && c.Membro.Removido == state).FirstOrDefault();
        }

        public override void Actualizar(MembroOrganizador entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }

        public IEnumerable<MembroOrganizador> ObterPorMembro(int id)
        {
            return _dbContext.Set<MembroOrganizador>().Include(c => c.ComissaoOrganizadora).Include(m => m.ComissaoOrganizadora.Eventos)
               .Where(c => c.MembroId == id && c.Removido == state &&
                c.ComissaoOrganizadora.Removido == state && c.Confirmado == !state).AsEnumerable();
        }

        public void Confirmar(MembroOrganizador entity)
        {
            base.Actualizar(entity);
        }
    }
}
