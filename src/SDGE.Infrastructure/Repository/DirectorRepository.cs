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
    public class DirectorRepository : EFRepository<Director>, IDirectorRepository
    {

        public DirectorRepository(ParticipanteContext dbContext) : base(dbContext)
        {

        }
        private bool state = false;
        public override IEnumerable<Director> ObterTodos()
        {
            return _dbContext.Set<Director>().Include(x => x.Faculdade)
                 .Where(x => x.Removido == state && x.Faculdade.Removido == state)
                .AsEnumerable();
        }

        public bool VerificarFaculdade(int id)
        {
            var result = _dbContext.Set<Director>().Where(c => c.FaculdadeId == id && c.Removido == state).FirstOrDefault();

            if (result == null)
                return false;

            return true;
        }
        public bool VerificarEmail(string email)
        {
            var result = _dbContext.Set<Director>().Where(c => c.Email.Equals(email) && c.Removido == state).FirstOrDefault();
           
            if (result == null)
                return false;

            return true;
        }
        public override void Remover(Director entity)
        {
            entity.Removido = !entity.Removido;
            base.Actualizar(entity);
        }
    }
}
