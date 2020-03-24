using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IAlertaService
    {
        Alerta Adicionar(Alerta entity);
        void Actualizar(Alerta entity);
        IEnumerable<Alerta> ObterTodos();
        Alerta ObterPorId(int id);
        IEnumerable<Alerta> Buscar(Expression<Func<Alerta, bool>> predicado);
        void Remover(Alerta entity);
    }
}
