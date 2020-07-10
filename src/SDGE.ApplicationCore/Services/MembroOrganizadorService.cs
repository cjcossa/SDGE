using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class MembroOrganizadorService : IMembroOrganizadorService
    {

        private readonly IMembroOrganizadorRepository _membroOrganizadorRepository;

        public MembroOrganizadorService(IMembroOrganizadorRepository membroOrganizadorRepository)
        {
            _membroOrganizadorRepository = membroOrganizadorRepository;
        }
        public void Actualizar(MembroOrganizador entity)
        {
            _membroOrganizadorRepository.Actualizar(entity);
        }

        public MembroOrganizador Adicionar(MembroOrganizador entity)
        {
            return _membroOrganizadorRepository.Adicionar(entity);
        }

        public IEnumerable<MembroOrganizador> Buscar(Expression<Func<MembroOrganizador, bool>> predicado)
        {
            return _membroOrganizadorRepository.Buscar(predicado);
        }

        public MembroOrganizador ObterPorId(int id)
        {
            return _membroOrganizadorRepository.ObterPorId(id);
        }

        public IEnumerable<MembroOrganizador> ObterTodos()
        {
            return _membroOrganizadorRepository.ObterTodos();
        }

        public void Remover(MembroOrganizador entity)
        {
            _membroOrganizadorRepository.Remover(entity);
        }
    }
}
