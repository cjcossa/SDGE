using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IParticipanteService
    {
        Participante Adicionar(Participante entity);
        void Actualizar(Participante entity);
        IEnumerable<Participante> ObterTodos();
        Participante ObterPorId(int id);
        IEnumerable<Participante> Buscar(Expression<Func<Participante, bool>> predicado);
        void Remover(Participante entity);
    }
}
