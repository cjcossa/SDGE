using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class MembroCientificoService : IMembroCientificoService
    {

        private readonly IMembroCientificoRepository _membroCientificoRepository;

        public MembroCientificoService(IMembroCientificoRepository membroCientificoRepository)
        {
            _membroCientificoRepository = membroCientificoRepository;
        }
        public void Actualizar(MembroCientifico entity)
        {
            _membroCientificoRepository.Actualizar(entity);
        }

        public MembroCientifico Adicionar(MembroCientifico entity)
        {
            return _membroCientificoRepository.Adicionar(entity);
        }

        public IEnumerable<MembroCientifico> Buscar(Expression<Func<MembroCientifico, bool>> predicado)
        {
            return _membroCientificoRepository.Buscar(predicado);
        }

        public MembroCientifico ObterPorId(int id)
        {
            return _membroCientificoRepository.ObterPorId(id);
        }

        public IEnumerable<MembroCientifico> ObterTodos()
        {
            return _membroCientificoRepository.ObterTodos();
        }

        public void Remover(MembroCientifico entity)
        {
            _membroCientificoRepository.Remover(entity);
        }
    }
}
