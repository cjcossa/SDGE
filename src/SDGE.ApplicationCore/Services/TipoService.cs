using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class TipoService : ITipoService
    {

        private readonly ITipoRepository _tipoRepository;

        public TipoService(ITipoRepository tipoRepository)
        {
            _tipoRepository = tipoRepository;
        }
        public void Actualizar(Tipo entity)
        {
            _tipoRepository.Actualizar(entity);
        }

        public Tipo Adicionar(Tipo entity)
        {
            return _tipoRepository.Adicionar(entity);
        }

        public IEnumerable<Tipo> Buscar(Expression<Func<Tipo, bool>> predicado)
        {
            return _tipoRepository.Buscar(predicado);
        }

        public Tipo ObterPorId(int id)
        {
            return _tipoRepository.ObterPorId(id);
        }

        public IEnumerable<Tipo> ObterTodos()
        {
            return _tipoRepository.ObterTodos();
        }

        public void Remover(Tipo entity)
        {
            _tipoRepository.Remover(entity);
        }
    }
}
