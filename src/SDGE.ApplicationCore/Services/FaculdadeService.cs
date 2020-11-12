using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class FaculdadeService : IFaculdadeService
    {

        private readonly IFaculdadeRepository _faculdadeRepository;

        public FaculdadeService(IFaculdadeRepository faculdadeRepository)
        {
            _faculdadeRepository = faculdadeRepository;
        }

        public void Actualizar(Faculdade entity)
        {
            _faculdadeRepository.Actualizar(entity);
        }

        public Faculdade Adicionar(Faculdade entity)
        {
            return _faculdadeRepository.Adicionar(entity);
        }

        public IEnumerable<Faculdade> Buscar(Expression<Func<Faculdade, bool>> predicado)
        {
            return _faculdadeRepository.Buscar(predicado);
        }

        public Faculdade ObterPorId(int id)
        {
            return _faculdadeRepository.ObterPorId(id);
        }

        public IEnumerable<Faculdade> ObterTodos()
        {
            return _faculdadeRepository.ObterTodos();
        }

        public void Remover(Faculdade entity)
        {
            _faculdadeRepository.Remover(entity);
        }
    }
}
