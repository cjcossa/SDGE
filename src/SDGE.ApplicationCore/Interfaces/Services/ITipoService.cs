using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface ITipoService
    {
        Tipo Adicionar(Tipo entity);
        void Actualizar(Tipo entity);
        IEnumerable<Tipo> ObterTodos();
        Tipo ObterPorId(int id);
        IEnumerable<Tipo> Buscar(Expression<Func<Tipo, bool>> predicado);
        void Remover(Tipo entity);
    }
}
