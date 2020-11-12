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
            return _dbContext.Set<Alerta>().Include(a => a.Participante).Include(a => a.ComissaoOrganizadora)
                  .Where(a => a.Destino == !state && a.ParticipanteId == id && a.Removido == state && a.Status == state)
                  .AsEnumerable();

        }

        public IEnumerable<Alerta> ObterPorOrganizador(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoOrganizadora).Include(a => a.ComissaoOrganizadora.MembroOrganizadors).Include(a => a.Participante)
                 .Where(a => a.Destino == state && a.ComissaoOrganizadora.MembroOrganizadors.Any(a => a.MembroId == id) && a.Removido == state)
                 .AsEnumerable();
        }

        public IEnumerable<Alerta> ObterPorCientifico(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoCientifica).Include(a => a.ComissaoCientifica.MembroCientificos).Include(a => a.Participante)
                .Where(a => a.Destino == state && a.ComissaoCientifica.MembroCientificos.Any(a => a.MembroId == id) && a.Removido == state)
                .AsEnumerable();
        }

        public int TotalPorParticipante(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.Participante).Include(a => a.ComissaoOrganizadora)
                  .Where(a => a.Destino == !state && a.ParticipanteId == id && a.Removido == state && a.Status == state)
                  .ToList().Count;
        }

        public int TotalPorOrganizador(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoOrganizadora).Include(a => a.ComissaoOrganizadora.MembroOrganizadors).Include(a => a.Participante)
                 .Where(a => a.Destino == state && a.ComissaoOrganizadora.MembroOrganizadors.Any(a => a.MembroId == id) && a.Removido == state)
                 .ToList().Count();
        }

        public override void Actualizar(Alerta entity)
        {
            entity.Status = true;
            base.Actualizar(entity);
        }

        public IEnumerable<Alerta> ObterPorMembro(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoOrganizadora).Include(a => a.ComissaoOrganizadora.MembroOrganizadors).Include(a => a.Participante)
                  .Where(a => a.Destino == state && (a.ComissaoOrganizadora.MembroOrganizadors.Any(a => a.MembroId == id) || a.ComissaoCientifica.MembroCientificos.Any(a => a.MembroId == id)) 
                  && a.Removido == state && a.Status == state)
                  .AsEnumerable();
        }

        public int TotalPorMembro(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoOrganizadora).Include(a => a.ComissaoOrganizadora.MembroOrganizadors).Include(a => a.Participante)
                 .Where(a => a.Destino == state && (a.ComissaoOrganizadora.MembroOrganizadors.Any(a => a.MembroId == id) || a.ComissaoCientifica.MembroCientificos.Any(a => a.MembroId == id))
                 && a.Removido == state && a.Status == state)
                 .ToList().Count();
        }

        public int TotalPorCientifico(int id)
        {
            return _dbContext.Set<Alerta>().Include(a => a.ComissaoCientifica).Include(a => a.ComissaoCientifica.MembroCientificos).Include(a => a.Participante)
                .Where(a => a.Destino == state && a.ComissaoCientifica.MembroCientificos.Any(a => a.MembroId == id) && a.Removido == state)
                .ToList().Count();
        }
    }
}
