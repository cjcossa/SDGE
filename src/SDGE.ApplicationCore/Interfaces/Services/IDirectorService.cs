using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IDirectorService
    {
        Director Adicionar(Director entity);
        void Actualizar(Director entity);
        IEnumerable<Director> ObterTodos();
        Director ObterPorId(int id);
        IEnumerable<Director> Buscar(Expression<Func<Director, bool>> predicado);
        void Remover(Director entity);
    }
}
