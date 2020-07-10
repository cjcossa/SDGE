using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Services
{
    public interface IMembroCientificoService
    {
        MembroCientifico Adicionar(MembroCientifico entity);
        void Actualizar(MembroCientifico entity);
        IEnumerable<MembroCientifico> ObterTodos();
        MembroCientifico ObterPorId(int id);
        IEnumerable<MembroCientifico> Buscar(Expression<Func<MembroCientifico, bool>> predicado);
        void Remover(MembroCientifico entity);
    }
}
