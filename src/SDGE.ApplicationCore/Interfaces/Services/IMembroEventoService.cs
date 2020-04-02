using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IMembroEventoService
    {
        MembroEvento Adicionar(MembroEvento entity);
        void Actualizar(MembroEvento entity);
        IEnumerable<MembroEvento> ObterTodos();
        MembroEvento ObterPorId(int id);
        IEnumerable<MembroEvento> Buscar(Expression<Func<MembroEvento, bool>> predicado);
        void Remover(MembroEvento entity);
    }
}
