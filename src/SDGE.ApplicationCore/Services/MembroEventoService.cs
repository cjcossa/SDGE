using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class MembroEventoService : IMembroEventoService
    {

        private readonly IMembroEventoRepository _membroEventoRepository;

        public MembroEventoService(IMembroEventoRepository membroEventoRepository)
        {
            _membroEventoRepository = membroEventoRepository;
        }
        public void Actualizar(MembroEvento entity)
        {
            _membroEventoRepository.Actualizar(entity);
        }

        public MembroEvento Adicionar(MembroEvento entity)
        {
            return _membroEventoRepository.Adicionar(entity);
        }

        public IEnumerable<MembroEvento> Buscar(Expression<Func<MembroEvento, bool>> predicado)
        {
            return _membroEventoRepository.Buscar(predicado);
        }

        public MembroEvento ObterPorId(int id)
        {
            return _membroEventoRepository.ObterPorId(id);
        }

        public IEnumerable<MembroEvento> ObterTodos()
        {
            return _membroEventoRepository.ObterTodos();
        }

        public void Remover(MembroEvento entity)
        {
            _membroEventoRepository.Remover(entity);
        }
    }
}
