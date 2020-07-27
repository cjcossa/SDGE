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
    public class EventoRepository : EFRepository<Evento>, IEventoRepository
    {

        public EventoRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        private DateTime dataActual = DateTime.Now.Date;
        
        //private IEnumerable<Evento> _result = null;
        public bool VerificarEmail(string email)
        {
            var result = _dbContext.Set<Evento>().Where(c => c.Email.Equals(email) && c.Removido == state).FirstOrDefault();
            //_result = Buscar(c => c.Email.Equals(email) && c.Removido == state);
            if (result == null)
                return false;

            return true;
        }

        public bool VerificarTitulo(string titulo)
        {
            var result = _dbContext.Set<Evento>().Where(c => c.Titulo.Equals(titulo) && c.Removido == state).FirstOrDefault();
            //_result = Buscar(c => c.Titulo.Equals(titulo) && c.Removido == state);
            if (result == null)
                return false;

            return true;
        }

        public Evento ObterPorEmail(string email)
        {
            return _dbContext.Set<Evento>().Where(c => c.Email.Equals(email) && c.Removido == state).FirstOrDefault();
        }

        public Evento ObterPorTitulo(string titulo)
        {
            return _dbContext.Set<Evento>().Where(c => c.Titulo.Equals(titulo) && c.Removido == state).FirstOrDefault();
        }
        public override void Remover(Evento entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
        public override IEnumerable<Evento> ObterTodos()
        {
            return base.ObterTodos()
                .Where(e => e.Removido == state && (DateTime.Parse(e.DataInicio) <= dataActual && dataActual <= DateTime.Parse(e.DataFim)));
        }

        public IEnumerable<Evento> ObterEventos(int membroId)
        {
            return Buscar(e => e.ComissaoOrganizadora.MembroOrganizadors.Any(m => m.Removido == state && m.MembroId == membroId) && e.Removido == state);
        }

        public Evento ObterPorEvento(int id)
        {
            throw new NotImplementedException();
        }
    }
}
