using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class CorrecaoService : ICorrecaoService
    {

        private readonly ICorrecaoRepository _correcaoRepository;

        public CorrecaoService(ICorrecaoRepository correcaoRepository)
        {
            _correcaoRepository = correcaoRepository;
        }
        public void Actualizar(Correcao entity)
        {
            _correcaoRepository.Actualizar(entity);
        }

        public Correcao Adicionar(Correcao entity)
        {
            return _correcaoRepository.Adicionar(entity);
        }

        public IEnumerable<Correcao> Buscar(Expression<Func<Correcao, bool>> predicado)
        {
            return _correcaoRepository.Buscar(predicado);
        }

        public Correcao ObterPorId(int id)
        {
            return _correcaoRepository.ObterPorId(id);
        }

        public IEnumerable<Correcao> ObterTodos()
        {
            return _correcaoRepository.ObterTodos();
        }

        public void Remover(Correcao entity)
        {
            _correcaoRepository.Remover(entity);
        }
    }
}
