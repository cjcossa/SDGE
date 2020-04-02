using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IEventoParticipanteService
    {
        EventoParticipante Adicionar(EventoParticipante entity);
        void Actualizar(EventoParticipante entity);
        IEnumerable<EventoParticipante> ObterTodos();
        EventoParticipante ObterPorId(int id);
        IEnumerable<EventoParticipante> Buscar(Expression<Func<EventoParticipante, bool>> predicado);
        void Remover(EventoParticipante entity);
    }
}
