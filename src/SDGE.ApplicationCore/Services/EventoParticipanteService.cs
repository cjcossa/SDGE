using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class EventoParticipanteService : IEventoParticipanteService
    {

        private readonly IEventoParticipanteRepository _eventoParticipanteRepository;

        public EventoParticipanteService(IEventoParticipanteRepository eventoParticipanteRepository)
        {
            _eventoParticipanteRepository = eventoParticipanteRepository;
        }
        public void Actualizar(EventoParticipante entity)
        {
            _eventoParticipanteRepository.Actualizar(entity);
        }

        public EventoParticipante Adicionar(EventoParticipante entity)
        {
            return _eventoParticipanteRepository.Adicionar(entity);
        }

        public IEnumerable<EventoParticipante> Buscar(Expression<Func<EventoParticipante, bool>> predicado)
        {
            return _eventoParticipanteRepository.Buscar(predicado);
        }

        public EventoParticipante ObterPorId(int id)
        {
            return _eventoParticipanteRepository.ObterPorId(id);
        }

        public IEnumerable<EventoParticipante> ObterTodos()
        {
            return _eventoParticipanteRepository.ObterTodos();
        }

        public void Remover(EventoParticipante entity)
        {
            _eventoParticipanteRepository.Remover(entity);
        }
    }
}
