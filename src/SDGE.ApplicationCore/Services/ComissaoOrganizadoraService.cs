using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class ComissaoOrganizadoraService : IComissaoOrganizadoraService
    {

        private readonly IComissaoOrganizadoraRepository _comissaoOrganizadoraRepository;

        public ComissaoOrganizadoraService(IComissaoOrganizadoraRepository comissaoOrganizadoraRepository)
        {
            _comissaoOrganizadoraRepository = comissaoOrganizadoraRepository;
        }

        public void Actualizar(ComissaoOrganizadora entity)
        {
            _comissaoOrganizadoraRepository.Actualizar(entity);
        }

        public ComissaoOrganizadora Adicionar(ComissaoOrganizadora entity)
        {
            return _comissaoOrganizadoraRepository.Adicionar(entity);
        }

        public IEnumerable<ComissaoOrganizadora> Buscar(Expression<Func<ComissaoOrganizadora, bool>> predicado)
        {
            return _comissaoOrganizadoraRepository.Buscar(predicado);
        }

        public ComissaoOrganizadora ObterPorId(int id)
        {
            return _comissaoOrganizadoraRepository.ObterPorId(id);
        }

        public IEnumerable<ComissaoOrganizadora> ObterTodos()
        {
            return _comissaoOrganizadoraRepository.ObterTodos();
        }

        public void Remover(ComissaoOrganizadora entity)
        {
            _comissaoOrganizadoraRepository.Remover(entity);
        }
    }
}
