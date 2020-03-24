using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface ISubmissaoService
    {
        Submissao Adicionar(Submissao entity);
        void Actualizar(Submissao entity);
        IEnumerable<Submissao> ObterTodos();
        Submissao ObterPorId(int id);
        IEnumerable<Submissao> Buscar(Expression<Func<Submissao, bool>> predicado);
        void Remover(Submissao entity);
    }
}
