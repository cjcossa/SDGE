using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IComissaoCientificaService
    {
        ComissaoCientifica Adicionar(ComissaoCientifica entity);
        void Actualizar(ComissaoCientifica entity);
        IEnumerable<ComissaoCientifica> ObterTodos();
        ComissaoCientifica ObterPorId(int id);
        IEnumerable<ComissaoCientifica> Buscar(Expression<Func<ComissaoCientifica, bool>> predicado);
        void Remover(ComissaoCientifica entity);
    }
}
