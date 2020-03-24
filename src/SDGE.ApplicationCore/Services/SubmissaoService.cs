using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class SubmissaoService : ISubmissaoService
    {

        private readonly ISubmissaoRepository _submissaoRository;

        public SubmissaoService(ISubmissaoRepository submissaoRepository)
        {
            _submissaoRository = submissaoRepository;
        }
        public void Actualizar(Submissao entity)
        {
            _submissaoRository.Actualizar(entity);
        }

        public Submissao Adicionar(Submissao entity)
        {
            return _submissaoRository.Adicionar(entity);
        }

        public IEnumerable<Submissao> Buscar(Expression<Func<Submissao, bool>> predicado)
        {
            return _submissaoRository.Buscar(predicado);
        }

        public Submissao ObterPorId(int id)
        {
            return _submissaoRository.ObterPorId(id);
        }

        public IEnumerable<Submissao> ObterTodos()
        {
            return _submissaoRository.ObterTodos();
        }

        public void Remover(Submissao entity)
        {
            _submissaoRository.Remover(entity);
        }
    }
}
