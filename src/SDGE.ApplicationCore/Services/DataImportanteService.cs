using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.ApplicationCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Services
{
    public class DataImportanteService : IDataImportanteService
    {

        private readonly IDataImportanteRepository _dataImportanteRepository;

        public DataImportanteService(IDataImportanteRepository dataImportanteRepository)
        {
             _dataImportanteRepository = dataImportanteRepository;
        }

        public void Actualizar(DataImportante entity)
        {
            _dataImportanteRepository.Actualizar(entity);
        }

        public DataImportante Adicionar(DataImportante entity)
        {
            return _dataImportanteRepository.Adicionar(entity);
        }

        public IEnumerable<DataImportante> Buscar(Expression<Func<DataImportante, bool>> predicado)
        {
            return _dataImportanteRepository.Buscar(predicado);
        }

        public DataImportante ObterPorId(int id)
        {
            return _dataImportanteRepository.ObterPorId(id);
        }

        public IEnumerable<DataImportante> ObterTodos()
        {
            return _dataImportanteRepository.ObterTodos();
        }

        public void Remover(DataImportante entity)
        {
            _dataImportanteRepository.Remover(entity);
        }
    }
}
