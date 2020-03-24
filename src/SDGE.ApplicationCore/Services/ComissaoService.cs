using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class ComissaoService : IComissaoService
    {

        private readonly IComissaoRepository _comissaoRepository;

        public ComissaoService(IComissaoRepository comissaoRepository)
        {
            _comissaoRepository = comissaoRepository;
        }
        public void Actualizar(Comissao entity)
        {
            _comissaoRepository.Actualizar(entity);
        }

        public Comissao Adicionar(Comissao entity)
        {
            return _comissaoRepository.Adicionar(entity);
        }

        public IEnumerable<Comissao> Buscar(Expression<Func<Comissao, bool>> predicado)
        {
            return _comissaoRepository.Buscar(predicado);
        }

        public Comissao ObterPorId(int id)
        {
            return _comissaoRepository.ObterPorId(id);
        }

        public IEnumerable<Comissao> ObterTodos()
        {
            return _comissaoRepository.ObterTodos();
        }

        public void Remover(Comissao entity)
        {
            _comissaoRepository.Remover(entity);
        }
    }
}
