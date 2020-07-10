using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IDataImportanteService
    {
        DataImportante Adicionar(DataImportante entity);
        void Actualizar(DataImportante entity);
        IEnumerable<DataImportante> ObterTodos();
        DataImportante ObterPorId(int id);
        IEnumerable<DataImportante> Buscar(Expression<Func<DataImportante, bool>> predicado);
        void Remover(DataImportante entity);
    }
}
