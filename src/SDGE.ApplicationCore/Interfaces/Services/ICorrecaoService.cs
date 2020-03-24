using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface ICorrecaoService
    {
        Correcao Adicionar(Correcao entity);
        void Actualizar(Correcao entity);
        IEnumerable<Correcao> ObterTodos();
        Correcao ObterPorId(int id);
        IEnumerable<Correcao> Buscar(Expression<Func<Correcao, bool>> predicado);
        void Remover(Correcao entity);
    }
}
