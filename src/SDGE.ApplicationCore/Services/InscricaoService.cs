using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class InscricaoService : IInscricaoService
    {

        private readonly IInscricaoRepository _inscricaoRepository;

        public InscricaoService(IInscricaoRepository inscricaoRepository)
        {
            _inscricaoRepository = inscricaoRepository;
        }
        public void Actualizar(Inscricao entity)
        {
            _inscricaoRepository.Actualizar(entity);
        }

        public Inscricao Adicionar(Inscricao entity)
        {
            return _inscricaoRepository.Adicionar(entity);
        }

        public IEnumerable<Inscricao> Buscar(Expression<Func<Inscricao, bool>> predicado)
        {
            return _inscricaoRepository.Buscar(predicado);
        }

        public Inscricao ObterPorId(int id)
        {
            return _inscricaoRepository.ObterPorId(id);
        }

        public IEnumerable<Inscricao> ObterTodos()
        {
            return _inscricaoRepository.ObterTodos();
        }

        public void Remover(Inscricao entity)
        {
            _inscricaoRepository.Remover(entity);
        }
    }
}
