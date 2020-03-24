using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IMembroService
    {
        Membro Adicionar(Membro entity);
        void Actualizar(Membro entity);
        IEnumerable<Membro> ObterTodos();
        Membro ObterPorId(int id);
        IEnumerable<Membro> Buscar(Expression<Func<Membro, bool>> predicado);
        void Remover(Membro entity);
    }
}
