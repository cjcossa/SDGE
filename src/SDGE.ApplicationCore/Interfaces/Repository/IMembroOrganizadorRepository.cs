﻿using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IMembroOrganizadorRepository : IRepository<MembroOrganizador>
    {
        public bool VerificarMembro(int membroId, int comissaoId, bool _state);
        public IEnumerable<MembroOrganizador> ObterPorComissao(int id, bool confirmado = false);
        public IEnumerable<MembroOrganizador> ObterPorMembro(int id);
        public MembroOrganizador ObterPorMembroComissao(int membroId, int comissaoId, bool _state);
        public void Confirmar(MembroOrganizador entity);

    }
}
