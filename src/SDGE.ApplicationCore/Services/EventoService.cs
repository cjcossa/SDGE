using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class EventoService : IEventoRepository
    {

        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }
        public void Actualizar(Evento entity)
        {
            _eventoRepository.Actualizar(entity);
        }

        public Evento Adicionar(Evento entity)
        {
            return _eventoRepository.Adicionar(entity);
        }

        public IEnumerable<Evento> Buscar(Expression<Func<Evento, bool>> predicado)
        {
            return _eventoRepository.Buscar(predicado);
        }

        public Evento ObterPorId(int id)
        {
            return _eventoRepository.ObterPorId(id);
        }

        public IEnumerable<Evento> ObterTodos()
        {
            return _eventoRepository.ObterTodos();
        }

        public void Remover(Evento entity)
        {
            _eventoRepository.Remover(entity);
        }
    }
}
