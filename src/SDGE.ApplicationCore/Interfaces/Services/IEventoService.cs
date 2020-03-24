using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IEventoService
    {
        Evento Adicionar(Evento entity);
        void Actualizar(Evento entity);
        IEnumerable<Evento> ObterTodos();
        Evento ObterPorId(int id);
        IEnumerable<Evento> Buscar(Expression<Func<Evento, bool>> predicado);
        void Remover(Evento entity);
    }
}
