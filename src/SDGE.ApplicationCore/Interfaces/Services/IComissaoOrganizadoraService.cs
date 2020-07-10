using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IComissaoOrganizadoraService
    {
        ComissaoOrganizadora Adicionar(ComissaoOrganizadora entity);
        void Actualizar(ComissaoOrganizadora entity);
        IEnumerable<ComissaoOrganizadora> ObterTodos();
        ComissaoOrganizadora ObterPorId(int id);
        IEnumerable<ComissaoOrganizadora> Buscar(Expression<Func<ComissaoOrganizadora, bool>> predicado);
        void Remover(ComissaoOrganizadora entity);
    }
}
