using SDGE.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDGE.ApplicationCore.Interfaces.Repository
{
    public interface IAlertaRepository : IRepository<Alerta>
    {
        public IEnumerable<Alerta> ObterPorParticipante(int id);
        public IEnumerable<Alerta> ObterPorOrganizador(int id);
        public IEnumerable<Alerta> ObterPorCientifico(int id);
    }
}
