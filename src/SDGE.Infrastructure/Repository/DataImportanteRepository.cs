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
    public class DataImportanteRepository : EFRepository<DataImportante>, IDataImportanteRepository
    {

        public DataImportanteRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        private DateTime dataActual = DateTime.Now.Date;
        private DateTime dataInicio;
        private DateTime dataFim;
        public bool VerificarFinalidade(string finalidade, int id, bool state)
        {
            var result = _dbContext.Set<DataImportante>()
                .Where(c => c.Finalidade.Equals(finalidade) && c.EventoId == id && c.Removido == state).FirstOrDefault();
            if (result == null)
                return false;

            return true;
        }

        public IEnumerable<DataImportante> ObterPorEvento(int id)
        {
            return _dbContext.Set<DataImportante>().Include(d => d.Evento)
                .Where(d => d.EventoId == id && d.Removido == state && d.Evento.Removido == state)
                .AsEnumerable();
        }
        public override void Remover(DataImportante entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }

        public DataImportante ObterPorData(int id)
        {
           return _dbContext.Set<DataImportante>()
                .Where(c => c.DataImportanteId == id && c.Removido == state).FirstOrDefault();
        }
        public override void Actualizar(DataImportante entity)
        {
            base.Actualizar(entity);
        }

        public DataImportante ObterPorFinalidade(string finalidade, int id, bool state)
        {
            return _dbContext.Set<DataImportante>()
                .Where(c => c.Finalidade.Equals(finalidade) && c.EventoId == id && c.Removido == state).FirstOrDefault();
        }

        public bool VerificarPrazoFinalidade(string finalidade, int id)
        {
            var result = _dbContext.Set<DataImportante>()
               .Where(c => c.Finalidade.Equals(finalidade) && c.EventoId == id && c.Removido == state).FirstOrDefault();
            if(result != null)
            {
                dataInicio = DateTime.Parse(result.DataInicio);
                dataFim = DateTime.Parse(result.DataFim);
                
                if (dataInicio <= dataActual && dataActual <= dataFim)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
