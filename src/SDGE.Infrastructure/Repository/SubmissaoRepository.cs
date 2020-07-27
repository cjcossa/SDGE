using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SDGE.ApplicationCore.Entity;
using SDGE.ApplicationCore.Interfaces.Repository;
using SDGE.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDGE.Infrastructure.Repository
{
    public class SubmissaoRepository : EFRepository<Submissao>, ISubmissaoRepository
    {
       
        public SubmissaoRepository(ParticipanteContext dbContext) : base(dbContext)
        {
            
        }
        private bool state = false;
        public IEnumerable<Submissao> ObterPorEvento(int id)
        {

             return _dbContext.Set<Submissao>().Include(x => x.Participante).Include(x => x.Tipo).Include(x => x.Evento)
                .Where(x => x.EventoId == id && x.Removido == state && x.Evento.Removido == false && x.Tipo.Removido == false && x.Participante.Removido == false)
                .AsEnumerable();
            //return Buscar(x => x.EventoId == id && x.Removido == state && x.Evento.Submissoes.Any(e => e.Removido == state));
        }

        public IEnumerable<Submissao> ObterPorParticipante(int id)
        {
            return _dbContext.Set<Submissao>().Include(x => x.Participante).Include(x => x.Evento).Include(x => x.Tipo)
                 .Where(x => x.ParticipanteId == id && x.Removido == state &&
                x.Participante.Removido == state && x.Evento.Removido == state && x.Tipo.Removido == state)
                .AsEnumerable();
        }

        public override IEnumerable<Submissao> ObterTodos()
        {
            return _dbContext.Set<Submissao>().Include(x => x.Participante).Include(x => x.Evento).Include(x => x.Tipo)
                 .Where(x => x.Removido == state &&
                 x.Participante.Removido == state && x.Evento.Removido == state && x.Tipo.Removido == state)
                .AsEnumerable();
        }

        public Submissao ObterPorSubmissao(int id)
        {
            return _dbContext.Set<Submissao>().Include(x => x.Participante).Include(x => x.Evento).Include(x => x.Tipo)
               .Where(x => x.SubmissaoId == id && x.Removido == state &&
                x.Participante.Removido == state && x.Evento.Removido == state && x.Tipo.Removido == state)
               .FirstOrDefault();
        }
        public override void Remover(Submissao entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
    }
}
