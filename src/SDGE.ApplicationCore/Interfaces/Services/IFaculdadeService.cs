using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IFaculdadeService
    {
        Faculdade Adicionar(Faculdade entity);
        void Actualizar(Faculdade entity);
        IEnumerable<Faculdade> ObterTodos();
        Faculdade ObterPorId(int id);
        IEnumerable<Faculdade> Buscar(Expression<Func<Faculdade, bool>> predicado);
        void Remover(Faculdade entity);
    }
}
