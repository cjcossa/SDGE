using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IInscricaoService
    {
        Inscricao Adicionar(Inscricao entity);
        void Actualizar(Inscricao entity);
        IEnumerable<Inscricao> ObterTodos();
        Inscricao ObterPorId(int id);
        IEnumerable<Inscricao> Buscar(Expression<Func<Inscricao, bool>> predicado);
        void Remover(Inscricao entity);
    }
}
