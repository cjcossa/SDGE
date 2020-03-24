using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class MembroService : IMembroService
    {

        private readonly IMembroRepository _membroRepository;

        public MembroService(IMembroRepository membroRepository)
        {
            _membroRepository = membroRepository;
        }
        public void Actualizar(Membro entity)
        {
            _membroRepository.Actualizar(entity);
        }

        public Membro Adicionar(Membro entity)
        {
            return _membroRepository.Adicionar(entity);
        }

        public IEnumerable<Membro> Buscar(Expression<Func<Membro, bool>> predicado)
        {
            return _membroRepository.Buscar(predicado);
        }

        public Membro ObterPorId(int id)
        {
            return _membroRepository.ObterPorId(id);
        }

        public IEnumerable<Membro> ObterTodos()
        {
            return _membroRepository.ObterTodos();
        }

        public void Remover(Membro entity)
        {
            _membroRepository.Remover(entity);
        }
    }
}
