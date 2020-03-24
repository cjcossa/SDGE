using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class AlertaService : IAlertaRepository
    {

        private readonly IAlertaRepository _alertaRepository;

        public AlertaService(IAlertaRepository alertaRepository)
        {
            _alertaRepository = alertaRepository;
        }

        public void Actualizar(Alerta entity)
        {
            _alertaRepository.Actualizar(entity);
        }

        public Alerta Adicionar(Alerta entity)
        {
            return _alertaRepository.Adicionar(entity);
        }

        public IEnumerable<Alerta> Buscar(Expression<Func<Alerta, bool>> predicado)
        {
            return _alertaRepository.Buscar(predicado);
        }

        public Alerta ObterPorId(int id)
        {
            return _alertaRepository.ObterPorId(id);
        }

        public IEnumerable<Alerta> ObterTodos()
        {
            return _alertaRepository.ObterTodos();
        }

        public void Remover(Alerta entity)
        {
            _alertaRepository.Remover(entity);
        }
    }
}
