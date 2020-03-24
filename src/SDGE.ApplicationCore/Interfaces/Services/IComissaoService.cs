using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IComissaoService
    {
        Comissao Adicionar(Comissao entity);
        void Actualizar(Comissao entity);
        IEnumerable<Comissao> ObterTodos();
        Comissao ObterPorId(int id);
        IEnumerable<Comissao> Buscar(Expression<Func<Comissao, bool>> predicado);
        void Remover(Comissao entity);
    }
}
