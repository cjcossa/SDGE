using Microsoft.EntityFrameworkCore;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class AlertaRepository : EFRepository<Alerta>, IAlertaRepository
    {

        public AlertaRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public IEnumerable<Alerta> ObterPorParticipante(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoCientifica).Include(a => a.ComissaoOrganizadora).Include(a => a.Participante)
                  .Where(a => a.Destino == state && a.ParticipanteId == id)
                  .AsEnumerable();
        }
    }
}
