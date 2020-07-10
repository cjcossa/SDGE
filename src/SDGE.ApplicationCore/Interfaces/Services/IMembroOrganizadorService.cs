using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IMembroOrganizadorService
    {
        MembroOrganizador Adicionar(MembroOrganizador entity);
        void Actualizar(MembroOrganizador entity);
        IEnumerable<MembroOrganizador> ObterTodos();
        MembroOrganizador ObterPorId(int id);
        IEnumerable<MembroOrganizador> Buscar(Expression<Func<MembroOrganizador, bool>> predicado);
        void Remover(MembroOrganizador entity);
    }
}
