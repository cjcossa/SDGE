using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class DirectorService : IDirectorService
    {

        private readonly IDirectorRepository _directorRepository;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public void Actualizar(Director entity)
        {
            _directorRepository.Actualizar(entity);
        }

        public Director Adicionar(Director entity)
        {
            return _directorRepository.Adicionar(entity);
        }

        public IEnumerable<Director> Buscar(Expression<Func<Director, bool>> predicado)
        {
            return _directorRepository.Buscar(predicado);
        }

        public Director ObterPorId(int id)
        {
            return _directorRepository.ObterPorId(id);
        }

        public IEnumerable<Director> ObterTodos()
        {
            return _directorRepository.ObterTodos();
        }

        public void Remover(Director entity)
        {
            _directorRepository.Remover(entity);
        }
    }
}
