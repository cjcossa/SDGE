using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class ComissaoCientificaService : IComissaoCientificaService
    {

        private readonly IComissaoCientificaRepository _comissaoCientificaRepository;

        public ComissaoCientificaService(IComissaoCientificaRepository comissaoCientificaRepository)
        {
            _comissaoCientificaRepository = comissaoCientificaRepository;
        }

        public void Actualizar(ComissaoCientifica entity)
        {
            _comissaoCientificaRepository.Actualizar(entity);
        }

        public ComissaoCientifica Adicionar(ComissaoCientifica entity)
        {
            return _comissaoCientificaRepository.Adicionar(entity);
        }

        public IEnumerable<ComissaoCientifica> Buscar(Expression<Func<ComissaoCientifica, bool>> predicado)
        {
            return _comissaoCientificaRepository.Buscar(predicado);
        }

        public ComissaoCientifica ObterPorId(int id)
        {
            return _comissaoCientificaRepository.ObterPorId(id);
        }

        public IEnumerable<ComissaoCientifica> ObterTodos()
        {
            return _comissaoCientificaRepository.ObterTodos();
        }

        public void Remover(ComissaoCientifica entity)
        {
            _comissaoCientificaRepository.Remover(entity);
        }
    }
}
